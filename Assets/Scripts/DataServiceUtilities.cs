using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataServiceUtilities
{

    // Use this for initialization
    public static void Save()
    {
        DataService _connection = new DataService();
        //if (_connection.DbExists("GameNameDb"))
        //{
        //_connection.Connect();
        _connection.SaveLocations();
        _connection.SaveItems();
    }

    public static void DeleteDatabase()
    {
        DataService _connection = new DataService();
        _connection.DeleteDatabaseFile();
    }

    public static void CreateNewGame()
    {
        DataService _connection = new DataService();

        //DataService.instance.
        _connection.Connect();


    }





}