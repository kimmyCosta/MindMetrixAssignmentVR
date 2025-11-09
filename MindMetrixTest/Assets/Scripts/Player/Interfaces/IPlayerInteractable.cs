using UnityEngine;

public interface IPlayerInteractable
{

    Vector3 PositionPointer { get; }
    Vector3 PositionEndPointer { get; }
    void OnInteract();

   
}
