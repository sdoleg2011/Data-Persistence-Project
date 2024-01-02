using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_InputField input;
    [SerializeField] TextMeshProUGUI bestScoreText;


    public void Start()
    {
        GameManager.Instance.LoadScore();
        bestScoreText.text = $"Best score: {GameManager.Instance.bestPlayerName} : {GameManager.Instance.highScore}";
    }

    public void LoadGame()
    {
        GameManager.Instance.playerName = input.text;
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        GameManager.Instance.SaveScore();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
