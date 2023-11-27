using UnityEngine;

public class DiscussState : EnemyBaseState
{
    float timePassed = 0;
    public override void EnterState(EnemyStateManager enemy) {

        enemy.moveDestination.agent.destination = enemy.moveDestination.colleaguePosition.transform.position;
        enemy.patrollingState.canChat = false;
        enemy.patrollingState.StartCoroutine(enemy.patrollingState.EnemyCanChat());
    }
      

    public override void UpdateState(EnemyStateManager enemy) {
        if(timePassed < 5) {
            timePassed += Time.deltaTime;
        }
        else {

            enemy.SwitchState(enemy.patrollingState);
            timePassed = 0;
        }
    }

    public override void CollisionEnter(EnemyStateManager enemy, Collision collision) {
        // does nothing
    }


}
