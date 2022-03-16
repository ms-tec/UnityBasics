using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyState state = EnemyState.Chasing;

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
        switch(state)
        {
            case EnemyState.Patrolling:
                Patrol();
                break;
            case EnemyState.Chasing:
                Chase();
                break;
            case EnemyState.Returning:
                Return();
                break;
        }

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

        // State transition
        if(((Vector2)player.transform.position - body.position).magnitude < minChasePlayerDistance)
        {
            state = EnemyState.Chasing;
        }
    }

    void Chase()
    {
        MoveTowards(player.transform.position);

        // State transition
        if((NearestPatrolPoint() - body.position).magnitude > maxChaseDistance)
        {
            state = EnemyState.Returning;
        }
        if (((Vector2)player.transform.position - body.position).magnitude > maxChasePlayerDistance)
        {
            state = EnemyState.Returning;
        }
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

        // State transition
        if((returnPoint - body.position).magnitude < 1)
        {
            state = EnemyState.Patrolling;
        }
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

    enum EnemyState
    {
        Patrolling,
        Chasing,
        Returning
    }
}
