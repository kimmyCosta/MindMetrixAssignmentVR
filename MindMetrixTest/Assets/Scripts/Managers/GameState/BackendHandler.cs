using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class BackendHandler : MonoBehaviour
{
    [SerializeField] private string url = "http://localhost:5000";
    [SerializeField] private string endpoint = "/api/reaction";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterDataEvent(PlayerDataEvent playerDataEvent, EnemyDataEvent enemyData)
    {
        ConcatanateTwoJsonAndPost(JsonUtility.ToJson(playerDataEvent), JsonUtility.ToJson(enemyData), true);
        
        //return ConcatanateTwoJson(JsonUtility.ToJson(playerDataEvent), JsonUtility.ToJson(enemyData));
    }

    public void ConcatanateTwoJsonAndPost(string json1, string json2, bool isEvent)
    {
        string finalEndpoint;

        string js1 = json1;
        string js2 = json2;

        js1 = js1.Remove(js1.Length - 1);
        js2 = js2.Substring(1);

        string json = js1 + ',' + js2;
        Debug.Log(json);
        if (isEvent)
        {
            finalEndpoint = "/event";
        }
        else
        {
            finalEndpoint = "/recap";
        }
        StartCoroutine(PostData(url + endpoint + finalEndpoint, json));
        //return js1 + ',' + js2;
    }

    IEnumerator PostData(string url, string json)
    {

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
                Debug.Log("Upload complete! Server response: " + request.downloadHandler.text);
            else
                Debug.LogError("Error: " + request.error);
        }
    }
}
