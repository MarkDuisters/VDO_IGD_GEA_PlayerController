#if UNITY_EDITOR
using UnityEngine;
[ExecuteInEditMode]
public class WaypointDebugger : MonoBehaviour
{

    [SerializeField] Transform[] waypoints;

    void Start()
    {
        UpdateArray();
    }

    void Update()
    {

        if (waypoints.Length != transform.childCount)
        {
            UpdateArray();
        }

        for (int i = 1; i < waypoints.Length; i++)
        {
            int nextIndex = i + 1;
            nextIndex = nextIndex % waypoints.Length;
            nextIndex = nextIndex == 0 ? 1 : nextIndex;//skip altijd de eerste index.
            Debug.DrawLine(waypoints[i].position, waypoints[nextIndex].position, new Color(0.6f, 1f, 1f, 1f));

        }

    }

    void UpdateArray()
    {
        waypoints = GetComponentsInChildren<Transform>();
    }

}
#endif