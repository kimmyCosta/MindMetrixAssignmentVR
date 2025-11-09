using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class PlayerState : MonoBehaviour
{
    private Pawn pawn;
    private PlayerController playerController;

    [SerializeField] private AnimationCurve inputCurve;

    private int countEvent;

    private int countMissedClicked;

    private float sumInput;

    private string startPlayerGame;
    private string username = "Test";

    private int countPerfectKill;
    private int countOkKill;
    private int countTotalClicK;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPlayerGame = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss:ff");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HasClick(bool hasHitTarget, string enemyState)
    {
        if (hasHitTarget)
        {
            
            switch (Enum.Parse(typeof(EEnemyState), enemyState))
            {
                case EEnemyState.PerfectKill:
                    countPerfectKill++;
                    break;
                case EEnemyState.OkKill:
                    countOkKill++;
                    break;
                
            }
        }
        else { 
            countMissedClicked++; 
        }
            countEvent++;
            countTotalClicK++;

        }

    public void HasForgotTarget()
    {
        countEvent++;
        sumInput+= GetInputValue();
    }

    public float GetInputValue() { 
    
        return inputCurve.Evaluate(Time.time);
    }

    public float GetCurrentTimePlayer()
    {
        return Time.time;
    }

    public PlayerController GetPlayerController()
    {
        return playerController;
    }

    public Pawn GetPawn()
    {
        return pawn;
    }

    public void SetPawn(Pawn pawn)
    {
        this.pawn = pawn;
    }

    public void SetPlayerController(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public string GetUsername()
    {
        return username;
    }

    public void SetUsername(string username)
    {
        this.username = username;
    }

    private PlayerDataRecap CreateRecapSession(string startPlayerGame, string username, int totalEventsTriggered, int numberTotalClick, int numberPerfectKill, int numberOkKill, int numberMissed, float sumInputValue)
    {
        PlayerDataRecap playerDataRecap = ScriptableObject.CreateInstance<PlayerDataRecap>();
        playerDataRecap.startPlayerGame = startPlayerGame;
        playerDataRecap.username = username;
        playerDataRecap.totalEventsTriggered = totalEventsTriggered;
        playerDataRecap.numberTotalClick = numberTotalClick;
        playerDataRecap.numberPerfectKill = numberPerfectKill;
        playerDataRecap.numberOkKill = numberOkKill;
        playerDataRecap.numberMissed = numberMissed;
        Debug.Log("sumInputValue " + sumInputValue);
        playerDataRecap.avgInputValue = sumInputValue / (float)totalEventsTriggered;

        return playerDataRecap;
    }

    public PlayerDataRecap RegisterRecapSession()
    {
        PlayerDataRecap playerDataRecap = CreateRecapSession(startPlayerGame, username, countEvent, countTotalClicK, countPerfectKill, countOkKill, countMissedClicked, sumInput);

        return playerDataRecap;
    }

    public PlayerDataEvent RegisterCurrentPlayerData()
    {
        float inputVal = GetInputValue();

        PlayerDataEvent playerDataEvent = ScriptableObject.CreateInstance<PlayerDataEvent>();
        playerDataEvent.username = username;
        playerDataEvent.inputValue = inputVal;
        playerDataEvent.hasHitTarget = false;
        playerDataEvent.positionPointer = pawn.PositionPointer;
        playerDataEvent.actionTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss:ff");

        sumInput += inputVal;

        return playerDataEvent;
    }


}
