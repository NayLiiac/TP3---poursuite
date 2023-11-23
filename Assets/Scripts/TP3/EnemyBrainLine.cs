using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrainLine : MonoBehaviour
{
    public Transform targetTransform;
    public float speed = 10;

    public bool arrived = false;

    Vector3 velocity;
    Vector3 direction;
    Vector3 directionNormalized;

    Vector3 enemyVector;
    Vector3 targetVector;

    public EnemyBrainScout enemyBrainScout;
    
    // Start is called before the first frame update
    void Start() {
        targetTransform = enemyBrainScout.waypoints[0].transform;
        SetDirection();
    }

    // Update is called once per frame
    void Update() {
        if (!arrived) {
            
            GoToTarget();
        }
    }


    public void GoToTarget() {
        if (MathHelper.VectorDistance(targetTransform.position, this.transform.position) <= 0.3f) {

            arrived = true;
            enemyBrainScout.NextWaypointPosition();
        }

        velocity = directionNormalized * Time.deltaTime * speed;

        this.transform.Translate(velocity, Space.World);
    }

    public void SetDirection() {

        direction = targetTransform.position - this.transform.position;
        directionNormalized = direction.normalized;

        enemyVector = this.transform.position;
        targetVector = targetTransform.position;

        this.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }
}
