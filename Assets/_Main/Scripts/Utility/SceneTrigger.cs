using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string sceneToLoad;
    [SerializeField] private bool loadAdditively = false;

    [Header("Trigger Settings")]
    [SerializeField] private LayerMask playerLayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((playerLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            LoadScene();
        }
    }

    private void LoadScene()
    {
        if (string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.LogWarning("Scene name is empty! Assign a scene name in the inspector.");
            return;
        }

        if (loadAdditively)
        {
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
            Debug.Log($"Loading scene additively: {sceneToLoad}");
        }
        else
        {
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
            Debug.Log($"Loading scene: {sceneToLoad}");
        }
    }
}
