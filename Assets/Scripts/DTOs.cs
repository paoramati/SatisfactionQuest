using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

/*
 * DTO of a location. 
 * NB: DTO does not need regular updating, except if game model is being varied by developer
 */

public class LocationDTO
{
    [PrimaryKey]
    public string Name { get; set; }
    //public int Id { get; set; }
    public string Description { get; set; }
    public string Background { get; set; }
}

/*
 * DTO of a location exit. 
 * NB: DTO does not need regular updating, except if game model is being varied by developer
 */

public class ExitDTO
{
    [PrimaryKey]
    public int Id { get; set; }
    public int Direction { get; set; }
    public string FromLocation { get; set; }
    public string ToLocation { get; set; }
}

/*
 * DTO of an item. 
 * NB: DTO does not need regular updating, except if game model is being varied by developer
 */

public class ItemDTO
{
    [PrimaryKey,AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public string SecretLetter { get; set; }
    public int SessionId { get; set; }
}

/*
 * DTO of a player. 
 * NB: DTO does not need regular updating, except if game model is being varied by developer
 */

public class PlayerDTO
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}

/*
 * DTO of a game session. Holds player details particular to that session
 * PK of sessionDTO is used as virtual FK in other session tables
 */

public class SessionDTO
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name_Player1 { get; set; }
    public string Name_Player2 { get; set; }
    public string Location_Player1 { get; set; }
    public string Location_Player2 { get; set; }
    public int Esteem_Player1 { get; set; }
    public int Esteem_Player2 { get; set; }
    public int Score_Player1 { get; set; }
    public int Score_Player2 { get; set; }
}

/*
 * DTO of an item's details that are retained for each individual game session.
 */

//public class SessionItemDTO
//{
//    [PrimaryKey,AutoIncrement]
//    public int Id { get; set; }
//    public int ItemId { get; set; }         //Id of the item. Virtual FK of ItemDTO
//    public string ItemName { get; set; }    //Name of the item. Virtal FK of ItemDTO
//    public int SessionId { get; set; }      //Id of the session. Virtual FK of SessionDTO
//    public string Location { get; set; }    //either a location name, or a player username - this will not work if username same as a location name!
//}




//public class PlayerInventoryDTO
//{
//    [PrimaryKey, AutoIncrement]
//    public int Id { get; set; }
//}