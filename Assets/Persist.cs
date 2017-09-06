using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Persist : MonoBehaviour
{
    public static Persist control;
    public float Health;
    public float Experience;

    // Use this for initialization
    void Start()
    {
        // PLAYER PREFS
        // PlayerPrefs.SetInt("health",21);
        //int health = PlayerPrefs.GetInt("health");

        // DontDestroyOnLoad
        //DontDestroyOnLoad(gameObject);

    }

    // Now there can be only one of
    void Awake()
    {
        if (control == null)
        {
            try
            {
                Load();
            }
            catch (Exception e) { }




            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }

    }

    // Serialisation

    // Unity Serialisation

    // Update is called once per frame
    //void Update () {

    //}

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();                                             //serializaing data into the file
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");      //

        //Using the 
        PlayerData data = new PlayerData();
        data.health = Health;
        data.experience = Experience;
        bf.Serialize(file, data);                //using the bf, we are serializaing data transfer to the file from the player data
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            Health = data.health;
            Experience = data.experience;
        }
    }
}

[Serializable]
class PlayerData
{
    public float health;
    public float experience;
}