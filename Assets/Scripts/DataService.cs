using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using System;
using System.IO;
using System.Linq;

public class DataService {

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
                    FromLocation = location.Value.name,
                    Direction = (int)exit.Key,          //the key value corresponds to the enum's int value for that direction
                    ToLocation = exit.Value
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

    /**
     * Insert or update the permanent details of the game model - location and items
     */

    public void SaveItems()
    {
        var lcWorldItems = GameManager.instance.gameModel.worldItems;

        foreach(var item in lcWorldItems)
        {
            ItemDTO itemDTO = new ItemDTO
            {
                Name = item.name,
                Description = item.description
            };

            _connection.Insert(itemDTO);   //insert an item 
        }
    }

    public void SaveSessionItems(int pSessionId)
    {
        var lcWorldItems = GameManager.instance.gameModel.worldItems;

        foreach (var item in lcWorldItems)
        {
            SessionItemDTO sessionItemDTO = new SessionItemDTO
            {
                ItemName = item.name,
                SessionId = pSessionId,
                Location = item.location
            };

            _connection.Insert(sessionItemDTO);   //insert an item 
        }
    }

    //private bool SceneExists(int pSceneID)
    //{
    //    var y = _connection.Table<SceneDTO>().Where(
    //            x => x.SceneID == pSceneID).FirstOrDefault();

    //    return y != null;

    //}

    //private void SetScene(SceneDTO aSceneDTO)
    //{
    //    CreateIfNotExists<SceneDTO>();

    //    if (SceneExists(aSceneDTO.SceneID))
    //    {
    //        _connection.Update(aSceneDTO);
    //    }
    //    else
    //    {
    //        _connection.Insert(aSceneDTO);
    //    }
    //}

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


    //public IEnumerable<ItemDTO> GetLocationItems(string pLocationName)
    //{
    //    return _connection.Table<ItemDTO>().Where(x => x.Location == pLocationName);
    //}

    //REFERENCE: https://stackoverflow.com/a/16007371 Author: chue x

    public IEnumerable<ItemDTO> GetSessionLocationItems(string pLocationName)
    {
        //_connection.Table<SessionItemDTO>().Join<ItemDTO>().Where(x => x.Location)
        //return _connection.Table<ItemDTO>().Where(x => x.Location == pLocationName);

        Debug.Log("In DataService.GetSessionLocationItems");

        var q = _connection.Query<ItemDTO>(
            "select * from ItemDTO inner join SessionItemDTO"
            + " on ItemDTO.Name = SessionItemDTO.ItemName where SessionItemDTO.Location = ?", pLocationName);


        return q;
    }

    public IEnumerable<ItemDTO> GetSessionLocationItems(int pSessionId, string pLocationName)
    {
        //_connection.Table<SessionItemDTO>().Join<ItemDTO>().Where(x => x.Location)
        //return _connection.Table<ItemDTO>().Where(x => x.Location == pLocationName);

        Debug.Log("In DataService.GetSessionLocationItems2");

        var q = _connection.Query<ItemDTO>(
            "select * from ItemDTO inner join SessionItemDTO"
            + " on ItemDTO.Name = SessionItemDTO.ItemName where SessionItemDTO.Location = ?"
            + " and SessionItemDTO.SessionId = ?", pLocationName, pSessionId);

        return q;
    }

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
                Name = item.name,
                Description = item.description
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



        //SessionDTO temp = q;


        //return temp.Id;
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
