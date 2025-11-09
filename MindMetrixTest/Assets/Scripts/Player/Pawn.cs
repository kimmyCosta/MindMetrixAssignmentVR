using UnityEngine;
using UnityEngine.InputSystem;

public class Pawn : MonoBehaviour, IPlayerInteractable
{
    private PlayerController playerController;
    private PlayerState playerState;

    private Vector2 mousePos;
    private Vector3 mouseWorldPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //from IPlayerInteractable
    public void OnInteract()
    {
        SetStartPointer();
    }

    public Vector3 PositionPointer
    {
        get {
            return mouseWorldPosition;
        }
    }

    public Vector3 PositionEndPointer
    {
        get {
            return CalculateEndPointer(); 
        }
    }


    public PlayerController GetPlayerController()
    {
        return playerController;
    }
    public PlayerState GetPlayerState()
    {
        return playerState;
    }

    public void SetPlayerController(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void SetPlayerState(PlayerState playerState)
    {
        this.playerState = playerState;
    }

    private Vector3 CalculateEndPointer()
    {
        return mouseWorldPosition - Camera.main.transform.position;
    }

    private void SetStartPointer() {
        mousePos = Mouse.current.position.ReadValue();
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
    }
}
