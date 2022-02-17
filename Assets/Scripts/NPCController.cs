using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float moveSpeed;
    public Vector2 pointA;
    public Vector2 pointB;

    Animator anim;

    private State state = State.Patrolling;
    private Vector2 targetPoint;
    private Vector2 diff;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        targetPoint = pointA;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag == "Player") {
            Debug.Log("Met the player!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Patrol state
        if(state == State.Patrolling)
        {
            diff.x = targetPoint.x - transform.position.x;
            diff.y = targetPoint.y - transform.position.y;

            // If we've arrived, switch target.
            if(diff.magnitude < 0.1f)
            {
                if(targetPoint == pointA)
                {
                    targetPoint = pointB;
                } else
                {
                    targetPoint = pointA;
                }
                diff.x = targetPoint.x - transform.position.x;
                diff.y = targetPoint.y - transform.position.y;
            }

            Vector2 direction = diff.normalized;

            anim.SetFloat("moveX", direction.x);
            anim.SetFloat("moveY", direction.y);

            Vector3 newPosition = transform.position;
            newPosition.x += direction.x * moveSpeed * Time.deltaTime;
            newPosition.y += direction.y * moveSpeed * Time.deltaTime;
            transform.position = newPosition;
        }
    }

    enum State {
        Patrolling,
        Speaking
    }
}
