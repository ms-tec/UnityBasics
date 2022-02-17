using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    Animator anim;
    Rigidbody2D body;

    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag == "NPC") {
            Debug.Log("Met the NPC!");
        }
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

    void FixedUpdate()
    {
        body.MovePosition(body.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
