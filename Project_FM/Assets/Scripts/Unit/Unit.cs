using UnityEngine;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Animator animator;

    [SerializeField]
    private UnitData data;
    [SerializeField]
    private UnitAIController aiController;

    private Queue<Transform> waypoints = new Queue<Transform>();

    private Transform curWayPoint;

    public void Init(UnitData _data, Transform[] waypointsArr)
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        data = _data;
        foreach(Transform tf in waypointsArr)
        {
            waypoints.Enqueue(tf);
        }
        aiController.BuildBT(this);
    }

    private void Update()
    {
        aiController.EvaluateBT();
    }

    // 유닛 이동 함수
    public bool Move()
    {
        if (waypoints != null && waypoints.Count > 0)
        {
            if (curWayPoint == null || (curWayPoint != null && Vector2.Distance(transform.position, curWayPoint.position) <= 0.01f))
            {
                curWayPoint = waypoints.Dequeue();
            }
        }

        if (curWayPoint != null)
        {
            Debug.Log("Moving");
            animator?.SetBool("IsMoving", true);
            transform.position = Vector3.MoveTowards(transform.position, curWayPoint.position, data.baseMoveSpeed * Time.deltaTime);

            return true;
        }

        Debug.Log("Move fail");
        animator?.SetBool("IsMoving", false);
        return false;
    }
}
