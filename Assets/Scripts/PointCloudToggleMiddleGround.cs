using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class PointCloudToggleMiddleGround : MonoBehaviour
{
    List<GameObject> pointClouds = new List<GameObject>();

    bool isOn = false;

    public void OnToggle(Toggle value){
        this.isOn = value.isOn;

        foreach(GameObject cloud in pointClouds){
            cloud.GetComponent<ARPointCloudParticleVisualizer>().enabled = this.isOn;
        }
    }

    public void addCloud(GameObject cloud){
        pointClouds.Add(cloud);
        cloud.GetComponent<ARPointCloudParticleVisualizer>().enabled = this.isOn;
    }

    public void removeCloud(GameObject cloud){
        pointClouds.Remove(cloud);
    }
}
