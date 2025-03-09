using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardUIManager : MonoBehaviour
{
    public GameObject rewardItemPrefab;  
    public Transform rewardPanel;        

    public void ShowReward(RewardData rewardData)
    {
        GameObject rewardItem = Instantiate(rewardItemPrefab, rewardPanel);

        Image icon = rewardItem.transform.Find("Reward icon").GetComponent<Image>();
        TextMeshProUGUI rewardText = rewardItem.transform.Find("Reward Name").GetComponent<TextMeshProUGUI>();

        icon.sprite = rewardData.rewardIcon;
        rewardText.text = $"{rewardData.rewardValue}x {rewardData.rewardName}";

        Destroy(rewardItem, 3f);
    }
}
