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
    public TMP_Text showHighScore;

    public int highScore;
    public string highScorePlayer;
    public int sessionHighScore;
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

    private void Start()
    {
        // Load the saved high score
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            highScoreRecord = JsonUtility.FromJson<SaveData>(json);
            highScore = highScoreRecord.savedHighScore;
            highScorePlayer = highScoreRecord.savedHighScorePlayer;
            showHighScore.text = $"Current high score ({highScorePlayer}): {highScore.ToString()}";

        }
    }
    public void OnStartGame()
    {
        // Make sure the user entered a name
        if (nameInput.text.Length > 0)
        {
            currentPlayer = nameInput.text;
            sessionHighScore = 0;
            SceneManager.LoadScene("main");
        }
        
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

    public void ResetHighScore()
    {
        // Reset the save file

        SaveData newHighScoreRecord = new SaveData();
        newHighScoreRecord.savedHighScore = 0;
        newHighScoreRecord.savedHighScorePlayer = "New";

        string path = Application.persistentDataPath + "/savefile.json";
        string json = JsonUtility.ToJson(newHighScoreRecord);

        File.WriteAllText(path, json);

        // Reset the current UI
        highScore = 0;
        highScorePlayer = "New";

        showHighScore.text = $"Current high score ({highScorePlayer}): {highScore.ToString()}";

    }
}
