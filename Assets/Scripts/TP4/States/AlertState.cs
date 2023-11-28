using UnityEngine;
using UnityEngine.AI;

public class AlertState : EnemyBaseState
{
    public float alertTime;
    public NavMeshAgent agent;
    public override void EnterState(EnemyStateManager enemy) {
        agent.destination = enemy.playerController.noiseLocation;
    }


    public override void UpdateState(EnemyStateManager enemy) {


        if(MathHelper.VectorDistance(enemy.transform.position, enemy.playerController.noiseLocation) <= 0.3f) {
            // Stays in noise position during a short of time
            if (alertTime < 10)
            {
                alertTime += Time.deltaTime;
            }
            // checks if the enemy is near the player during inspection of the noise made
            else if(MathHelper.VectorDistance(enemy.transform.position, enemy.pursuitState.playerPosition.position) <= 3f)
            {
                enemy.SwitchState(enemy.pursuitState);
                alertTime= 0;
               
            }
            // if no ones around, return patrolling
            else
            {
                enemy.SwitchState(enemy.patrollingState);
                alertTime = 0;
            }
        }
    }


    public override void CollisionEnter(EnemyStateManager enemy, Collision collision) {
        // does nothing
    }
}
