using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    Animator anim;
    Rigidbody2D body;
    public Text pointText;

    private Vector2 movement;
    private int points = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        pointText.text = "Score: " + points;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        movement.x = horizontal;
        anim.SetFloat("moveX", horizontal);

        float vertical = Input.GetAxis("Vertical");
        movement.y = vertical;
        anim.SetFloat("moveY", vertical);
    }

    // Called a fixed number of times per second
    // Use for physics calculations ONLY
    void FixedUpdate()
    {
        body.MovePosition(body.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    // Detect collision with trigger objects
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("The player has detected a collision!");
    }
}
