using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        anim.SetFloat("moveX", horizontal);
        // Debug.Log(horizontal);

        Vector3 position = transform.position;
        position.x += horizontal * moveSpeed * Time.deltaTime;
        transform.position = position;
    }
}
