using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class AI_Behavior : MonoBehaviour
{
    public enum AIStates { Idle, Patrol, Chase, Attack };
    public AIStates currentState = AIStates.Idle;

    NavMeshAgent agent => GetComponent<NavMeshAgent>();
    [SerializeField] Transform currentTarget;

    [Range(0.4f, 8f)][SerializeField] float waypointStoppingdistance = 0.4f;
    [SerializeField] Transform[] waypoints;
    [SerializeField] int currentIndex = 0;

    void Start()
    {
        UpdateBehavior(AIStates.Patrol);
    }

    public void UpdateBehavior(AIStates newState)
    {
        StopAllCoroutines();
        currentState = newState;

        switch (currentState)
        {
            case AIStates.Idle:
                UpdateTarget(transform);
                agent.isStopped = true;
                break;
            case AIStates.Patrol:
                StartCoroutine(FollowWaypoints());
                break;
            case AIStates.Chase:
                break;
            case AIStates.Attack:
                break;

            default:
                UpdateBehavior(AIStates.Idle);
                Debug.LogWarning("Invalid state, default to Idle");
                break;

        }
    }

    IEnumerator FollowWaypoints()
    {

        if (waypoints.Length == 0)
        {
            UpdateBehavior(AIStates.Idle);
            agent.isStopped = true;
            yield break;//stop de coroutine;
        }

        float distance = 0f;
        //Door yield return null, wacht de while loop steeds 1 frame, en ticken we
        //dus mee in sync met de Update loop. Je kan dit bekijken als een custom Update loop.
        UpdateTarget(waypoints[currentIndex].transform);
        print(currentTarget.name);
        while (true)
        {
            distance = Vector3.Distance(transform.position, currentTarget.position);
            if (distance < waypointStoppingdistance)
            {
                currentIndex = NextWayPoint(currentIndex);
                print(currentIndex);
                UpdateTarget(waypoints[currentIndex].transform);
                print(currentTarget.name);
                print("Target reached");
                //  break;
            }
            Debug.DrawLine(transform.position, currentTarget.position, Color.red);
            yield return null;
        }
    }

    void UpdateTarget(Transform newTarget)
    {
        currentTarget = newTarget;
        agent.SetDestination(currentTarget.position);
    }

    int NextWayPoint(int currentIndex)
    {
        int newIndex = currentIndex + 1;
        newIndex = newIndex % waypoints.Length;
        return newIndex;

    }

    public void SetWayPointList(Transform[] setList)
    {
        waypoints = setList;
    }
}
