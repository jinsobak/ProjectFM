using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    private InputSystem_Actions inputActions;


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

    private void Update()
    {

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

        // UI와 게임 오브젝트가 겹쳐있을 경우 클릭 이벤트 발행X
        // UI만 클릭됨
        if (CheckPointerOnUI())
            return;

        // 클릭한 마우스 위치를 담아 이벤트 발행
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        EventManager.Publish(new Event_InStage_MLBPressed(mousePos));
    }

    private bool CheckPointerOnUI()
    {
        // 현재 씬에 EvnetSystem이 없으면 false 반환
        if (EventSystem.current == null)
            return false;

        // 
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = Mouse.current.position.ReadValue()
        };

        List<RaycastResult> results = new List<RaycastResult>();

        EventSystem.current.RaycastAll(pointerEventData, results);

        return results.Count > 0;
    }
}
