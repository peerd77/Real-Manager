using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Real_Manager
{
    public class TeamVM:Real_Manager.Manager.IIdentified
    {

        #region  properties

        public string Id { get; set; }
        //public string PlayerId { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public List<PlayerVM> Players { get; set; }
        //[HiddenInput(DisplayValue = false)]
        public int Version { get; set; }

        #endregion


        #region Constructors
        public TeamVM() { }


        public TeamVM(string name)
        {
            Name = name;
        }

        public TeamVM(string name, List<PlayerVM> players)
        {
            Name = name;
            Points = 0;
            if (players == null) Players = new List<PlayerVM>();
            else Players = players;
        }

        public TeamVM(string name, int points)
        {
            Name = name;
            Points = points;
        }

        #endregion



        #region Methods

        public TeamVM getPlayer()
        {
            return this; //?
            //Maybe return bson instead
        }

        public int getPoints()
        {
            return Points;
        }

        public void addPoints(int pointsToAdd)
        {
            Points += pointsToAdd;
        }

        public void addPlayer(PlayerVM player)
        {
            if (Players == null)
            {
                Players = new List<PlayerVM>();
                Players.Add(player);
            }

            else
                Players.Add(player);
        }

        public void removePlayer(string playerId)
        {
            int index = Players.FindIndex(p => p.Id == playerId);
            Players.RemoveAt(index);
        }

        #endregion
    }
}
