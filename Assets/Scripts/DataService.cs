using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using System;

public class DataService {

    private SQLiteConnection _connection;
    private string currentDbPath = "";
    private bool dbExists;

    public DataService()
    {
        //CreateDB("GameNameDB");
        CreateDB("SatisfactionQuestDB");
        Connect();
        _connection.CreateTable<PlayerDTO>();

    }

    //creates a database with the given name at the specified path
    private void CreateDB(string DatabaseName)
    {
        var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);

        currentDbPath = dbPath;
    }

    public void Connect()
    {
        _connection = new SQLiteConnection(currentDbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
    }

    //Check if username and password is valid. Return true if count greater than 0
    public bool CheckLogin(string pUsername, string pPassword)
    {
        int count = _connection.Table<PlayerDTO>().Where(x => x.Username == pUsername
                                                         && x.Password == pPassword).Count();

        return count > 0;
    }

    //Add a player to PlayerDTO table
    public void AddPlayer(string pUsername, string pPassword)
    {
        var player = _connection.Insert(new PlayerDTO()
        {
            Username = pUsername,
            Password = pPassword
        });
    }

    public IEnumerable<PlayerDTO> GetPlayers()
    {
        return _connection.Table<PlayerDTO>();
    }

    public PlayerDTO GetPlayer(string pUsername)
    {
        return _connection.Table<PlayerDTO>().Where(x => x.Username == pUsername).FirstOrDefault();
    }

    /**
     * Save locations from the GameModel location map, including their exits and items (deprecated)
     */
    
    public void SaveLocations()
    {
        var lcLocationMap = GameManager.instance.gameModel.locationMap;

        foreach (var location in lcLocationMap)    //for every location in the location map
        {
            LocationDTO locationDTO = new LocationDTO
            {
                Name = location.Value.name,
                Description = location.Value.description,
                Background = location.Value.background
            };

            _connection.Insert(locationDTO);   //insert the location DTO

            foreach (var exit in location.Value.exits)      //for every exit in the location's exit dictionary
            {
                ExitDTO exitDTO = new ExitDTO
                {
                    FromName = location.Value.name,
                    Direction = (int)exit.Key,          //the key value corresponds to the enum's int value for that direction
                    ToName = exit.Value
                };

                _connection.Insert(exitDTO);   //insert the location's exit DTO
            }




            /*
             *  update will not delete items that have been picked up. So does this need a dropped table every time?
             *  Also, how will these DTOs capture the correct player's world?
             */

            //foreach (var anItem in aLocation.Value.items)   //for every item at the location
            //{
            //    ItemDTO anItemDTO = new ItemDTO
            //    {
            //        Name = anItem.name,
            //        Location = anItem.location,
            //        Description = anItem.description
            //    };

            //    _connection.Insert(anItemDTO);   //insert an item at the location
            //}
        }
    }   //SaveLocations

    public void SaveItems()
    {
        var lcWorldItems = GameManager.instance.gameModel.worldItems;

        foreach(var item in lcWorldItems)
        {
            ItemDTO itemDTO = new ItemDTO
            {
                Name = item.name,
                Location = item.location,
                Description = item.description
            };
            _connection.Insert(itemDTO);   //insert an item 

        }

    }


}
