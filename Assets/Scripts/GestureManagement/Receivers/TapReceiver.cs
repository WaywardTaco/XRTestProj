using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapReceiver : MonoBehaviour
{
    [SerializeField] private GameObject templateObject;
    // Start is called before the first frame update
    void Start(){
        this.templateObject.SetActive(false);
        
        GestureManager.Instance.OnTap += this.OnTap;
    }

    private void OnDisable() {
        GestureManager.Instance.OnTap -= this.OnTap;
    }
    
    public void OnTap(object sender, TapEventArgs args){
        if(args.HitObject != null)
            return;

        GameObject obj = GameObject.Instantiate(templateObject, this.transform);
        obj.SetActive(true);

        Vector3 spawnLoc = Vector3.zero;
        
        Ray ray = Camera.main.ScreenPointToRay(args.Position);

        spawnLoc += ray.GetPoint(10);

        obj.transform.localPosition = spawnLoc;
    }
}
