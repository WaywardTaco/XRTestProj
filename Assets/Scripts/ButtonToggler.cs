using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonToggler : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;

    private bool isActive = false;
    public void Start(){
        targetObject.SetActive(isActive);
    }
    public void OnToggle(){
        this.isActive = !this.isActive;
        this.targetObject.SetActive(this.isActive);
    }
}
