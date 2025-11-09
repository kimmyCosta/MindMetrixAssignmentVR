using UnityEngine;

[CreateAssetMenu(fileName = "SO_GameMode", menuName = "Modes/GameMode")]
public class GameMode : ScriptableObject
{
    [SerializeField] private GameObject gameState;
    [SerializeField] private GameObject playerController;
    [SerializeField] private GameObject playerState;
    [SerializeField] private GameObject pawn;
}
