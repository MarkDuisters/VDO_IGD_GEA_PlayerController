using System;
using UnityEngine;

public class AISpawner : MonoBehaviour
{
    [SerializeField] WayPointList[] waypointCollection;

    [SerializeField] GameObject agentPrefab;
    [SerializeField] int maxAgents = 5;


    void Start()
    {
        for (int i = 0; i < maxAgents; i++)
        {
            GameObject go = Instantiate(agentPrefab, transform.position, Quaternion.identity);
            if (waypointCollection.Length == 0) continue;
            int randomIndex = UnityEngine.Random.Range(0, waypointCollection.Length);
            //We hebben een lijst van "WayPointList" genaamd waypointCollection.
            //Uit deze lijst kiezen we willekeurig 1 "object" dus we krijgen een "WayPointList" terug.
            //vervolgens kunnen we van de gekoze WayPointList zijn lokale lijst "waypoints" opvragen
            //met waypointCollection[randomIndex].waypoints.
            //Deze sturen we door naar de ingespawnde agent.
            Transform[] getList = waypointCollection[randomIndex].waypoints;
            go.GetComponent<AI_Behavior>().SetWayPointList(getList);
        }
    }

}

[Serializable]
class WayPointList
{
    public Transform[] waypoints;
}
