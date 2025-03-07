using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject actionPanel;
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;

    public void ShowActionPanel()
    {
        actionPanel.SetActive(true);
    }

    public void HideActionPanel()
    {
        actionPanel.SetActive(false);
    }

    public void ShowGameOver(string message)
    {
        gameOverText.text = message;
        gameOverPanel.SetActive(true);
    }

    public void HideGameOver()
    {
        gameOverPanel.SetActive(false);
    }

    public void OnRetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
