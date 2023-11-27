using UnityEngine;

public abstract class EnemyBaseState : MonoBehaviour
{
    public abstract void EnterState(EnemyStateManager enemy);

    public abstract void UpdateState(EnemyStateManager enemy);

    public abstract void CollisionEnter(EnemyStateManager enemy, Collision collision);
}
