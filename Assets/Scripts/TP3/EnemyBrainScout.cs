using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrainScout : MonoBehaviour
{
    public List<GameObject> waypoints = new List<GameObject>();
    public int waypointListIndex = 0;

    public EnemyBrainLine enemyBrainLine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextWaypointPosition() {
        
        waypointListIndex++;

        if (waypointListIndex < waypoints.Count) {

            enemyBrainLine.targetTransform = waypoints[waypointListIndex].transform;


        }
        else {

            waypointListIndex = 0;

            enemyBrainLine.targetTransform = waypoints[waypointListIndex].transform;

        }

        enemyBrainLine.SetDirection();

        enemyBrainLine.arrived = false;
    }

}
