using System;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataRecap", menuName = "DataStruct/PlayerDataRecap")]
public class PlayerDataRecap : ScriptableObject
{
    public string startPlayerGame;
    public string username;
    public int totalEventsTriggered;
    public int numberTotalClick;
    public int numberPerfectKill;
    public int numberOkKill;
    public int numberMissed;
    public float avgInputValue;
}
