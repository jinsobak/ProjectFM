using UnityEngine;

public enum LineType
{
    Ground,
    Sky
}

public enum GroundLinePosition
{
    UP,
    DOWN
}

public class StageLine : MonoBehaviour
{
    [SerializeField]
    private LineType lineType;
    [SerializeField]
    public Transform[] waypoints;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if(waypoints != null && waypoints.Length >= 2)
        {
            for(int i = 0; i < waypoints.Length - 1; i++)
            {
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            }
        }
    }
}
