using UnityEngine;

[CreateAssetMenu(fileName = "New Reward", menuName = "Rewards/Reward")]
public class EnemyLineupData
{
    [Header("Reward Info")]
    public int maxHp;
    public int speed;
    public Animation enemyAnimation;
}
