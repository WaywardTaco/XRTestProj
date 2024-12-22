using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TrackedImageObjectHandler : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private Vector3 spawnOffset = new Vector3(0.0f, 0.05f, 0.0f);

    public void OnTrackedImageChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs){
        foreach (var trackable in eventArgs.added){
            Debug.Log("Tracking new Image: " + trackable.name + " | Tracking State: " + trackable.trackingState);
        
            GameObject virtualObj = GameObject.Instantiate(objectPrefab);
            virtualObj.transform.SetParent(trackable.gameObject.transform);
            virtualObj.transform.localPosition = spawnOffset;
        }
        foreach (var trackable in eventArgs.updated){
            Debug.Log("Image Updated: " + trackable.name + " | Tracking State: " + trackable.trackingState);
        }
        foreach (var trackable in eventArgs.removed){
            Debug.Log("Removed Image: " + trackable.name + " | Tracking State: " + trackable.trackingState);
        }
    }
}
