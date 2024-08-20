using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{

    private GameObject aktivObject;
    private aktivedAnimatioByVei AABV;
    void Update()
    {
    // Create a ray from the center of the screen
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        // Create a RaycastHit variable to store information about what the ray hits
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit))
        {
            // Check if the ray hit an object
            GameObject hitObject = hit.collider.gameObject;

            if (aktivObject != null && aktivObject != hitObject)
            {
                // Deactivate animation of the previous object
                AABV = aktivObject.GetComponent<aktivedAnimatioByVei>();
                if (AABV != null)
                {
                    AABV.deaktivedTheAnimation();
                }
            }

            // Update the current active object
            aktivObject = hitObject;

            // Activate animation of the new object
            AABV = aktivObject.GetComponent<aktivedAnimatioByVei>();
            if (AABV != null)
            {
                AABV.aktivedTheAnimation();
            }

            // Output the name of the object the ray hit
            Debug.Log("Hit object: " + hitObject.name);
        }
        else
        {
            // Ray did not hit any object
            if (aktivObject != null)
            {
                // Deactivate animation of the previous object
                AABV = aktivObject.GetComponent<aktivedAnimatioByVei>();
                if (AABV != null)
                {
                    AABV.deaktivedTheAnimation();
                }

                // Reset the reference to the currently active object
                aktivObject = null;
            }
        }
    }
}
