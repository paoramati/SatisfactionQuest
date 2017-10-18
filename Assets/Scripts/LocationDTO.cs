using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

public class LocationDTO
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Background { get; set; }
}

public class ExitDTO
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string FromName { get; set; }
    public int Direction { get; set; }
    public string ToName { get; set; }
}

/**
 * DTO of an item. 
 */

public class ItemDTO
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }    //An Item's location is either player1, player2, or the virtual FK name of location
    public string Description { get; set; }
}
