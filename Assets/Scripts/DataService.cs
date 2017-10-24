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
        CreateIfNotExists<SessionItemDTO>();
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
        var y = _connection.Table<SessionItemDTO>().Where(
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

    private void SetSessionItem(SessionItemDTO pSessionItemDTO)
    {
        CreateIfNotExists<SessionItemDTO>();

        if (SessionItemExists(pSessionItemDTO.Id))
        {
            _connection.Update(pSessionItemDTO);
        }
        else
        {
            _connection.Insert(pSessionItemDTO);
        }
    }


    /**
     * Save locations from the GameModel location map, including their exits and items (deprecated)
     */
    public void SaveLocations()
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
            //_connection.Insert(locationDTO);   //insert the location DTO

            foreach (var exit in location.Value.exits)      //for every exit in the location's exit dictionary
            {
                ExitDTO exitDTO = new ExitDTO
                {
                    FromLocation = location.Value.id,
                    Direction = (int)exit.Key,          //the key value corresponds to the enum's int value for that direction
                    ToLocation = (int)exit.Value
                };
                SetExit(exitDTO);
                //_connection.Insert(exitDTO);   //insert the location's exit DTO
            }
        }
    }   //SaveLocations

    public void SaveItems()
    {
        var lcWorldItems = GameManager.instance.gameModel.worldItems;

        foreach (var item in lcWorldItems)
        {
            ItemDTO itemDTO = new ItemDTO
            {
                Id = (int)item.Key,
                Name = item.Value.name,
                Description = item.Value.description
            };
            SetItem(itemDTO);
            //_connection.Insert(itemDTO);   //insert an item 
        }
    }

    public void SaveSessionItems(int pSessionId)
    {
        var lcWorldItems = GameManager.instance.gameModel.worldItems;

        foreach (var item in lcWorldItems)
        {
            SessionItemDTO sessionItemDTO = new SessionItemDTO
            {
                ItemName = item.Value.name,
                SessionId = pSessionId,
                Location = item.Value.location
            };
            SetSessionItem(sessionItemDTO);
            //_connection.Insert(sessionItemDTO);   //insert an item 
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

    public IEnumerable<SessionItemDTO> GetSessionItems()
    {
        return _connection.Table<SessionItemDTO>();
    }

    /*
     * Get list of items from given session at given location
     * REFERENCE: https://stackoverflow.com/a/16007371 Author: chue x
     */
    //public IEnumerable<ItemDTO> GetSessionLocationItems(int pSessionId, string pLocationName)
    //{
    //    var q = _connection.Query<ItemDTO>(
    //        "select * from ItemDTO inner join SessionItemDTO"
    //        + " on ItemDTO.Name = SessionItemDTO.ItemName where (SessionItemDTO.Location = ?"
    //        + " and SessionItemDTO.SessionId = ?)", pLocationName, pSessionId);

    //    return q;
    //}

    public IEnumerable<SessionItemDTO> GetSessionLocationItems(int pSessionId, string pLocationName)
    {
        var q = _connection.Query<SessionItemDTO>(
            "select * from SessionItemDTO inner join ItemDTO"
            + " on ItemDTO.Name = SessionItemDTO.ItemName where (SessionItemDTO.Location = ?"
            + " and SessionItemDTO.SessionId = ?)", pLocationName, pSessionId);

        return q;
    }

    //public IEnumerable<ItemDTO> GetSessionLocationItems(string pLocationName)
    //{
    //    //_connection.Table<SessionItemDTO>().Join<ItemDTO>().Where(x => x.Location)
    //    //return _connection.Table<ItemDTO>().Where(x => x.Location == pLocationName);

    //    Debug.Log("In DataService.GetSessionLocationItems");

    //    var q = _connection.Query<ItemDTO>(
    //        "select * from ItemDTO inner join SessionItemDTO"
    //        + " on ItemDTO.Name = SessionItemDTO.ItemName where SessionItemDTO.Location = ?", pLocationName);


    //    return q;
    //}

    //var q = db.Query<Questions>(
    //"select Q.* from Questions Q inner join GameSaved G"
    //+ " on Q.QuestionId = G.QuestionId"
    //).First();

    /**
     * Game state would save 
     */

    public void SaveGameState()
    {

    }

    public void UpdateGameModel(int pSessionId)
    {
        var lcWorldItems = GameManager.instance.gameModel.worldItems;

        foreach (var item in lcWorldItems)
        {
            ItemDTO itemDTO = new ItemDTO
            {
                Name = item.Value.name,
                Description = item.Value.description
            };
            _connection.Update(itemDTO);   //insert an item 
        }
    }

    public void SetItemLocation(int pSessionId, string pLocation)
    {
        //_connection.Update()
    }

    //public int CreateGameSession(string pUsername)
    //{
    //    SessionDTO sessionDTO = new SessionDTO();
    //    _connection.Insert(sessionDTO);

    //    //return _connection.Table<SessionDTO>().OrderByDescending<>();
    //    //_connection.las
    //}

    public int CreateGameSession()
    {
        SessionDTO sessionDTO = new SessionDTO();
        _connection.Insert(sessionDTO);

        var q = _connection.Query<SessionDTO>(
            "select Id from SessionDTO"
            ).LastOrDefault();
        return q.Id;
    }

    public int CreateGameSession(string pUsername)
    {
        SessionDTO sessionDTO = new SessionDTO
        {
            Name_Player1 = pUsername,
            Location_Player1 = GameModel.locationNames[0]
        };
        _connection.Insert(sessionDTO);

        var q = _connection.Query<SessionDTO>(
            "select Id from SessionDTO"
            ).LastOrDefault();
        return q.Id;
    }

    public SessionDTO LoadGameSession(string pUsername)
    {
        return _connection.Table<SessionDTO>().Where(x => x.Name_Player1 == pUsername).LastOrDefault();
        //_connection.Table<SessionDTO>.

        //        public PlayerDTO GetPlayer(string pUsername)
        //{
        //    return _connection.Table<PlayerDTO>().Where(x => x.Username == pUsername).FirstOrDefault();
        //}
        //SessionDTO sessionDTO = new SessionDTO
        //{
        //    Name_Player1 = pUsername,
        //    Location_Player1 = GameModel.locationNames[0]
        //};
        //_connection.Insert(sessionDTO);

        //var q = _connection.Query<SessionDTO>(
        //    "select Id from SessionDTO"
        //    ).LastOrDefault();
        //return q.Id;
    }


    public void AddPlayerToGameSession(string pUsername)
    {

    }

    /**
     * Test methods for debugging purposes
     */

    public static void DisplayPlayers()
    {
        DataService dataService = new DataService();    //could be replaced by a static object

        foreach (PlayerDTO player in dataService.GetPlayers())
        {
            Debug.Log(player.Id + " - " + player.Username + " - " + player.Password);
        }
    }

    //public static void DisplayLocationItems(string pLocationName)
    //{
    //    DataService dataService = new DataService();    //could be replaced by a static object

    //    foreach (ItemDTO item in dataService.GetLocationItems(pLocationName))
    //    {
    //        Debug.Log(item.Id + " - " + item.Name + " - " + item.Description + " - " + item.Location + "\n"); 
    //    }
    //}

    public static void DisplayAllItems()
    {
        DataService dataService = new DataService();    //could be replaced by a static object

        foreach (ItemDTO item in dataService.GetItems())
        {
            Debug.Log("Item: " + item.Id + " - " + item.Name + " - " + item.Description + "\n");
        }
    }

    public static void DisplayAllLocations()
    {
        DataService dataService = new DataService();    //could be replaced by a static object

        foreach (LocationDTO location in dataService.GetLocations())
        {
            Debug.Log("Location: " + location.Id + " - " + location.Name + " - " + location.Description + " - " + location.Background + "\n");
        }
    }


    public static void DisplayAllSessions()
    {
        DataService dataService = new DataService();    //could be replaced by a static object

        foreach (SessionDTO session in dataService.GetSessions())
        {
            Debug.Log("Session: " + session.Id + " - " + session.Name_Player1 + "\n");
        }
    }

    public static void DisplayAllSessionItems()
    {
        DataService dataService = new DataService();    //could be replaced by a static object

        foreach (SessionItemDTO sessionItem in dataService.GetSessionItems())
        {
            Debug.Log("SessionItem: " + sessionItem.Id + " - " + sessionItem.ItemName + " - " + sessionItem.SessionId + " - " + sessionItem.Location + "\n");
        }
    }
}
