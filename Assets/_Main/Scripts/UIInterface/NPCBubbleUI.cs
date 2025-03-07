using UnityEngine;
using TMPro;

public class NPCBubbleUI : MonoBehaviour
{
    public TMP_Text bubbleText;                // Text untuk pesan NPC
    public SpriteRenderer bubbleSprite;       // Sprite untuk bubble
    public Vector2 padding = new Vector2(0.5f, 0.5f); // Padding untuk bubble (X: lebar, Y: tinggi)

    public void SetBubbleText(string message)
    {
        bubbleText.text = message;
        ResizeBubble();
    }

    private void ResizeBubble()
    {
        if (bubbleSprite != null && bubbleText != null)
        {
            Vector2 textSize = new Vector2(bubbleText.preferredWidth, bubbleText.preferredHeight);

            // Resize sprite mengikuti teks + padding
            bubbleSprite.size = textSize + padding;
        }
    }
}
