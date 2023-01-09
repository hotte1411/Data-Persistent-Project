using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public string playerName;
    public string bestPlayerName;
    public int bestScore;

    void Awake()
    {
        LoadAlldata();
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveAllData()
    {
        SaveData data = new SaveData();
        SaveBestScore(data);
        SaveCurrentPlayerName(data);

        string json = JsonUtility.ToJson(data);
        string path = $"{Application.persistentDataPath}/savedata.json";
        File.WriteAllText(path, json);
    }

    public void LoadAlldata()
    {
        string path = $"{Application.persistentDataPath}/savedata.json";
        if (File.Exists(path))
        {
            SaveData data = new SaveData();
            LoadBestScore(path, data);
            bestPlayerName = data.bestPlayerName;
            bestScore = data.bestScore;
            LoadCurrentPlayerName(path, data);
            playerName = data.playerName;
        }
    }

    public void SaveBestScore(SaveData data) 
    {
        data.bestPlayerName = bestPlayerName;
        data.bestScore = bestScore;
    }    

    public void SaveCurrentPlayerName(SaveData data)
    {
        data.playerName = playerName;
    }

    public void LoadBestScore(string path, SaveData data)
    {
        string json = File.ReadAllText(path);
        data.bestScore = JsonUtility.FromJson<SaveData>(json).bestScore;
        data.bestPlayerName = JsonUtility.FromJson<SaveData>(json).bestPlayerName;
    }

    public void LoadCurrentPlayerName(string path, SaveData data)
    {
        string json = File.ReadAllText(path);
        data.playerName = JsonUtility.FromJson<SaveData>(json).playerName;
    }

    [System.Serializable]
    public class SaveData
    {
        public string playerName;
        public string bestPlayerName;
        public int bestScore;
    }
}
