using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

public class VirutalButtonLimitCheck : MonoBehaviour
{

    private ARTrackedImage trackedImage;
    [SerializeField] private UnityEvent onTrackedImageLimited;
    // Start is called before the first frame update
    void Start()
    {
        trackedImage = GetComponent<ARTrackedImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if(trackedImage == null) return;

        if(trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Limited){
            onTrackedImageLimited.Invoke();
        }
    }
}
