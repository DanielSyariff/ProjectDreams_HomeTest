using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemies/Enemy")]

[Serializable]
public class Lineup
{
    public string name;
    public int maxHp;
    public int speed;
    public int damage;
    public RuntimeAnimatorController animator;
}
public class EnemyLineupData : ScriptableObject
{
    [Header("Enemy Info")]
    public List<Lineup> lineup;
}
