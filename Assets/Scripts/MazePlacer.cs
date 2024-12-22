using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class MazePlacer : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    [SerializeField] private GameObject mazePrefab;
    private float rayDrawTime = 1.5f;

    [SerializeField] public Touch[] touches = new Touch[2];
    bool placedMaze = false;
    
    // Start is called before the first frame update
    void Start()
    {
        this.raycastManager = this.gameObject.GetComponent<ARRaycastManager>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0){
            this.touches[0] = Input.GetTouch(0); 
            this.CheckTap();
        }
    }

    private void CheckTap(){
        Vector2 touchPos = touches[0].position;
        if(!IsOverUI(touches[0].fingerId, touchPos)){
            GameObject hitObject = this.GetHitObject(touchPos);
            if(hitObject != null){
                if (hitObject.TryGetComponent<ARPlane>(out var plane)){
                    if(!placedMaze)
                        this.PlaceObject(touchPos);
                }
            }
        }
    }

    private void PlaceObject(Vector2 screenPos){
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        Debug.DrawRay(ray.origin, ray.direction, Color.red, rayDrawTime);
        List<ARRaycastHit> hits = new();
        if(raycastManager.Raycast(ray, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon)){
            GameObject newAnchor = new("NewAnchor");
            newAnchor.transform.parent = null;
            newAnchor.transform.position = hits[0].pose.position;
            newAnchor.AddComponent<ARAnchor>();

            GameObject obj = Instantiate(mazePrefab, newAnchor.transform);
            obj.transform.localPosition = Vector3.zero;
            placedMaze = true;
        }
    }

    private GameObject GetHitObject(Vector2 screenPoint, float maxDist = Mathf.Infinity){
        GameObject hitObject = null;

        Ray ray = Camera.main.ScreenPointToRay(screenPoint);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDist))
            hitObject = hit.collider.gameObject;

        return hitObject;
    }

    private bool IsOverUI(int pointerId, Vector2 screenPos){
        if(EventSystem.current.IsPointerOverGameObject(pointerId)) 
            return false;

        PointerEventData eventPosition = new(EventSystem.current){
            position = new Vector2(screenPos.x, screenPos.y)
        };

        List<RaycastResult> results = new();
        EventSystem.current.RaycastAll(eventPosition, results);

        return results.Count > 0;
    }

    private Vector2 GetPreviousPoint(Touch finger){
        return finger.position - finger.deltaPosition;
    }
}
