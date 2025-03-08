using UnityEngine;
using DG.Tweening;
using TMPro;
public class WorldUIManager : MonoBehaviour
{
    public static WorldUIManager instance;

    public bool onPause;

    [Header("Panel")]
    public RectTransform combatTriggerPanel;
    public TextMeshProUGUI combatText;

    public float slideDuration = 0.5f;
    public float startXOffset = -800f; // Posisi awal di luar layar
    public float endXPosition = 0f;    // Posisi akhir (di tengah layar)

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        combatTriggerPanel.anchoredPosition = new Vector2(startXOffset, combatTriggerPanel.anchoredPosition.y);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowCombatTrigger("YAHAHA");
        }
    }
    public void ShowCombatTrigger(string text)
    {
        onPause = true;
        combatTriggerPanel.gameObject.SetActive(true);
        combatTriggerPanel.DOAnchorPosX(endXPosition, slideDuration).SetEase(Ease.OutQuad);
        combatText.text = text;
    }
}
