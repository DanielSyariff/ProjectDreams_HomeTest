using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [Header("Reward Settings")]
    [SerializeField] private RewardData reward;  // Assign reward lewat Inspector

    private bool isOpened = false;

    public void Interact()
    {
        if (!isOpened)
        {
            OpenChest();
        }
        else
        {
            Debug.Log("Chest already opened!");
        }
    }

    private void OpenChest()
    {
        isOpened = true;
        Debug.Log($"Chest Opened! You received: {reward.rewardName} (Value: {reward.rewardValue})");

        ShowRewardUI();

        if (reward.rewardPrefab != null)
        {
            Instantiate(reward.rewardPrefab, transform.position, Quaternion.identity);
        }
    }

    private void ShowRewardUI()
    {
        Debug.Log($"Showing reward UI: {reward.rewardName}");
    }
}
