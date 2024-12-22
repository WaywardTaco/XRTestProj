using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanReceiver : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;

    // Start is called before the first frame update
    void Start(){
        GestureManager.Instance.OnPan += this.OnPan;
    }

    private void OnDisable() {
        GestureManager.Instance.OnPan -= this.OnPan;
    }
    
    public void OnPan(object sender, PanEventArgs args){
        Vector2 avgDelta = (args.Fingers[0].deltaPosition + args.Fingers[1].deltaPosition) / 2;

        Vector2 panDelta = Screen.dpi * this._speed * avgDelta;
        Vector3 deltaMove = new (panDelta.x, panDelta.y, 0);

        this.transform.position += deltaMove;
    }
}
