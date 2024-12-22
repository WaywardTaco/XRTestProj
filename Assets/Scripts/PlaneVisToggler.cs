using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class PlaneVisToggler : MonoBehaviour
{
    [SerializeField] private Toggle toggle;

    // Start is called before the first frame update
    void OnEnable()
    {
        toggle = GameObject.Find("PlaneVisToggle").GetComponent<Toggle>();

        toggle.onValueChanged.AddListener(delegate {
            OnToggle(toggle);
        });

        this.gameObject.GetComponent<ARPlaneMeshVisualizer>().enabled = toggle.isOn;
    }

    public void OnToggle(Toggle value){
        Debug.Log("Toggle: " + value.isOn);    
        this.gameObject.GetComponent<ARPlaneMeshVisualizer>().enabled = toggle.isOn;
    }
}
