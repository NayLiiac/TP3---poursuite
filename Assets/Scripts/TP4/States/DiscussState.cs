using UnityEngine;
using UnityEngine.AI;

public class DiscussState : EnemyBaseState
{
    float timePassed = 0;
    public NavMeshAgent agent;
    public override void EnterState(EnemyStateManager enemy) {
        Debug.Log("Discuss State");
        agent.destination = enemy.patrollingState.colleaguePosition.transform.position;
        enemy.patrollingState.canChat = false;
        enemy.patrollingState.StartCoroutine(enemy.patrollingState.EnemyCanChat());
    }
      

    public override void UpdateState(EnemyStateManager enemy) {
        if(timePassed < 5) {
            timePassed += Time.deltaTime;
        }
        else {
            Debug.Log("Hum hum, sorry bro, I have some work to do");
            enemy.SwitchState(enemy.patrollingState);
            timePassed = 0;
        }
    }

    public override void CollisionEnter(EnemyStateManager enemy, Collision collision) {
        // does nothing
    }


}
