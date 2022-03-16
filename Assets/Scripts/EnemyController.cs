using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;

    // Variables for patrol state
    public float moveSpeed;
    public Vector2[] patrolPoints;
    private int patrolPointIndex = 0;

    // Variables for chase state
    public GameObject player;
    public float minChasePlayerDistance;
    public float maxChasePlayerDistance;
    public float maxChaseDistance;

    // Variables for return state
    private Vector2 returnPoint;
    private bool hasReturn = false;

    // Variables used in all states
    private Vector2 moveDirection = new Vector2(0, 0);

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Initialize patrol route
        if (patrolPoints.Length == 0)
        {
            patrolPoints = new Vector2[] { body.position };
            Debug.LogError("No patrol route configured for enemy! Keeping enemy at position.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Write code that figures out which behavior function to call. Options
        // are Patrol(), Chase(), and Return()
        Patrol();

        // Update animation
        anim.SetFloat("moveX", Mathf.Sign(moveDirection.x));
        anim.SetFloat("moveY", Mathf.Sign(moveDirection.y));
    }

    void FixedUpdate()
    {
        body.MovePosition(body.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    void Patrol()
    {
        float distanceToNextPoint = (patrolPoints[patrolPointIndex] - body.position).magnitude;
        if(distanceToNextPoint < 0.1)
        {
            patrolPointIndex = (patrolPointIndex + 1) % patrolPoints.Length;
            distanceToNextPoint = (patrolPoints[patrolPointIndex] - body.position).magnitude;
        }

        MoveTowards(patrolPoints[patrolPointIndex]);

        // TODO: When to transition to a different statey, and how?
    }

    void Chase()
    {
        MoveTowards(player.transform.position);

        // TODO: When to transition to a different statey, and how?
    }

    void Return()
    {
        // Find out where to return to. This is only done when we enter this state.
        if(!hasReturn)
        {
            hasReturn = true;
            returnPoint = NearestPatrolPoint();
        }

        // Knowing where to return to, do so:
        MoveTowards(returnPoint);

        // TODO: When to transition to a different statey, and how?
    }

    void MoveTowards(Vector2 point)
    {
        moveDirection = (point - body.position).normalized;
    }

    Vector2 NearestPatrolPoint()
    {
        Vector2 nearest = patrolPoints[0];
        float dist = (nearest - body.position).magnitude;
        float newDist = dist;
        foreach (Vector2 point in patrolPoints)
        {
            newDist = (point - body.position).magnitude;
            if (newDist < dist)
            {
                nearest = point;
                dist = newDist;
            }
        }

        return nearest;
    }
}
