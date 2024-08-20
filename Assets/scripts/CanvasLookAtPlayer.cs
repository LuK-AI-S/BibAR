using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLookAtPlayer : MonoBehaviour
{

    public Transform playerTransform; 
   

    void Update()
    {
        // Ensure the canvas looks at the player
        if (playerTransform != null)
        {
            transform.LookAt(playerTransform);
            
            // Optionally, flip the canvas to face the camera correctly
            transform.Rotate(0, 180, 0);
        }
    }
}
