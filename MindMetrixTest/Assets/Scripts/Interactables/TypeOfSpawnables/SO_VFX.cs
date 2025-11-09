using UnityEngine;

[CreateAssetMenu(fileName = "SO_VFX", menuName = "Scriptable Objects/SO_VFX")]
public class SO_VFX : ScriptableObject
{
    [SerializeField] public GameObject perfect;
    [SerializeField] public GameObject ok;
    [SerializeField] public GameObject lose;
}
