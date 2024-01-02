using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public TextMeshProUGUI userName;
    public TMP_InputField userInputField;

    public TextMeshProUGUI bestScoreText;

    private int savedScore;
    private string savedName;
    public string playerName;

    public string GetPlayerName
    {
        get { return playerName; }
        private set { playerName = value; }
    }

    public int GetSavedScore { get { return savedScore; } }
    public string GetSavedName { get { return savedName; } }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadInfo();
    }


    public void StartGame()
    {
        SetPlayerName(userInputField.text);
        //userName.text = userInputField.text;
        SceneManager.LoadScene(1);
    }

    public void QuidGame()
    {
        Application.Quit();
    }

    [System.Serializable]
    class SaveData
    {
        public int _score;
        public string _name;
    }

    public void SaveInfo(int score, string name)
    {
        SaveData data = new SaveData();
        if (data._score < savedScore)
        {
            data._score = score;
            data._name = name;
        }
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadInfo()
    {

        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);


            savedScore = data._score;
            savedName = data._name;

            bestScoreText.text = $"Best Score: {savedName} : {savedScore}";
        }
    }

    public bool UpdateBestScore(int newScore)
    {
        if (savedScore <= newScore)
        {
            savedScore = newScore;
            savedName = userName.text;
            return true;
        }
        return false;
    }
    public string ShowInfo()
    {
        return $"Best Score: {userName.text} : {savedScore}";
    }
    public void SetPlayerName(string newPlayerName)
    {
        playerName = newPlayerName;
    }
}