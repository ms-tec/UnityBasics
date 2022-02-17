using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float maxDistX;
    public float maxDistY;

    public float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        Vector3 target = transform.position;
        float dX = player.transform.position.x - transform.position.x;
        float dY = player.transform.position.y - transform.position.y;

        if(Mathf.Abs(dX) > maxDistX || Mathf.Abs(dY) > maxDistY) {
            target.x = target.x + Mathf.Lerp(0, dX, 0.1f) * moveSpeed * Time.deltaTime;
            target.y = target.y + Mathf.Lerp(0, dY, 0.1f) * moveSpeed * Time.deltaTime;
        }
        
        transform.position = target;
    }
}
