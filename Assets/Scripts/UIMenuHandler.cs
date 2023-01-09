using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIMenuHandler : MonoBehaviour
{
    public TMP_InputField inputNameField;
    public TMP_Text bestScoreText;


    void Start()
    {
        bestScoreText.text = $"Best score: \n{DataManager.Instance.bestPlayerName}: {DataManager.Instance.bestScore}";
        inputNameField.text = DataManager.Instance.playerName;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        DataManager.Instance.SaveAllData();
        EditorApplication.ExitPlaymode();
#else
        DataManager.Instance.LoadAlldata();
        Application.Quit();
#endif
    }

    public void SetPlayerName()
    {
        DataManager.Instance.playerName = inputNameField.GetComponent<TMP_InputField>().text;
    }
}
