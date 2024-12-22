using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapDeleteReceiver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveTap(){
        Debug.Log("Tap Received");
        GameObject.Destroy(this.gameObject);
    }
}
