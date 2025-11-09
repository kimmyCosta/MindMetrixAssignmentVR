using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataEvent", menuName = "DataStruct/PlayerDataEvent")]
public class PlayerDataEvent : ScriptableObject
{
    public string username;
    public float inputValue;
    public bool hasHitTarget;
    public Vector3 positionPointer;
    public string actionTime;
}
