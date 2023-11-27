using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveDestination : MonoBehaviour
{
    [Header("Different Targets")]
    public Transform goalPosition;
    public Transform playerPosition;
    public Transform colleaguePosition;

    [Header("Waypoints & Colleagues")]
    public List<GameObject> waypoints = new List<GameObject>();
    public int index = 0;

    [Header("Infos")]
    public NavMeshAgent agent;
    public bool arrived = false;
    public bool coroutineLaunched = false;


    public void Patrol(Transform transform) {
        if (!arrived) {

            goalPosition.position = transform.position;
            agent.destination = goalPosition.position;
        }
    }

    public void NextWaypoint() {
        if (!coroutineLaunched)
        {
            //NextWaypoint() comes from an Update() method, it's just a security in case the agent is too slow
            StartCoroutine(NextWaypointCoroutine());
        }
        
        
    }

    
    public IEnumerator NextWaypointCoroutine()
    {
        coroutineLaunched= true;
        if (index < (waypoints.Count - 1)) {

            index++;
            goalPosition = waypoints[index].transform;
        }
        else{

            index= 0;
        }


        arrived = false;
        yield return new WaitForSeconds(5);
        coroutineLaunched= false;
    }
}
