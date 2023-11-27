using UnityEngine;

public class PursuitState : EnemyBaseState
{
    public Transform target;

    public override void EnterState(EnemyStateManager enemy) {
        target = enemy.moveDestination.playerPosition.transform;
        enemy.moveDestination.goalPosition = target;

        enemy.moveDestination.Patrol(target);
    }

    public override void UpdateState(EnemyStateManager enemy) {

        if (enemy.playerController.isPlayerAlive && 
            MathHelper.VectorDistance(enemy.transform.position, target.position) <= 7.5f) {

            enemy.moveDestination.goalPosition = target;
            enemy.moveDestination.Patrol(target);
        }
        else {
            enemy.SwitchState(enemy.patrollingState);
        }
    }

    public override void CollisionEnter(EnemyStateManager enemy, Collision collision) {

        if(collision.gameObject.tag == "Player" && enemy.playerController.isPlayerAlive) {

            enemy.playerController.isPlayerAlive = false;
        }
    }

}
