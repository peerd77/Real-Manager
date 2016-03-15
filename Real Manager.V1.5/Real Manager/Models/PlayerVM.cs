using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Real_Manager
{
    public class PlayerVM
    {
       
        #region  properties 

        public string Id {get; set;}
        //public string PlayerId { get; set; }
        public string Name { get; set; }
        public int Stars { get; set; }
        public string Position { get; set; }
        public long Price { get; set; }
        public string TeamId { get; set; }
        
        #endregion


        #region Constructors

        public PlayerVM() { }

        public PlayerVM(PlayerVM player)
        {
            Name = player.Name;
            Stars = player.Stars;
            
      
        }
  
        public PlayerVM(string name)
        {
            Name = name;
            
            Stars = 0;

        }

        public PlayerVM(string name, int stars)
        {
            Name = name;
            Stars = stars;      
        }

        #endregion


        #region Methods
        public string getPlayer()
        {
            return JsonConvert.SerializeObject(this);
            //Maybe return bson instead
        }
        public int getPoints()
        {
            return Stars;
        }
        public void addPoints(int pointsToAdd)
        {
            Stars += pointsToAdd;
        }

        
        #endregion
    }
}
