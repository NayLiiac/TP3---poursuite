using System.Collections;
using UnityEngine;

public class PatrollingState : EnemyBaseState
{
    public Transform target;
    public bool canChat = true;
    public override void EnterState(EnemyStateManager enemy) {
        
        target = enemy.moveDestination.waypoints[enemy.moveDestination.index].transform;
        enemy.moveDestination.goalPosition = target;

        enemy.moveDestination.arrived = false;
        enemy.moveDestination.Patrol(target);
    }

    public override void UpdateState(EnemyStateManager enemy) {

        // Patrolling state
        if (MathHelper.VectorDistance(enemy.transform.position, target.position) <= 0.3f) {

            enemy.moveDestination.arrived= true;

            enemy.moveDestination.NextWaypoint();

            target = enemy.moveDestination.waypoints[enemy.moveDestination.index].transform;
            enemy.moveDestination.Patrol(target);

        }

        // if Enemy sees the player :
        if(MathHelper.VectorDistance(enemy.transform.position, enemy.moveDestination.playerPosition.position) <= 7.5f && enemy.playerController.isPlayerAlive) {
            enemy.SwitchState(enemy.pursuitState);
        }

         // if Enemy sees a colleague :
        if(MathHelper.VectorDistance(enemy.transform.position, enemy.moveDestination.colleaguePosition.position) <= 4){
            if (canChat) {
                enemy.SwitchState(enemy.discussState);
            }
        }

        // if a noise was made :
        if(enemy.playerController.noiseWasMade) { 
            enemy.SwitchState(enemy.alertState);
            enemy.playerController.noiseWasMade = false;

        }
    }

    public override void CollisionEnter(EnemyStateManager enemy, Collision collision)
    {
        // does nothing
    }

    public IEnumerator EnemyCanChat()
    {
        yield return new WaitForSeconds(10);
        canChat = true;

    }
}
