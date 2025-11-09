using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataEvent", menuName = "DataStruct/EnemyDataEvent")]
public class EnemyDataEvent : ScriptableObject
{
    public string enemyState;
    public int pointEarned;
    public float timeIntervalShot;
}
