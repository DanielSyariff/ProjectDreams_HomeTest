using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] private float duration;

    private void Start()
    {
        Destroy(this.gameObject, duration);
    }
}
