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

    private void Start()
    {

        LoadPoint();

        // bestScoreText.text = $"Best Score: {savedName} : {savedScore}";
    }


    public void StartGame()
    {
        userName.text = userInputField.text;
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

    public void SavePoint(int score, string name)
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
    public void LoadPoint()
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
}
