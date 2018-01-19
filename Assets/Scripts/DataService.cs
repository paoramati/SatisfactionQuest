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

        string DatabaseName = "SatisfactionQuestDB";

#if UNITY_EDITOR
        var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            //while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Final PATH: " + dbPath);

        CreateIfNotExists<PlayerDTO>();
        CreateIfNotExists<LocationDTO>();
        CreateIfNotExists<ExitDTO>();
        CreateIfNotExists<ItemDTO>();
        CreateIfNotExists<SessionDTO>();

    }

    public void DeleteDatabaseFile()
    {
        File.Delete("SatisfactionQuestDB");
    }

    public void Connect()
    {
        _connection = new SQLiteConnection(currentDbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
    }

    //create DTO table if it does not exist
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
     * Creates a new game session, returns sessionId 
     */
    public int CreateGameSession(string pUsername)
    {
        SessionDTO sessionDTO = new SessionDTO
        {
            Name_Player1 = pUsername,
            Location_Player1 = Location.NAME.TOMB.ToString()
        };
        SetSession(sessionDTO);
        //_connection.Insert(sessionDTO);

        var q = _connection.Query<SessionDTO>(
            "select Id from SessionDTO"
            ).LastOrDefault();
        return q.Id;
    }

    public void SaveSession()
    {
        var session = GameManager.instance;

        SessionDTO sessionDTO = new SessionDTO
        {
            Id = session.Id,
            Name_Player1 = session.Player1.username,
            Location_Player1 = session.gameModel.currentLocation.name,
            Esteem_Player1 = session.Player1.esteem,
            Score_Player1 = session.Player1.score
            //Name_Player2 = session.player2.username,
            //Location_Player2 = session.gameModel.currentLocation.name,     
            //Esteem_Player1 = session.player1.esteem,
            //Score_Player1 = session.player1.score
        };
        SetSession(sessionDTO);
    }

    public void LoadSession()
    {

    }

    /**
    * Save / create locations from the GameModel location map, including their exits and items 
    * NB: Only used to create the game model, as these values are currently static
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

    /**
     *  Load location and exits from database to populate game model.
     *  NB: 
     */
    public void LoadLocations()
    {
        foreach (var locationDTO in GetLocations())
        {
            GameManager.instance.gameModel.LoadWorldLocation(locationDTO);
            //GameManager.instance.gameModel.LoadWorldLocation(locationDTO.Name, locationDTO.Description, locationDTO.Background);
        }

        foreach (var exitDTO in GetExits())
        {
            GameManager.instance.gameModel.LoadWorldLocationExit(exitDTO);
            //GameManager.instance.gameModel.LoadWorldLocationExits(exitDTO.FromLocation, exitDTO.Direction, exitDTO.ToLocation);
        }
    }

    //create session items for new game session
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
            SetSessionItem(itemDTO);
        }
    }

    //Save or update db details of all session items for a session
    public void SaveSessionItems()
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
            SetSessionItem(itemDTO);
        }
    }

    //Save or update db details of a single item
    private void SaveSessionItem(int pId, string pKey)
    {
        var item = GameManager.instance.gameModel.worldItems[pKey];

        ItemDTO itemDTO = new ItemDTO
        {
            Id = item.id,
            Description = item.description,
            Location = item.location,
            SecretLetter = item.secretLetter,
            SessionId = item.sessionId
        };
        SetSessionItem(itemDTO);
    }


    public void LoadSessionItems()
    {
        foreach (var item in GetSessionItems(GameManager.instance.Id))
        {

            GameManager.instance.gameModel.LoadWorldItem(item);
        }
    }

    //Update local model of session items from database
    public void UpdateLocalSessionItems()
    {
        foreach (var item in GetSessionItems(GameManager.instance.Id))
        {
            GameManager.instance.gameModel.UpdateWorldItem(item);
        }
    }

    public void UpdateLocalSessionState()
    {
        //GameManager.instance.gameModel

    }

    /**
     * Set methods to perform inserts or updates of records
     */

    private void SetSession(SessionDTO pSessionDTO)
    {
        CreateIfNotExists<SessionDTO>();

        if (SessionExists(pSessionDTO.Id))
        {
            Debug.Log("SetSession update sessionId = " + pSessionDTO.Id);
            _connection.Update(pSessionDTO);
        }
        else
        {
            Debug.Log("SetSession insert sessionId = " + pSessionDTO.Id);

            _connection.Insert(pSessionDTO);
        }
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

    private void SetSessionItem(ItemDTO pItemDTO)
    {
        CreateIfNotExists<ItemDTO>();

        if (SessionItemExists(pItemDTO.SessionId, pItemDTO.Name))
        {
            _connection.Update(pItemDTO);
        }
        else
        {
            _connection.Insert(pItemDTO);
        }
    }

    /**
     * Get methods to retrieve data
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

    /**
     * Exist methods for determining whether records are present
     */

    private bool SessionExists(int pId)
    {
        var y = _connection.Table<ItemDTO>().Where(
                x => x.Id == pId).FirstOrDefault();

        return y != null;
    }

    //detects a previous game session by player username
    public bool PreviousSessionExists(string pUsername)
    {
        var y = _connection.Table<SessionDTO>().Where(x => x.Name_Player1 == pUsername).LastOrDefault();
        return y != null;
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

    private bool SessionItemExists(int pSessionId, string pItemName)
    {
        var y = _connection.Table<ItemDTO>().Where(
                x => x.SessionId == pSessionId && x.Name == pItemName).FirstOrDefault();

        return y != null;
    }
}

//public bool DbExists(string DatabaseName)
//{
//    // Watch out! this method has a side effect
//    bool result = false;

//#if UNITY_EDITOR
//    var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
//    result = File.Exists(dbPath);
//#else
//		// check if file exists in Application.persistentDataPath
//		var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

//		if (!File.Exists(filepath))
//		{
//		result = false;
//		Debug.Log("Database not in Persistent path");
//		// if it doesn't ->
//		// open StreamingAssets directory and load the db ->

//#if UNITY_ANDROID
//		var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
//		while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
//		// then save to Application.persistentDataPath
//		File.WriteAllBytes(filepath, loadDb.bytes);
//#elif UNITY_IOS
//		var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
//		// then save to Application.persistentDataPath
//		File.Copy(loadDb, filepath);
//#elif UNITY_WP8
//		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
//		// then save to Application.persistentDataPath
//		File.Copy(loadDb, filepath);

//#elif UNITY_WINRT
//		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
//		// then save to Application.persistentDataPath
//		File.Copy(loadDb, filepath);
//#else
//		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
//		// then save to Application.persistentDataPath
//		File.Copy(loadDb, filepath);

//#endif

//		Debug.Log("Database written");
//		}

//		var dbPath = filepath;
//#endif

//    currentDbPath = dbPath;
//    Debug.Log("Final PATH: " + dbPath);

//    return result;
//}

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
