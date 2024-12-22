using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateDisplayValue : MonoBehaviour
{
    private TextMeshProUGUI textContent;
    private float forwardOffset = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        textContent = this.gameObject.GetComponent<TextMeshProUGUI>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SliderCallback(float value){
        this.forwardOffset = (value * 1.9f) + 0.1f;
        Debug.Log(this.forwardOffset);
        this.textContent.text = this.forwardOffset.ToString();
    }

}
