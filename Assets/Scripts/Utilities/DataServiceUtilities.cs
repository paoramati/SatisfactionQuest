using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataServiceUtilities
{
    public static void SaveGame()
    {
        DataService dataService = new DataService();

        dataService.SaveSessionItems();
        dataService.SaveSession();
        dataService.UpdateLocalSessionItems();
        dataService.UpdateLocalSessionState();
    }

    public static void RefreshGameSession()
    {
        DataService dataService = new DataService();

        SaveGame();
        dataService.UpdateLocalSessionItems();
        dataService.UpdateLocalSessionState();
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

    public static void DisplayAllItems()
    {
        DataService dataService = new DataService();    //could be replaced by a static object

        foreach (ItemDTO item in dataService.GetItems())
        {
            Debug.Log("Item: Id = " + item.Id + ", Name = " + item.Name + ", Location = " + item.Location + ", Desc. = " + item.Description 
                + ", SessionId = " + item.SessionId + ", secret = " + item.SecretLetter + "\n");
        }
    }

    public static void DisplayAllLocations()
    {
        DataService dataService = new DataService();    //could be replaced by a static object

        foreach (LocationDTO location in dataService.GetLocations())
        {
            Debug.Log("Location: " + location.Name + " - " + location.Description + " - " 
                + location.Background + "\n");
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

        foreach (ItemDTO sessionItem in dataService.GetItems())
        {
            Debug.Log("SessionItem: Id = " + sessionItem.Id + " - itemName = " + sessionItem.Name 
                + " - sessionId = " + sessionItem.SessionId + " - location = " + sessionItem.Location + "\n");
        }
    }
}