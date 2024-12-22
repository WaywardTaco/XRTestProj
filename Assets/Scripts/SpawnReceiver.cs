using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnReceiver : MonoBehaviour, ITappable, ISwipeable, IDraggable
{
    [SerializeField] private float _speed = 10.0f;
    private Vector3 _targetPosition = Vector3.zero;
    [SerializeField] private int _type = 0;

    private void OnEnable() {
        this._targetPosition = this.transform.position;    
    }

    public void OnTap(TapEventArgs args){
        GameObject.Destroy(this.gameObject);
    }

    public void OnSwipe(SwipeEventArgs args){
        switch(this._type){
            case 0:
                this.MovePerpendicular(args);
                break;
            case 1:
                this.MoveDiagonal(args);
                break;
        }
    }

    public void OnDrag(DragEventArgs args){
        if(args.HitObject == this.gameObject){
            Vector2 position = args.Finger.position;
            Ray ray = Camera.main.ScreenPointToRay(position);
            Vector2 worldPosition = ray.GetPoint(10);

            this._targetPosition = worldPosition;
            this.transform.position = worldPosition;
        }
    }

    private void MovePerpendicular(SwipeEventArgs args){
        Vector3 direction = Vector3.zero;

        switch(args.Direction){
            case ESwipeDirection.UP:
                direction.y = 1;
                break;
            case ESwipeDirection.RIGHT:
                direction.x = 1;
                break;
            case ESwipeDirection.DOWN:
                direction.y = -1;
                break;
            case ESwipeDirection.LEFT:
                direction.x = -1;
                break;
        }

        this._targetPosition += (direction * this._speed);
    }

    private void MoveDiagonal(SwipeEventArgs args){

    }

    private void Update(){
        if(this.transform.position != this._targetPosition)
            this.transform.position =
                Vector3.MoveTowards(this.transform.position, this._targetPosition, this._speed * Time.deltaTime);
    }
}
