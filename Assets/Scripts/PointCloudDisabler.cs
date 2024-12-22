using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PointCloudDisabler : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private GameObject pointCloudVis;

    // Start is called before the first frame update
    void OnEnable()
    {
        toggle = GameObject.Find("PointCloudToggle").GetComponent<Toggle>();

        toggle.onValueChanged.AddListener(delegate {
            OnToggle(toggle);
        });

        this.pointCloudVis.SetActive(toggle.isOn);
    }

    public void OnToggle(Toggle value){
        Debug.Log("Toggle: " + value.isOn);
        this.pointCloudVis.SetActive(value.isOn);
    }
}
