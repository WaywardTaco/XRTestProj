using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragReceiver : MonoBehaviour
{
    [SerializeField] private GameObject templateObject;
    // Start is called before the first frame update
    void Start(){
        this.templateObject.SetActive(false);
        
        GestureManager.Instance.OnDrag += this.OnDrag;
    }

    private void OnDisable() {
        GestureManager.Instance.OnDrag -= this.OnDrag;
    }
    
    public void OnDrag(object sender, DragEventArgs args){
        if(args.HitObject != null)
            return;

        // GameObject obj = GameObject.Instantiate(templateObject, this.transform);
        // obj.SetActive(true);

        // Vector3 spawnLoc = Vector3.zero;
        
        // Ray ray = Camera.main.ScreenPointToRay(args.Position);

        // spawnLoc += ray.GetPoint(10);

        // obj.transform.localPosition = spawnLoc;
    }
}
