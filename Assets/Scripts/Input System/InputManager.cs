using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;

    public delegate void EndTouch(Vector2 position, float time);
    public event StartTouch OnEndTouch;

    private PlayerController playerControls;
    private Camera mainCamera;
    private void Awake()
    {

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        playerControls = new PlayerController();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        playerControls.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        playerControls.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        if(OnStartTouch != null)
        {
            OnStartTouch(Utils.ScreenToWorld
                (mainCamera, playerControls.Touch.PrimaryPosition.ReadValue<Vector2>()),
                (float)context.startTime);
        }
    }

    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnEndTouch != null)
        {
            OnEndTouch(Utils.ScreenToWorld
                (mainCamera, playerControls.Touch.PrimaryPosition.ReadValue<Vector2>()),
                (float)context.startTime);
        }
    }

    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorld(mainCamera, playerControls.Touch.PrimaryPosition.ReadValue<Vector2>());
    }
}
