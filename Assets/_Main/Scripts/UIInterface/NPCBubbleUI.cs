using UnityEngine;
using TMPro;

public class NPCBubbleUI : MonoBehaviour
{
    public TMP_Text bubbleText;                
    public SpriteRenderer bubbleSprite;       
    public Vector2 padding = new Vector2(0.5f, 0.5f); 

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

            bubbleSprite.size = textSize + padding;
        }
    }
}
