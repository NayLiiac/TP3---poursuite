using UnityEngine;
using UnityEngine.AI;

public class PursuitState : EnemyBaseState
{
    // public Transform target;
    public Transform playerPosition;
    public NavMeshAgent agent;

    public bool isInPursuitState = false;
    public override void EnterState(EnemyStateManager enemy) {
        Debug.Log("Pursuit State");
        isInPursuitState = true;
        agent.destination = playerPosition.position;
    }

    public override void UpdateState(EnemyStateManager enemy) {

        if (enemy.playerController.isPlayerAlive && 
            MathHelper.VectorDistance(enemy.transform.position, playerPosition.position) <= 7.5f) {
            Debug.Log("not a trivial pursuit");

            Pursuit(playerPosition);
        }
        else {
            Debug.Log("ran too fast for me");
            enemy.SwitchState(enemy.patrollingState);
            isInPursuitState = false;
        }
    }

    public override void CollisionEnter(EnemyStateManager enemy, Collision collision) {

        if(collision.gameObject.tag == "Player" && enemy.playerController.isPlayerAlive) {
            Debug.Log("My work out shows some benefits");
            enemy.playerController.isPlayerAlive = false;
        }
    }


    public void Pursuit(Transform transform) {
        agent.destination = transform.position;
    }
}
