using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public string playerName;
    public int highScore;
    public string highScorePlayer;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetHighScore(int score)
    {
        highScore = score;
    }

    public void SetHighScorePlayer(string playerName)
    {
        highScorePlayer = playerName;
    }

    public void SetPlayerName(string playersName)
    {
        playerName = playersName;
    }

    [System.Serializable]
    class SaveData
    {
        public int highScore;
    }
    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.highScore = highScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public int LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScore = data.highScore;

        }else
        {
            highScore = 0;
        }

        return highScore;
    }
}
