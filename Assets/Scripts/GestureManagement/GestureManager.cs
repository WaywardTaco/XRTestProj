using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GestureManager : MonoBehaviour
{
    [SerializeField] private TapProperty _tapProperty;
    [SerializeField] private SwipeProperty _swipeProperty;
    [SerializeField] private DragProperty _dragProperty;
    [SerializeField] private PanProperty _panProperty;
    [SerializeField] private SpreadProperty _spreadProperty;
    [SerializeField] private RotateProperty _rotateProperty;

    [SerializeField] public EventHandler<TapEventArgs> OnTap;
    [SerializeField] public EventHandler<SwipeEventArgs> OnSwipe;
    [SerializeField] public EventHandler<DragEventArgs> OnDrag ;
    [SerializeField] public EventHandler<PanEventArgs> OnPan ;
    [SerializeField] public EventHandler<SpreadEventArgs> OnSpread ;
    [SerializeField] public EventHandler<RotateEventArgs> OnRotate ;
    
    public static GestureManager Instance;
    
    [SerializeField] private Touch[] _trackedFingers;
    private float _gestureTime;
    private Vector2 _startPoint = Vector2.zero;
    private Vector2 _endPoint = Vector2.zero;

    // Start is called before the first frame update
    void Awake(){
        if(Instance == null)
            Instance = this;
        else
            GameObject.Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update(){
        switch(Input.touchCount){
            case 1: 
                GestureManager.Instance.CheckSingleFingerInput();
                break;
            case 2: 
                GestureManager.Instance.CheckDualFingerInput();
                break;
        }
    }

    private void CheckSingleFingerInput(){
        this._trackedFingers[0] = Input.GetTouch(0);
        switch(this._trackedFingers[0].phase){
            case TouchPhase.Began:  
                this._startPoint = this._trackedFingers[0].position;
                this._gestureTime = 0.0f;
                break;
            case TouchPhase.Ended:  
                this._endPoint = this._trackedFingers[0].position;
                this.CheckTap();
                this.CheckSwipe();
                break;
            default:
                this._gestureTime += Time.deltaTime;
                this.CheckDrag();
                break;
        }
    }

    private void CheckDualFingerInput(){
        this._trackedFingers[0] = Input.GetTouch(0);
        this._trackedFingers[1] = Input.GetTouch(1);

        switch(this._trackedFingers[0].phase, this._trackedFingers[1].phase){
            case (TouchPhase.Moved, TouchPhase.Moved):
                this.CheckPan();
                break;
            case (TouchPhase.Moved, _):
            case (_, TouchPhase.Moved):
                this.CheckSpread();
                this.CheckRotate();
                break;
        }
    }

    /* SINGLE FINGER PROCESSING */
    /* TAP PROCESSING */
    private void CheckTap(){
        if(
            this._gestureTime <= this._tapProperty.Time &&
            Vector2.Distance(this._startPoint, this._endPoint) <=
                (Screen.dpi * this._tapProperty.MaxDistance)
        )
            this.FireTapEvent();
    }

    private void FireTapEvent(){
        GameObject hitObject = this.GetHitObject(this._startPoint);
        TapEventArgs args = new (this._startPoint, hitObject);
        this.OnTap?.Invoke(this, args);

        if(hitObject != null){
            if(hitObject.TryGetComponent<ITappable>(out var target))
                target.OnTap(args);
        }
    }

    /* SWIPE PROCESSING */
    private void CheckSwipe(){
        if(
            this._gestureTime <= this._swipeProperty.Time &&
            Vector2.Distance(this._startPoint, this._endPoint) >=
                (Screen.dpi * this._swipeProperty.MinDistance)
        )
            this.FireSwipeEvent();
    }

    private void FireSwipeEvent(){
        GameObject hitObject = this.GetHitObject(this._startPoint);

        ESwipeDirection swipeDirection = 
            this.GetSwipeDirection(this._endPoint - this._startPoint);
        
        SwipeEventArgs args = new (swipeDirection, this._endPoint - this._startPoint, this._startPoint, hitObject);
        this.OnSwipe?.Invoke(this, args);

        if(hitObject != null){
            if(hitObject.TryGetComponent<ISwipeable>(out var target))
                target.OnSwipe(args);
        }
    }

    /* DRAG PROCESSING */
    private void CheckDrag(){
        if(this._gestureTime >= this._dragProperty.Time)
            this.FireDragEvent();
    }

    private void FireDragEvent(){
        GameObject hitObject = this.GetHitObject(this._trackedFingers[0].position);
        
        DragEventArgs args = new (this._trackedFingers[0], hitObject);
        this.OnDrag?.Invoke(this, args);

        if(hitObject != null){
            if(hitObject.TryGetComponent<IDraggable>(out var target))
                target.OnDrag(args);
        }
    }

    /* DUAL FINGER PROCESSING */
    /* PAN PROCESSING */
    private void CheckPan(){
        if(Vector2.Distance(this._trackedFingers[0].position, this._trackedFingers[1].position) <= 
            (Screen.dpi * this._panProperty.MaxDistance))
            this.FirePanEvent();
    }
    
    private void FirePanEvent(){
        Vector2 midpoint = GetMidpoint(this._trackedFingers[0].position, this._trackedFingers[1].position);
        GameObject hitObject = this.GetHitObject(midpoint);

        PanEventArgs args = new (this._trackedFingers, hitObject);
        this.OnPan?.Invoke(this, args);
        
        if(hitObject != null){
            if(hitObject.TryGetComponent<IPannable>(out var target))
                target.OnPan(args);
        }
    }

    /* SPREAD PROCESSING */
    private void CheckSpread(){
        float distanceDelta = 
            Vector2.Distance(
                this._trackedFingers[0].position,
                this._trackedFingers[1].position
            ) - Vector2.Distance(
                this.GetPreviousPoint(this._trackedFingers[0]),
                this.GetPreviousPoint(this._trackedFingers[1])
            );

        if(
            Mathf.Abs(distanceDelta) >= 
            (this._spreadProperty.MinDistanceChange)
        )
            this.FireSpreadEvent(distanceDelta);
    }

    private void FireSpreadEvent(float distanceDelta){
        Vector2 midpoint = GetMidpoint(this._trackedFingers[0].position, this._trackedFingers[1].position);
        GameObject hitObject = this.GetHitObject(midpoint);

        SpreadEventArgs args = new (this._trackedFingers, distanceDelta, hitObject);
        this.OnSpread?.Invoke(this, args);

        if(hitObject != null){
            if(hitObject.TryGetComponent<ISpreadable>(out var target))
                target.OnSpread(args);
        }
    }

    /* ROTATE PROCESSING */
    private void CheckRotate(){

        Vector2 prevDifference = this.GetPreviousPoint(this._trackedFingers[0]) - this.GetPreviousPoint(this._trackedFingers[1]);
        Vector2 currDifference = this._trackedFingers[0].position - this._trackedFingers[1].position;

        float angleDelta = Vector2.Angle(prevDifference, currDifference);

        if(
            Vector2.SqrMagnitude(currDifference) >= (Screen.dpi * this._rotateProperty.MinDistance) * (Screen.dpi * this._rotateProperty.MinDistance) &&
            angleDelta >= this._rotateProperty.MinAngleChange
        )
            this.FireRotateEvent(angleDelta, prevDifference, currDifference);
    }

    private void FireRotateEvent(float angle, Vector2 prevDifference, Vector2 currDifference){

        Vector3 cross = Vector3.Cross(prevDifference, currDifference);
        ERotateDirection rotateDir = ERotateDirection.CLOCKWISE;
        if(cross.z > 0) rotateDir = ERotateDirection.COUNTERCLOCKWISE;

        Vector2 midpoint = GetMidpoint(this._trackedFingers[0].position, this._trackedFingers[1].position);
        GameObject hitObject = this.GetHitObject(midpoint);

        RotateEventArgs args = new (this._trackedFingers, rotateDir, angle, hitObject);
        this.OnRotate?.Invoke(this, args);

        if(hitObject != null){
            if(hitObject.TryGetComponent<IRotateable>(out var target))
                target.OnRotate(args);
        }
    }

    /* HELPER FUNCTIONS */
    private GameObject GetHitObject(Vector2 screenPoint){
        GameObject hitObject = null;

        Ray ray = Camera.main.ScreenPointToRay(screenPoint);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            hitObject = hit.collider.gameObject;

        return hitObject;
    }

    private ESwipeDirection GetSwipeDirection(Vector2 rawDirection){
        if(Mathf.Abs(rawDirection.x) >= Mathf.Abs(rawDirection.y)){
            if(rawDirection.x >= 0)
                return ESwipeDirection.RIGHT;
            else
                return ESwipeDirection.LEFT;
        }
        else {
            if(rawDirection.y >= 0)
                return ESwipeDirection.UP;
            else
                return ESwipeDirection.DOWN;
            }
    }
    private Vector2 GetPreviousPoint(Touch finger){
        return finger.position - finger.deltaPosition;
    }
    private Vector2 GetMidpoint(Vector2 pointA, Vector2 pointB){
        return (pointA + pointB) / 2;
    }
}
