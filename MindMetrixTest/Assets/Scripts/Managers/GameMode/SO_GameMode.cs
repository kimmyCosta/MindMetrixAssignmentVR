using UnityEngine;

[CreateAssetMenu(fileName = "SO_GameMode", menuName = "Modes/GameMode")]
public class SO_GameMode : ScriptableObject
{
    [SerializeField] public GameObject gameState;
    [SerializeField] public GameObject playerController;
    [SerializeField] public GameObject playerState;
    [SerializeField] public GameObject pawn;
}
