using UnityEngine;

[CreateAssetMenu(fileName = "SO_Sound", menuName = "Scriptable Objects/SO_Sound")]
public class SO_Sound : ScriptableObject
{
    [SerializeField] public AudioClip popupSound;
    [SerializeField] public AudioClip okSound;
    [SerializeField] public AudioClip deadSound;
    [SerializeField] public AudioClip perfectSound;
}
