using UnityEngine;

[CreateAssetMenu(fileName = "SO_SpawnableInfo", menuName = "Scriptable Objects/SO_SpawnableInfo")]
public class SO_SpawnableInfo : ScriptableObject
{
    [SerializeField] public GameObject enemy;
    [SerializeField] public float deadTime;
    [SerializeField] public float okTime;
    [SerializeField] public int point;
    [SerializeField] public SO_Sound sound;
    [SerializeField] public SO_VFX vfx;


}
