using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField]
    private Button _restart;
    [SerializeField]
    private Button _exit;
    [SerializeField]
    private Button _closePanel;

    private void Awake()
    {
        _restart.onClick.AddListener(RestartGame);
        _exit.onClick.AddListener(ExitGame);
        _closePanel.onClick.AddListener(ClosePanel);
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    private void ClosePanel()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}