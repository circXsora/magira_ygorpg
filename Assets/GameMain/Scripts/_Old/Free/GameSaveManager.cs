using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    public InventroyObject InventroyObject;
    private void Start()
    {
        folder_path = Application.persistentDataPath + "/game_SaveData";
        file_path = Application.persistentDataPath + "/game_SaveData/inventory.txt";
        LoadGame();
    }
    string folder_path;
    string file_path;


    public void SaveGame()
    {
        string json = JsonUtility.ToJson(InventroyObject);

        StreamWriter writer = new StreamWriter(file_path);

        try
        {
            writer.Write(json);
            writer.Flush();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            if (writer != null)
                writer.Dispose();
        }
    }
    public void LoadGame()
    {
#if !UNITY_EDITOR
        if (File.Exists(file_path)) {
            string json = File.ReadAllText(file_path);
 
            try {
                JsonUtility.FromJsonOverwrite(json, InventroyObject);
            }
            catch (Exception e) {
                throw e;
            }
        }
#endif
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SaveGame();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGame();
        }
    }
}
