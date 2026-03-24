using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        EventManager.RegisterEvent<Event_InStage_MLBPressed>(OnLeftClick);
    }

    public RaycastHit2D DrawRay(Vector3 direction, Vector3 StartPosition, float rayDist, string layerMask, Color rayColor)
    {
        Debug.DrawRay(StartPosition, direction * rayDist, rayColor);

        //착지 체크
        RaycastHit2D RayHit =
            Physics2D.Raycast(StartPosition, direction, rayDist, LayerMask.GetMask(layerMask));

        return RayHit;
    }

    public RaycastHit2D[] DrawRayAll(Vector3 direction, Vector3 StartPosition, float rayDist, string layerMask, Color rayColor)
    {
        Debug.DrawRay(StartPosition, direction * rayDist, rayColor);

        //착지 체크
        RaycastHit2D[] RayHit =
            Physics2D.RaycastAll(StartPosition, direction, rayDist, LayerMask.GetMask(layerMask));

        return RayHit;
    }

    public void OnLeftClick(Event_InStage_MLBPressed message)
    {
        // 이벤트 클래스에서 마우스 클릭 위치를 받아 저장
        Vector3 mousePos = message.mousePos;

        RaycastHit2D[] rayHit = GameManager.instance.DrawRayAll(new Vector3(0, 0, -1), mousePos, 1.02f, "Interactable", Color.white);
        if (rayHit.Length == 0)
        {
            BuildManager.instance.SelectBuilding(null);
            return;
        }

        RaycastHit2D bestHit = rayHit[0];
        int bestLayerValue = int.MinValue;
        int bestOrder = int.MinValue;

        foreach (RaycastHit2D hit in rayHit)
        {
            SpriteRenderer sr = hit.collider.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                int layerValue = SortingLayer.GetLayerValueFromID(sr.sortingLayerID);
                int order = sr.sortingOrder;

                Debug.Log(layerValue);

                bool isHigherLayer = layerValue > bestLayerValue;
                bool sameLayerHigherOrder = layerValue == bestLayerValue && order > bestOrder;

                // 더 앞에 보이는 Sprite 선택
                if (isHigherLayer || sameLayerHigherOrder)
                {
                    bestHit = hit;
                    bestLayerValue = layerValue;
                    bestOrder = order;
                }
            }
        }

        Debug.Log("hit");
        if (bestHit.collider.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            Debug.Log("interact");
            interactable.Interact();
        }

        BuildManager.instance.ChangeBuildMode(buildMode.None);
    }
}
