using UnityEngine;

public class MB_Target : MonoBehaviour
{
    [SerializeField] private Material materialPerfect;
    private float deadTime;
    private float okTime;
    private bool isDeadTimeSet;

    private bool isFirstColor = true;
    private bool isAlive = true;


    private float time;
    private Vector3 startScale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = 0f;
        startScale = this.gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDeadTimeSet)
        {
            time += Time.deltaTime;
            if (time < deadTime)
            {
                if (time >= okTime && isFirstColor) 
                {
                    this.gameObject.GetComponent<MeshRenderer>().material = materialPerfect;
                    isFirstColor = false;
                }
                this.gameObject.transform.localScale = startScale * (float)(1f - (time / deadTime));
            }
            else
            {
                if (isAlive) {
                    KillSpawn();
                    isAlive = false;
                }
            }
        }
        
        
    }

    public void KillSpawn()
    {
        MB_InfoSpawnable parentScript = this.gameObject.transform.root.GetComponent<MB_InfoSpawnable>();
        parentScript.SetIsSelfDestructed(true);
        parentScript.SetTime(time);
        //parentScript.KillEnemy();
        parentScript.SelfDestroyed();
        //Destroy(this.gameObject.transform.root.gameObject);
    }

    public void SetTimeParent()
    {
        this.gameObject.transform.root.GetComponent<MB_InfoSpawnable>().SetTime(time);
    }
   
    public void SetTimeInfos(float deadTime, float timeOk)
    {
        this.deadTime = deadTime;
        okTime = timeOk;

        isDeadTimeSet = true;
    }

        
}
