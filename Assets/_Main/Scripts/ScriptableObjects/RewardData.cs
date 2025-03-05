using UnityEngine;

[CreateAssetMenu(fileName = "New Reward", menuName = "Rewards/Reward")]
public class RewardData : ScriptableObject
{
    [Header("Reward Info")]
    public string rewardName;
    public Sprite rewardIcon;
    public GameObject rewardPrefab;
    public int rewardValue;

    [TextArea]
    public string rewardDescription;
}
