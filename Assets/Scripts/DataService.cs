using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using System;
using System.IO;
using System.Linq;

public class DataService
{

    public static DataService instance;

    private SQLiteConnection _connection;
    private string currentDbPath = "";
    private bool dbExists;

    public DataService()
    {
        //CreateDB("GameNameDB");
        CreateDB("SatisfactionQuestDB");
        Connect();

        CreateIfNotExists<PlayerDTO>();
        CreateIfNotExists<LocationDTO>();
        CreateIfNotExists<ExitDTO>();
        CreateIfNotExists<ItemDTO>();
        CreateIfNotExists<SessionDTO>();
        //CreateIfNotExists<SessionItemDTO>();
    }



    //creates a database with the given name at the specified path
    private void CreateDB(string DatabaseName)
    {
        //var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);

        currentDbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName); ;
    }

    public void DeleteDatabaseFile()
    {
        File.Delete("SatisfactionQuestDB");
    }

    public void Connect()
    {
        _connection = new SQLiteConnection(currentDbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
    }

    private void CreateIfNotExists<T>() where T : new()
    {
        _connection.CreateTable<T>();
    }

   


    //Check if username and password is valid. Return true if count greater than 0
    public bool CheckLogin(string pUsername, string pPassword)
    {
        int count = _connection.Table<PlayerDTO>().Where(x => x.Username == pUsername
                                             && x.Password == pPassword).Count();

        return count > 0;
    }

    //Check if username is taken. Can be combined with CheckLogin, but this is the method for now
    public bool CheckUsernameExists(string pUsername)
    {
        int count = _connection.Table<PlayerDTO>().Where(x => x.Username == pUsername).Count();

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

    /**
    * Save locations from the GameModel location map, including their exits and items (deprecated)
    */
    public void CreateLocations()
    {
        var lcLocationMap = GameManager.instance.gameModel.worldMap;

        foreach (var location in lcLocationMap)    //for every location in the location map
        {
            LocationDTO locationDTO = new LocationDTO
            {
                Id = location.Value.id,
                Name = location.Value.name,
                Description = location.Value.description,
                Background = location.Value.background
            };

            SetLocation(locationDTO);       //set location is slightly unnessary because Locations are static in this implementation

            foreach (var exit in location.Value.exits)      //for every exit in the location's exit dictionary
            {
                ExitDTO exitDTO = new ExitDTO
                {
                    FromLocation = location.Value.id,
                    Direction = (int)exit.Key,          //the key value corresponds to the enum's int value for that direction
                    ToLocation = (int)exit.Value
                };
                SetExit(exitDTO);
            }
        }
    }   //SaveLocations

    public int CreateGameSession(string pUsername)
    {
        SessionDTO sessionDTO = new SessionDTO
        {
            Name_Player1 = pUsername,
            Location_Player1 = Location.NAME.TOMB.ToString()
        };
        _connection.Insert(sessionDTO);

        var q = _connection.Query<SessionDTO>(
            "select Id from SessionDTO"
            ).LastOrDefault();
        return q.Id;
    }

    public void CreateSessionItems(int pSessionId)
    {
        var lcWorldItems = GameManager.instance.gameModel.worldItems;

        foreach (var item in lcWorldItems)
        {
            ItemDTO itemDTO = new ItemDTO
            {
                Name = item.Key,
                Description = item.Value.description,
                Location = item.Value.location,
                SecretLetter = item.Value.secretLetter,
                SessionId = pSessionId,
            };
            SetItem(itemDTO);
        }
    }


    private bool LocationExists(int pLocationId)
    {
        var y = _connection.Table<LocationDTO>().Where(
                x => x.Id == pLocationId).FirstOrDefault();

        return y != null;
    }

    private bool ExitExists(int pExitId)
    {
        var y = _connection.Table<ExitDTO>().Where(
                x => x.Id == pExitId).FirstOrDefault();

        return y != null;
    }

    private bool ItemExists(int pItemId)
    {
        var y = _connection.Table<ItemDTO>().Where(
                x => x.Id == pItemId).FirstOrDefault();

        return y != null;
    }

    private bool SessionItemExists(int pSessionItemId)
    {
        var y = _connection.Table<ItemDTO>().Where(
                x => x.Id == pSessionItemId).FirstOrDefault();

        return y != null;
    }

    private void SetLocation(LocationDTO pLocationDTO)
    {
        CreateIfNotExists<LocationDTO>();

        if (LocationExists(pLocationDTO.Id))
        {
            _connection.Update(pLocationDTO);
        }
        else
        {
            _connection.Insert(pLocationDTO);
        }
    }

    private void SetExit(ExitDTO pExitDTO)
    {
        CreateIfNotExists<ExitDTO>();

        if (ExitExists(pExitDTO.Id))
        {
            _connection.Update(pExitDTO);
        }
        else
        {
            _connection.Insert(pExitDTO);
        }
    }

    private void SetItem(ItemDTO pItemDTO)
    {
        CreateIfNotExists<ItemDTO>();

        if (ItemExists(pItemDTO.Id))
        {
            _connection.Update(pItemDTO);
        }
        else
        {
            _connection.Insert(pItemDTO);
        }
    }

    //internal void SaveSessionItem(int pId)
    //{
    //    var lcWorldItems = GameManager.instance.gameModel.worldItems;

    //    foreach (var item in lcWorldItems)
    //    {
    //        ItemDTO itemDTO = new ItemDTO
    //        {
    //            Id = item.Value.id,
    //            Name = item.Key,
    //            Description = item.Value.description,
    //            Location = item.Value.location,
    //            SecretLetter = item.Value.secretLetter,
    //            SessionId = item.Value.sessionId
    //        };
    //        SetItem(itemDTO);
    //    }
    //}

    internal void SaveSessionItems()
    {
        var lcWorldItems = GameManager.instance.gameModel.worldItems;

        foreach (var item in lcWorldItems)
        {
            ItemDTO itemDTO = new ItemDTO
            {
                Id = item.Value.id,
                Name = item.Key,
                Description = item.Value.description,
                Location = item.Value.location,
                SecretLetter = item.Value.secretLetter,
                SessionId = item.Value.sessionId
            };
            SetItem(itemDTO);
        }
    }

    //internal void SaveSessionItems(int pSessionId)
    //{
    //    var lcWorldItems = GameManager.instance.gameModel.worldItems;

    //    foreach (var item in lcWorldItems)
    //    {
    //        ItemDTO itemDTO = new ItemDTO
    //        {
    //            Id = item.Value.id,
    //            Name = item.Key,
    //            Description = item.Value.description,
    //            Location = item.Value.location,
    //            SecretLetter = item.Value.secretLetter,
    //            SessionId = item.Value.sessionId
    //        };
    //        SetItem(itemDTO);
    //    }
    //}

    public void LoadSessionItems()
    {
        //GameManager.instance.gameModel.worldItems.Clear();
        //Debug.Log(GameManager.instance.gameModel.worldItems.Count());

        var itemDTOs = GetSessionItems(GameManager.instance.sessionId);

        foreach (ItemDTO itemDTO in itemDTOs)
        {
            GameManager.instance.gameModel.LoadWorldItem(itemDTO.Name, itemDTO.Location, itemDTO.SecretLetter);
        }



    }



    /**
     * GET methods to retrieve data
     */

    public IEnumerable<PlayerDTO> GetPlayers()
    {
        return _connection.Table<PlayerDTO>();
    }

    public PlayerDTO GetPlayer(string pUsername)
    {
        return _connection.Table<PlayerDTO>().Where(x => x.Username == pUsername).FirstOrDefault();
    }

    public IEnumerable<LocationDTO> GetLocations()
    {
        return _connection.Table<LocationDTO>();
    }

    public IEnumerable<ItemDTO> GetItems()
    {
        return _connection.Table<ItemDTO>();
    }

    public IEnumerable<SessionDTO> GetSessions()
    {
        return _connection.Table<SessionDTO>();
    }

    //returns last played session
    public SessionDTO GetPreviousSession(string pUsername)
    {
        return _connection.Table<SessionDTO>().Where(x => x.Name_Player1 == pUsername).LastOrDefault();
    }

    public bool PreviousSessionExists(string pUsername)
    {
        var y =  _connection.Table<SessionDTO>().Where(x => x.Name_Player1 == pUsername).LastOrDefault();
        return y != null;
    }

    public IEnumerable<ItemDTO> GetSessionItems(int pSessionId)
    {
        return _connection.Table<ItemDTO>().Where(x => x.SessionId == pSessionId);
    }

    public IEnumerable<ItemDTO> GetSessionLocationItems(int pSessionId, string pLocation)
    {
        return _connection.Table<ItemDTO>().
            Where(x => x.SessionId == pSessionId && x.Location == pLocation);
    }

    public IEnumerable<ItemDTO> GetPlayerItems(int pSessionId, string pUsername)
    {
        return _connection.Table<ItemDTO>().
            Where(x => x.SessionId == pSessionId && x.Location == pUsername);
    }

    /*
     * Get list of items from given session at given location
     * REFERENCE: https://stackoverflow.com/a/16007371 Author: chue x
     */
    //public IEnumerable<SessionItemDTO> GetSessionLocationItems(int pSessionId, string pLocationName)
    //{
    //    var q = _connection.Query<SessionItemDTO>(
    //        "select * from SessionItemDTO inner join ItemDTO"
    //        + " on ItemDTO.Name = SessionItemDTO.ItemName where (SessionItemDTO.Location = ?"
    //        + " and SessionItemDTO.SessionId = ?)", pLocationName, pSessionId);

    //    return q;
    //}

  
}
