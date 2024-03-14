using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
public class InputManager : MonoBehaviour
{
    public static InputManager Instance => instance;
    private static InputManager instance;

    private Vector3 inputPosition;
    private Vector3 previousInputPosition;
    private bool hasInput;
    
    [SerializeField]
    private float inputSensivity;

    private void Awake()
    {
        if(instance !=null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }
    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }
    private void Update()
    {
        if(PlayerController.Instance == null)
        {
            return;
        }

#if UNITY_EDITOR
        inputPosition = Mouse.current.position.ReadValue();

        if (Mouse.current.leftButton.isPressed)
        {
            if (!hasInput)
            {
                previousInputPosition = inputPosition;

                // Start the game when the first input is received
                if (!GameManager.Instance.IsGameStarted)
                {
                    GameManager.Instance.StartGame();
                }
            }
            hasInput = true;
        }
        else
        {
            hasInput = false;
        }
#else
        if(Touch.activeTouches.Count > 0)
        {
            inputPosition = Touch.activeTouches[0].screenPosition;

            if (!hasInput)
            {
                previousInputPosition = inputPosition;
            }
            hasInput= true;
        }
#endif
        if (hasInput)
        {
            float normalizedDeltaPosition = (inputPosition.x - previousInputPosition.x) / Screen.width * inputSensivity;
            PlayerController.Instance.SetDeltaPosition(normalizedDeltaPosition);
        }
        else
        {
            PlayerController.Instance.CancelMovement();
        }
        previousInputPosition = inputPosition;
    }
}