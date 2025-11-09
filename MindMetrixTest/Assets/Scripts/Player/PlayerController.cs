using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class PlayerController : MonoBehaviour
{
    private PlayerState playerState;
    private Pawn pawn;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire(InputAction.CallbackContext value)
    {
        if (value.performed) {
            bool hasHitATarget=false;
            EnemyDataEvent enemyDataEvent = ScriptableObject.CreateInstance<EnemyDataEvent>();

            double time = value.startTime;

            pawn.OnInteract();
            Vector3 mousePosWorld = pawn.PositionPointer;
            
            Vector3 diretionLine = pawn.PositionEndPointer;
            RaycastHit hit;
            if(Physics.Raycast(mousePosWorld, diretionLine, out hit, 100))
            {
                
                if(hit.collider.tag == "Interactable")
                {
                    hit.collider.GetComponent<MB_Target>().SetTimeParent();
                    enemyDataEvent = hit.collider.gameObject.transform.root.GetComponent<MB_InfoSpawnable>().KillEnemy();
                    hasHitATarget = true;
                }
            }
            float inputValue = playerState.GetInputValue();
            
            PlayerDataEvent playerDataEvent = CreatePlayerDataEvent(inputValue, hasHitATarget, mousePosWorld);
            GameState gameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
            if (hasHitATarget)
            {
                gameState.RegisterClickAndEnemy(playerDataEvent, enemyDataEvent);
                playerState.HasClick(hasHitATarget, enemyDataEvent.enemyState);
            }
            else
            {
                playerState.HasClick(hasHitATarget, EEnemyState.NotTouched.ToString());
                gameState.RegisterClick(playerDataEvent);
            }
   
        }

    }

    public PlayerState GetPlayerState()
    {
        return playerState;
    }

    public Pawn GetPawn()
    {
        return pawn;
    }

    public void SetPawn(Pawn pawn)
    {
        this.pawn = pawn;
    }

    public void SetPlayerState(PlayerState playerState)
    {
        this.playerState = playerState;
    }

    private PlayerDataEvent CreatePlayerDataEvent(float inputValue, bool hasHitTarget, Vector3 positionPointer)
    {
        PlayerDataEvent playerDataEvent = ScriptableObject.CreateInstance<PlayerDataEvent>();
        playerDataEvent.username = playerState.GetUsername(); 
        playerDataEvent.inputValue = inputValue;
        playerDataEvent.hasHitTarget = hasHitTarget;
        playerDataEvent.positionPointer = positionPointer;
        playerDataEvent.actionTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss:ff");

        return playerDataEvent;
    }

}
