using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public TMP_InputField nameInput;
    public Button startButton;

    public int highScore;
    public string highScorePlayer;

    public string currentPlayer;

    private SaveData highScoreRecord;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public int savedHighScore;
        public string savedHighScorePlayer;
    }

    public void OnStartGame()
    {
        // Load the saved high score
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            highScoreRecord = JsonUtility.FromJson<SaveData>(json);
            highScore = highScoreRecord.savedHighScore;
            highScorePlayer = highScoreRecord.savedHighScorePlayer;
        }
        currentPlayer = nameInput.text;
        SceneManager.LoadScene("main");

    }

    public void SaveNewHighScore(int score)
    {

        SaveData newHighScoreRecord = new SaveData();
        newHighScoreRecord.savedHighScore = score;
        newHighScoreRecord.savedHighScorePlayer = currentPlayer;

        string path = Application.persistentDataPath + "/savefile.json";
        string json = JsonUtility.ToJson(newHighScoreRecord);

        File.WriteAllText(path, json);

    }
}
