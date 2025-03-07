using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerController : MonoBehaviour
{

    private static SceneManagerController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Fungsi untuk pindah ke scene tertentu (Single Mode)
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    // Fungsi untuk load scene secara Additive
    public void LoadSceneAdditive(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    // Fungsi untuk unload scene yang tidak dibutuhkan
    public void UnloadScene(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
        else
        {
            Debug.LogWarning($"Scene '{sceneName}' belum diload.");
        }
    }

    // Fungsi untuk reload scene saat ini
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Fungsi untuk pindah ke scene berikutnya
    public void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("Sudah di scene terakhir!");
        }
    }

    // Fungsi untuk keluar dari game
    public void QuitGame()
    {
        Debug.Log("Keluar dari game...");
        Application.Quit();
    }
}
