using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrollingState : EnemyBaseState
{
    public Transform currentTarget;
    public bool canChat = true;

    public Transform colleaguePosition;


    [Header("Waypoints")]
    public List<GameObject> waypoints = new List<GameObject>();
    public int index = 0;

    [Header("Infos")]
    public NavMeshAgent agent;
    public bool arrived = false;
    public bool coroutineLaunched = false;

    public override void EnterState(EnemyStateManager enemy) {

        currentTarget = waypoints[index].transform;

        arrived = false;
        Patrol(currentTarget);
    }

    public override void UpdateState(EnemyStateManager enemy) {

        // Patrolling state
        if (MathHelper.VectorDistance(enemy.transform.position, currentTarget.position) <= 0.3f) {

            arrived= true;
            NextWaypoint();

            currentTarget = waypoints[index].transform;
            Patrol(currentTarget);

        }

        // if Enemy sees the player :
        if(MathHelper.VectorDistance(enemy.transform.position, enemy.pursuitState.playerPosition.position) <= 7.5f && 
            enemy.playerController.isPlayerAlive) {
            
            
            Debug.Log("Pursuit Time !");
            enemy.SwitchState(enemy.pursuitState);
        }

         // if Enemy sees a colleague :
        if(MathHelper.VectorDistance(enemy.transform.position, colleaguePosition.position) <= 4){
            if (canChat) {
                Debug.Log("Discuss time !");
                enemy.SwitchState(enemy.discussState);
            }
        }

        // if a noise was made :
        if(enemy.playerController.noiseWasMade && !enemy.pursuitState.isInPursuitState) {
            Debug.Log("RED ALERT, SUSPECT NOISE HAS BEEN REPORTED");
            enemy.SwitchState(enemy.alertState);
            enemy.playerController.noiseWasMade = false;

        }
    }

    public override void CollisionEnter(EnemyStateManager enemy, Collision collision) {
        // does nothing :(
    }

    public IEnumerator EnemyCanChat() {
        yield return new WaitForSeconds(10);
        canChat = true;
    }

    public void Patrol(Transform transform) {
        if (!arrived) {

            agent.destination = transform.position;
        }
    }


    public void NextWaypoint() {
        if (!coroutineLaunched) {
            //NextWaypoint() comes from an Update() method, it's just a security in case the agent is too slow
            StartCoroutine(NextWaypointCoroutine());
        }
    }

    public IEnumerator NextWaypointCoroutine() {
        coroutineLaunched = true;
        if (index < (waypoints.Count - 1)) {
            
            index++;
            currentTarget = waypoints[index].transform;
        }
        else {
            index = 0;
        }

        arrived = false;
        yield return new WaitForSeconds(5);
        coroutineLaunched = false;
    }
}
