using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    // Detect collision with non-trigger objects
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("The star has detected a collision!");
    }
}
