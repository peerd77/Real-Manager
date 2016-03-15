using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using MongoDB.Driver.Linq;




namespace Real_Manager
{
    public static class Manager
    {

        private  static  int idLength = 10;
        private static IMongoClient _client = new MongoClient("mongodb://127.0.0.1:27017/test");
        private static IMongoDatabase _database = _client.GetDatabase("test");

        private static string randomId()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, idLength)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        private static async Task<string> getId(string collectionName)
        {
           
            bool isFoundId = false;
            string newId = null;
            List<BsonDocument> resultedDoc = null;
            var collection = _database.GetCollection<BsonDocument>(collectionName);
            while (!isFoundId)
            {
                newId = randomId();
                var filter = Builders<BsonDocument>.Filter.Eq("Id", newId);
                Task<List<BsonDocument>> doc = collection.Find(filter).ToListAsync();
                resultedDoc = await doc;
                if(resultedDoc.Count == 0)
                {
                    isFoundId = true;
                }
            }

            return newId;            
        }
        
        public static async Task<PlayerVM> getPlayer(string playerId)
        {
            var collection = _database.GetCollection<PlayerVM>("players");
            var filter = Builders<PlayerVM>.Filter.Eq("Id", playerId);
            PlayerVM player1 = await collection.Find<PlayerVM>(filter).FirstAsync<PlayerVM>();
            return player1;
        }

        public static async Task<TeamVM> getTeam(string teamId)
        {
            var teams = _database.GetCollection<TeamVM>("teams");
            TeamVM team = await teams.Find<TeamVM>(t => t.Id == teamId).FirstAsync<TeamVM>();
            return team;          
        }

        public static async void getLeague(List<TeamVM> outListOfTeams)
        {
            // Todo later
            
        }

        public static async Task<PlayerVM> addPlayer(PlayerVM playerToAdd)
        {
            Task<string> newIdTask = getId("players");
            string newId = await newIdTask;
            playerToAdd.Id = newId;                  
            var playersCollection = _database.GetCollection<PlayerVM>("players");
            await playersCollection.InsertOneAsync(playerToAdd);
            return playerToAdd;
        }


        public interface IIdentified
        {
            string Id { get; }
            int Version { get; set; }
        }

        public static async Task<TeamVM> addTeam(TeamVM teamToAdd)
        {

            string newId = await getId("teams");
            teamToAdd.Id = newId;
            teamToAdd.Version = 0;
            var teamCollection = _database.GetCollection<TeamVM>("teams");
            await teamCollection.InsertOneAsync(teamToAdd);
            return teamToAdd;
        }


        public static async Task<List<TeamVM>> getTeams()
        {
            var teams = _database.GetCollection<TeamVM>("teams");
             List<TeamVM> ans = await teams.Find(new BsonDocument()).ToListAsync(); 
            return ans;
        }

        public static async Task<List<PlayerVM>>  getTeamPlayers(string teamId)
        {
            
            var teams = _database.GetCollection<TeamVM>("teams");
            TeamVM team = null;
            try
            {
                 team = await teams.Find(t => t.Id == teamId).FirstAsync<TeamVM>();    
            }
            catch(Exception e)
            {
                team = new TeamVM("");
            }
            return team.Players; 
        }

        public static async Task<List<PlayerVM>> getPlayers(string teamId)
        {
            TeamVM team = await getTeam(teamId);
            return team.Players;
        }


        public static async Task<TeamVM> updateTeam(TeamVM teamToChange)
        {
            var teams = _database.GetCollection<TeamVM>("teams");
            var update = Builders<TeamVM>.Update.Set(t => t.Name, teamToChange.Name)
                .Set(t => t.Players, teamToChange.Players).Set(t => t.Points, teamToChange.Points)
                .Inc(t => t.Version, 1);
            var filter = Builders<TeamVM>.Filter.Where(t => t.Id == teamToChange.Id
                   && t.Version == teamToChange.Version);
            return await teams.FindOneAndUpdateAsync<TeamVM>(filter, update);
        }

      

        public static async Task<bool> deleteTeam(TeamVM teamToDelete)
        {
            var teams = _database.GetCollection<TeamVM>("teams");
            var result = await teams.DeleteOneAsync(i => i.Id == teamToDelete.Id && i.Version == teamToDelete.Version);
            if (result.IsAcknowledged && result.DeletedCount == 1) 
                return true;
            return false;
        }

    }
}
