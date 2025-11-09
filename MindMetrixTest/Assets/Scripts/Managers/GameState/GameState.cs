using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameState : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private List<SO_SpawnableInfo> spawnInfo = new List<SO_SpawnableInfo>();
    [SerializeField] private GameObject defaultInteractable;
    [SerializeField] private int numberEnemyInLevel = 5;

    private List<PlayerState> playerStates = new List<PlayerState>(); //structured already for possible multiplayers

    private int countEnemy;
    private int countEnemyMissed;

    private BackendHandler backendHandler;

    void Start()
    {
        backendHandler = this.GetComponent<BackendHandler>();
        StartCoroutine(SpawnEnemy(0f));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemy(float delay)
    {
        yield return new WaitForSeconds(delay);
        int enemyType = UnityEngine.Random.Range(0,spawnInfo.Count);
        Vector3 randomPos = new Vector3((float)UnityEngine.Random.Range(-20, 21), 0f, (float)UnityEngine.Random.Range(10, 51));
        SO_SpawnableInfo enemySpawned = spawnInfo[enemyType];
        GameObject spawnedEnemy = Instantiate(defaultInteractable, randomPos, Quaternion.Euler(0, 0, 0));
        MB_InfoSpawnable infoSpawnable = spawnedEnemy.GetComponent<MB_InfoSpawnable>();
        infoSpawnable.setSpawnInfo(enemySpawned);
    }

    public List<PlayerState> GetListPlayers()
    {
        return playerStates;
    }

    public void AddPlayer(PlayerState playerState)
    {
        playerStates.Add(playerState);
    }

    public void RemovePlayer(PlayerState playerState)
    {
        playerStates.Remove(playerState);
    }


    public void IncrementEnemyCount()
    {
        countEnemy++;
        if (countEnemy >= numberEnemyInLevel)
        {
            for (int i = 0; i < playerStates.Count; i++) //structured already for possible multiplayers
            {
                RecapGame(playerStates[i]);
            }
        }
        else
        {
            StartCoroutine(SpawnEnemy(2f));
        }
    }

    /**** following portion handles Scriptable object to combine data and send it to the backenHandler ***/

    //called when the player clicks but without touching a target
    public void RegisterClick(PlayerDataEvent playerDataEvent)
    {
        EnemyDataEvent enemyData = ScriptableObject.CreateInstance<EnemyDataEvent>();
        enemyData.enemyState = EEnemyState.NotTouched.ToString();
        enemyData.timeIntervalShot = -1f;


        backendHandler.RegisterDataEvent(playerDataEvent, enemyData);

    }

    //called when player clicked and touched a target
    public void RegisterClickAndEnemy(PlayerDataEvent playerDataEvent, EnemyDataEvent enemyData)
    {
        backendHandler.RegisterDataEvent(playerDataEvent, enemyData);
         
        IncrementEnemyCount();


    }

    // called when target was not touched and it got self destructed. The missing target is concerning every player (in the case of a multiplayer session)
    public void RegisterEnemy(EnemyDataEvent enemyData)
    {

        for (int i = 0; i < playerStates.Count; i++)
        {
            backendHandler.RegisterDataEvent(playerStates[i].RegisterCurrentPlayerData(), enemyData);
            playerStates[i].HasForgotTarget();

            
        }
        
        countEnemyMissed++;
        IncrementEnemyCount();

    }

    //after number of enemies spawned for the level, ends game and aggregate recap
    public void RecapGame(PlayerState playerState)
    {
        GameDataRecap gameDataRecap = ScriptableObject.CreateInstance<GameDataRecap>();
        gameDataRecap.countEnemy = countEnemy;
        gameDataRecap.countEnemyMissed = countEnemyMissed;
        gameDataRecap.endGameTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss:ff");

        backendHandler.ConcatanateTwoJsonAndPost(JsonUtility.ToJson(playerState.RegisterRecapSession()), JsonUtility.ToJson(gameDataRecap), false);

    }


}
