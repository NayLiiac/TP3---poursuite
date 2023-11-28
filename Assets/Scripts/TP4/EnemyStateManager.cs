using UnityEngine;
// Video used to help to understand better the design pattern: https://www.youtube.com/watch?v=Vt8aZDPzRjI

public class EnemyStateManager : MonoBehaviour
{ 
    EnemyBaseState currentState;

    public PlayerController playerController;

    public PursuitState pursuitState = new PursuitState();
    public PatrollingState patrollingState = new PatrollingState();
    public DiscussState discussState = new DiscussState();
    public AlertState alertState = new AlertState();


    void Start() {
        currentState = patrollingState;
        
        currentState.EnterState(this);
    }

    void Update() {
        currentState.UpdateState(this);
    }

    public void OnCollisionEnter(Collision collision) {
        currentState.CollisionEnter(this, collision);
    }

    public void SwitchState(EnemyBaseState state) {

        currentState = state;
        currentState.EnterState(this);
    }
}
