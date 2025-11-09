using UnityEngine;

public class MB_InfoSpawnable : MonoBehaviour
{

    private int pointWon;

    private bool isSelfDestructed;
    private float time;

    private MB_Target target;


    private SO_SpawnableInfo spawnInfo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetIsSelfDestructed(bool isSelfDestroy)
    {
        isSelfDestructed = isSelfDestroy;
    }

    public bool GetIsSelfDestructed()
    {
        return isSelfDestructed;
    }

    public void SetTime(float t)
    {
        time = t;
    }

    public float GetTime()
    {
        return time;
    }
    public void setSpawnInfo(SO_SpawnableInfo spawnInfo)
    {
        this.spawnInfo = spawnInfo;
        SetEnemy();
        this.gameObject.GetComponent<AudioSource>().resource = spawnInfo.sound.popupSound;
        SetInfoTarget();
    }
    public void SetEnemy()
    {
        GameObject enemyInstance = Instantiate(spawnInfo.enemy, this.transform);


        //Vector3 headPos = enemyInstance.transform.GetChild(0).GetChild(0).GetChild(0).transform.position;
        Transform head = findHead(enemyInstance);
        if (head)
        {
            Vector3 headPos = head.transform.position;
            this.transform.GetChild(0).gameObject.transform.position = new Vector3(headPos.x, headPos.y, headPos.z - 2);
        }
        

    }

    private Transform findHead(GameObject enemyInstance)
    {
        Transform head = enemyInstance.transform.Find("Head");
        if(head)
        {
            return head;
        }else
        {
            for (int i = 0; i < enemyInstance.transform.childCount; i++)
            {
                return findHead(enemyInstance.transform.GetChild(i).gameObject);
            }
        }
        return null;
    }

    public void SetInfoTarget()
    {
        target = this.gameObject.GetComponentInChildren<MB_Target>();
        target.SetTimeInfos(spawnInfo.deadTime, spawnInfo.okTime);
    }

    public void SwitchSoundAndPlay(AudioClip sound, GameObject gameObject)
    {
        AudioSource audio = gameObject.GetComponent<AudioSource>();
        audio.resource = sound;
        audio.Play();
    }

    public float SpawnVFXwithSound(GameObject vfx, AudioClip sound)
    {
        GameObject vfxObject = Instantiate(vfx, this.gameObject.transform.GetChild(0).transform.position, Quaternion.Euler(0,0,0));
        SwitchSoundAndPlay(sound, vfxObject);
        float delayDestroy = vfxObject.GetComponent<ParticleSystem>().main.duration;
        Destroy(this.gameObject, delayDestroy);
        return delayDestroy;
    }

    public EnemyDataEvent KillEnemy()
    {
        EEnemyState enemyState;
        float timeIntervalShot;

        float delayDestroy;
        
        if(!isSelfDestructed)
        {
            if (time >= spawnInfo.okTime)
            {
                //SwitchSoundAndPlay(spawnInfo.sound.perfectSound);
                delayDestroy = SpawnVFXwithSound(spawnInfo.vfx.perfect, spawnInfo.sound.perfectSound);
                pointWon = spawnInfo.point;
                enemyState = EEnemyState.PerfectKill;

            }
            else
            {
                //SwitchSoundAndPlay(spawnInfo.sound.okSound);
                delayDestroy = SpawnVFXwithSound(spawnInfo.vfx.ok, spawnInfo.sound.okSound);
                pointWon = (int)(spawnInfo.point / 2);
                enemyState = EEnemyState.OkKill;
            }

            timeIntervalShot = (spawnInfo.deadTime - time)*1000;
            Destroy(this.gameObject,0.5f);
            Debug.Log("delayDestroy : " + delayDestroy);
            
            //Destroy(this.gameObject);
            
            return CreateEnemyDataEventInfo(enemyState.ToString(), pointWon, timeIntervalShot);
        }
        return null;


        
    }

    public void SelfDestroyed()
    {
        EEnemyState enemyState;
        float timeIntervalShot;

        float delayDestroy;
        if (isSelfDestructed)
        {
            //SwitchSoundAndPlay(spawnInfo.sound.deadSound);
            delayDestroy = SpawnVFXwithSound(spawnInfo.vfx.lose, spawnInfo.sound.deadSound);

            pointWon = (int)(-spawnInfo.point / 2);
            timeIntervalShot = -1f;
            enemyState = EEnemyState.SelfDestroyed;
            timeIntervalShot = spawnInfo.deadTime - time;

            Destroy(this.gameObject, delayDestroy);

            EnemyDataEvent enemyDataEvent = CreateEnemyDataEventInfo(enemyState.ToString(), pointWon, timeIntervalShot);
            GameState gameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();

            gameState.RegisterEnemy(enemyDataEvent);
        }
        
    }

    private EnemyDataEvent CreateEnemyDataEventInfo(string enemyState, int pointEarned, float timeIntervalShot)
    {
        EnemyDataEvent enemyData = ScriptableObject.CreateInstance<EnemyDataEvent>();
        enemyData.enemyState = enemyState.ToString();
        enemyData.pointEarned = pointEarned;
        enemyData.timeIntervalShot = timeIntervalShot;

        return enemyData;
    }
}
