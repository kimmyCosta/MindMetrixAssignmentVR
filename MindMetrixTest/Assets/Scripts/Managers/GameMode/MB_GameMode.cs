using UnityEngine;

public class MB_GameMode : MonoBehaviour
{
    [SerializeField] private SO_GameMode gameMode;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject playerControllerGO = Instantiate(gameMode.playerController);
        PlayerController playerController = playerControllerGO.GetComponent<PlayerController>();
        Debug.Log("playerController is null " + (playerController == null));

        GameObject playerStateGO = Instantiate(gameMode.playerState);
        PlayerState playerState = playerStateGO.GetComponent<PlayerState>();
        Debug.Log("playerState is null " + (playerState == null));

        GameObject pawnGO = Instantiate(gameMode.pawn);
        Pawn pawn = pawnGO.GetComponent<Pawn>();

        GameObject gameStateGO = Instantiate(gameMode.gameState);
        GameState gameState = gameStateGO.GetComponent<GameState>();

        playerController.SetPawn(pawn);
        playerController.SetPlayerState(playerState);

        playerState.SetPawn(pawn);
        playerState.SetPlayerController(playerController);

        pawn.SetPlayerController(playerController);
        pawn.SetPlayerState(playerState);

        gameState.AddPlayer(playerState);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
