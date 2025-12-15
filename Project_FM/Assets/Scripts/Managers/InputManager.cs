using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnTapKeyPressed(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        EventManager.Publish(new Event_InStage_SlotTapChange());
    }

    public void OnMouseLeftPressed(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        EventManager.Publish(new Event_InStage_MLBPressed());
    }
}
