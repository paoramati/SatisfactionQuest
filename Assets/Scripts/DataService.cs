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

    private bool LocationExists(string pLocationName)
    {
        var y = _connection.Table<LocationDTO>().Where(
                x => x.Name == pLocationName).FirstOrDefault();

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

    private bool SessionItemExists(int pId, int pSessionId, string pItemName)
    {
        var y = _connection.Table<ItemDTO>().Where(
                x => x.Id == pId && x.SessionId == pSessionId && x.Name == pItemName).FirstOrDefault();

        return y != null;
    }

    private bool SessionItemExists(int pSessionId, string pItemName)
    {
        var y = _connection.Table<ItemDTO>().Where(
                x => x.SessionId == pSessionId && x.Name == pItemName).FirstOrDefault();

        return y != null;
    }

    private bool SessionItemExists(int pId)
    {
        var y = _connection.Table<ItemDTO>().Where(
                x => x.Id == pId).FirstOrDefault();

        return y != null;
    }


    private void SetLocation(LocationDTO pLocationDTO)
    {
        CreateIfNotExists<LocationDTO>();

        if (LocationExists(pLocationDTO.Name))
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

    private void InsertSessionItem(ItemDTO pItemDTO)
    {
        if (!SessionItemExists(pItemDTO.SessionId, pItemDTO.Name))
        {
            _connection.Insert(pItemDTO);
        }
    }

    private void UpdateSessionItem(ItemDTO pItemDTO)
    {
        if (SessionItemExists(pItemDTO.SessionId, pItemDTO.Name))
        {
            _connection.Insert(pItemDTO);
        }
    }

    private void SetSessionItem(ItemDTO pItemDTO)
    {
        if (SessionItemExists(pItemDTO.Id, pItemDTO.SessionId, pItemDTO.Name))   //if every parameter of session item matches
        {

        }

        if (SessionItemExists(pItemDTO.SessionId, pItemDTO.Name))   //if session item exists, perhaps without id
        {

        }

        if (SessionItemExists(pItemDTO.Id))     //if inserted session item exists
        {

        }


            
        //CreateIfNotExists<ItemDTO>();

        //if (SessionItemExists(pItemDTO.SessionId, pItemDTO.Name, pItemDTO.Id))
        if (SessionItemExists(pItemDTO.SessionId, pItemDTO.Name))
        {
            Debug.Log("Update SessionItem: id = " + pItemDTO.Id + " name = " + pItemDTO.Name + " location = " + pItemDTO.Location);
            _connection.Update(pItemDTO);
        }
        else
        {
            Debug.Log("Insert SessionItem: id = " + pItemDTO.Id + " name = " + pItemDTO.Name + " location = " + pItemDTO.Location + " sessionId = " + pItemDTO.SessionId);
            _connection.Insert(pItemDTO);
        }
    }

    //private void CreateSessionItem(ItemDTO pItemDTO)
    //{
    //    if (SessionItemExists(pItemDTO))
    //}

    /**
    * Save / create locations from the GameModel location map, including their exits and items (deprecated)
    */
    public void SaveLocations()
    {
        var lcLocationMap = GameManager.instance.gameModel.worldMap;

        foreach (var location in lcLocationMap)    //for every location in the location map
        {
            LocationDTO locationDTO = new LocationDTO
            {
                Name = location.Value.name,
                Description = location.Value.description,
                Background = location.Value.background
            };

            SetLocation(locationDTO);       //set location is slightly unnessary because Locations are static in this implementation

            foreach (var exit in location.Value.exits)      //for every exit in the location's exit dictionary
            {
                ExitDTO exitDTO = new ExitDTO
                {
                    FromLocation = location.Value.name,
                    Direction = (int)exit.Key,          //the key value corresponds to the enum's int value for that direction
                    ToLocation = exit.Value
                };
                SetExit(exitDTO);
            }
        }
    }   

    public void LoadLocations()
    {
        //var locationDTOs = 

    }

    //create session items for new game session
    public void CreateSessionItems(int pSessionId)
    {
        var lcWorldItems = GameManager.instance.gameModel.worldItems;

        Debug.Log("CreateSessionItems() Before");

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
            InsertSessionItem(itemDTO);


            //SetSessionItem(itemDTO);
            //need to get DTO id value back to local copy
        }
    }

    //save state of session items
    public void SaveSessionItems()
    {
        var lcWorldItems = GameManager.instance.gameModel.worldItems;

        //Debug.Log("SaveSessionItems() Before");

        ////DataServiceUtilities.DisplayAllSessionItems();


        foreach (var item in lcWorldItems)
        {
            //Debug.Log(item.Value.location);
            ItemDTO itemDTO = new ItemDTO
            {
                Id = item.Value.id,
                Name = item.Key,
                Description = item.Value.description,
                Location = item.Value.location,
                SecretLetter = item.Value.secretLetter,
                SessionId = item.Value.sessionId
            };
            SetSessionItem(itemDTO);
        }
        Debug.Log("SaveSessionItems() After");

        //DataServiceUtilities.DisplayAllSessionItems();
    }

    public void UpdateLocalItems()
    {
        foreach (var item in GetSessionItems(GameManager.instance.sessionId))
        {
            GameManager.instance.gameModel.LoadWorldItem(item);

        }
        //foreach(var item in GameManager.instance.gameModel.worldItems)
        //{
        //    GameManager.instance.gameModel.LoadWorldItem()
        //}
    }

    //load state of session items
    public void LoadSessionItems()
    {
        var itemDTOs = GetSessionItems(GameManager.instance.sessionId);     //get session items from current instance of session id
        Debug.Log("LoadSessionItems()");
        DataServiceUtilities.DisplayAllSessionItems();


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

    public IEnumerable<ExitDTO> GetExits()
    {
        return _connection.Table<ExitDTO>();
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
