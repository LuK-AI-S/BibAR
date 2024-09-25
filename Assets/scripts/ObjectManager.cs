using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Management;


public class ObjectManager : MonoBehaviour
{
    public GameObject[] objectsA;
    public GameObject[] objectsB;
    public GameObject[] objectsC;
    public GameObject[] objectsD;

    private char mode = 'A';
    private ARSession arSession;

    public Transform xrOrigin;
    public Camera camInScene;
    public GameObject User;
    private Quaternion initialRotation;
    private Vector3 initialPosition;


    // Start is called before the first frame update
    void Start()
    {
        // Initially set the correct objects to be active based on the mode
        UpdateRendering();
        // Get the ARSession and XR Origin components
        arSession = FindObjectOfType<ARSession>();
        initialRotation = xrOrigin.transform.rotation;
        initialPosition = xrOrigin.transform.position;
    }

    // Update is called once per frame
    

    public void SetMode(char newMode)
    {
        Recenter();
        if (mode != newMode)
        {
            mode = newMode;
            UpdateRendering();
        }
    }

    private void UpdateRendering()
    {

        // Deactivate all objects first
        DeactivateAllObjects();

        // Activate the objects based on the current mode
        switch (mode)
        {
            case 'A':
                SetActiveObjects(objectsA, true);
                break;
            case 'B':
                SetActiveObjects(objectsB, true);
                break;
            case 'C':
                SetActiveObjects(objectsC, true);
                break;
            case 'D':
                SetActiveObjects(objectsD, true);
                break;
            default:
                Debug.LogWarning("Invalid mode: " + mode);
                break;
        }
    }

    private void DeactivateAllObjects()
    {
        SetActiveObjects(objectsA, false);
        SetActiveObjects(objectsB, false);
        SetActiveObjects(objectsC, false);
        SetActiveObjects(objectsD, false);
    }

    private void SetActiveObjects(GameObject[] objects, bool isActive)
    {
        foreach (GameObject obj in objects)
        {
            if (obj != null)
            {
                obj.SetActive(isActive);
            }
        }
    }

/*
Die ARSession wird komplett beendet und neu erstellt wodurch sie die "Scene" mit den jeweiligen objecten
Öffnet alls weir kurz schawerz und erscheint dann wieder 

*/
    public void ResetOrigin()
    {
        if (arSession != null)
        {
            arSession.Reset();
        }
    }


    //rotation wird angepasst
    void Recenter()
    {
        // Get the current local rotation of the AR Camera
        Quaternion cameraLocalRotation = camInScene.transform.localRotation;

        // Extract the Y-axis rotation
        Vector3 cameraEulerAngles = cameraLocalRotation.eulerAngles;
        float cameraYaw = cameraEulerAngles.y;

        // Calculate the inverse Y-axis rotation
        Quaternion inverseYawRotation = Quaternion.Euler(0, -cameraYaw, 0);

        // Reset XR Origin to the initial rotation
        xrOrigin.transform.rotation = initialRotation;

        // Apply the inverse Y-axis rotation to the XR Origin
        xrOrigin.transform.rotation *= inverseYawRotation;

        //try to put the user in the middle 
        // !!!!
        // dosent work if plane-detaction is on
        // !!! 
        xrOrigin.transform.position = initialPosition;
        xrOrigin.transform.localPosition = new Vector3 (camInScene.transform.position.x * -1,xrOrigin.transform.position.y,camInScene.transform.position.z * -1);

        // Reset the local rotation of the AR Camera around the Y-axis
        camInScene.transform.localRotation = Quaternion.Euler(cameraEulerAngles.x, 0, cameraEulerAngles.z);
    }

    
}
