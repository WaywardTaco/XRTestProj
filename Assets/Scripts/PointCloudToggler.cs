using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class PointCloudToggler : MonoBehaviour
{
    // [SerializeField] private Toggle toggle;

    [SerializeField] private PointCloudToggleMiddleGround toggleRef;

    // Start is called before the first frame update
    void OnEnable()
    {
        toggleRef = GameObject.Find("PointCloudToggleMiddleGround").GetComponent<PointCloudToggleMiddleGround>();

        toggleRef.addCloud(this.gameObject);
        // toggle = GameObject.Find("PointCloudToggle").GetComponent<Toggle>();

        // toggle.onValueChanged.AddListener(delegate {
        //     OnToggle(toggle);
        // });

        // this.gameObject.GetComponent<ARPointCloudParticleVisualizer>().enabled = toggle.isOn;
    }

    private void OnDestroy() {
        toggleRef.removeCloud(this.gameObject);
        
    }

    // public void OnToggle(Toggle value){
    //     // Debug.Log("Toggle: " + value.isOn);    
    //     // this.gameObject.GetComponent<ARPointCloudParticleVisualizer>().enabled = toggle.isOn;
    // }
}
