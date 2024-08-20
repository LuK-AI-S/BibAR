using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class PlaceTrackedImage : MonoBehaviour
{
    // Reference to AR tracked image manager component
    private ARTrackedImageManager _trackedImagesManager;

    // List of prefabs to instantiate - these should be named the same
    // as their corresponding 2D images in the reference image library 
    public GameObject[] ArPrefabs;

    // UI Image to indicate tracking status
    public Image trackingStatusImage;

    // Keep dictionary array of created prefabs
    private readonly Dictionary<string, GameObject> _instantiatedPrefabs = new Dictionary<string, GameObject>();

    void Awake()
    {
        trackingStatusImage.color = Color.yellow;
        // Cache a reference to the Tracked Image Manager component
        if (_trackedImagesManager == null)
        {
            _trackedImagesManager = GetComponent<ARTrackedImageManager>();
        }
    }

    void OnEnable()
    {
        // Attach event handler when tracked images change
        _trackedImagesManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        // Remove event handler 
        _trackedImagesManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    // Event Handler
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        trackingStatusImage.color = Color.blue;
        bool anyTracking = false;

        // Loop through all new tracked images that have been detected
        foreach (var trackedImage in eventArgs.added)
        {
            // Get the name of the reference image
            var imageName = trackedImage.referenceImage.name;

            // Now loop over the array of prefabs
            foreach (var curPrefab in ArPrefabs)
            {
                // Check whether this prefab matches the tracked image name, and that
                // the prefab hasn't already been created
                if (string.Compare(curPrefab.name, imageName, StringComparison.OrdinalIgnoreCase) == 0
                    && !_instantiatedPrefabs.ContainsKey(imageName))
                {
                    // Instantiate the prefab, parenting it to the ARTrackedImage
                    var newPrefab = Instantiate(curPrefab, trackedImage.transform);
                    // Add the created prefab to our array
                    _instantiatedPrefabs[imageName] = newPrefab;
                }
            }
        }

        // For all prefabs that have been created so far, set them active or not depending
        // on whether their corresponding image is currently being tracked
        foreach (var trackedImage in eventArgs.updated)
        {
            if (_instantiatedPrefabs.TryGetValue(trackedImage.referenceImage.name, out var prefab))
            {
                bool isTracking = trackedImage.trackingState == TrackingState.Tracking;
                prefab.SetActive(isTracking);

                if (isTracking)
                {
                    anyTracking = true;
                }
            }
        }

        // If the AR subsystem has given up looking for a tracked image
        foreach (var trackedImage in eventArgs.removed)
        {
            if (_instantiatedPrefabs.TryGetValue(trackedImage.referenceImage.name, out var prefab))
            {
                // Destroy its prefab
                Destroy(prefab);
                // Also remove the instance from our array
                _instantiatedPrefabs.Remove(trackedImage.referenceImage.name);
            }
        }

        // Update the UI image color based on tracking state
        if (trackingStatusImage != null)
        {
            trackingStatusImage.color = anyTracking ? Color.green : Color.red;
        }
    }
}