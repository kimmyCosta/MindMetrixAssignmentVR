using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameDataRecap", menuName = "DataStruct/GameDataRecap")]

public class GameDataRecap : ScriptableObject
{
    //public PlayerDataRecap playerDataRecap;
    public int countEnemy;
    public int countEnemyMissed;
    public string endGameTime;
    
}
