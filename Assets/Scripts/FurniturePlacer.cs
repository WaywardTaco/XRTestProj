using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class FurniturePlacer : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private Vector3 camCenterWorldPositionOnPlane;
    [SerializeField] private List<GameObject> furniturePrefabs;
    [SerializeField] private float rotateSpeed = 5.0f;
    Color placeColor = Color.red;
    Color pickupColor = Color.green;
    private float rayDrawTime = 1.5f;
    private Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

    [SerializeField] public Touch[] touches = new Touch[2];
    private GameObject pickedUpObject = null;
    private int furnitureIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.raycastManager = this.gameObject.GetComponent<ARRaycastManager>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(this.pickedUpObject != null)
            this.UpdatePickedUpFurniture();
        if(Input.touchCount > 0){
            this.touches[0] = Input.GetTouch(0); 
            if(Input.touchCount > 1)
                this.touches[1] = Input.GetTouch(1); 

            if(Input.touchCount == 1){
                this.CheckTap();
            } else if(Input.touchCount >= 2){
                if(touches[0].phase == TouchPhase.Moved || touches[1].phase == TouchPhase.Moved)
                    this.CheckRotate();
            }
        }
    }

    private void CheckTap(){

        Vector2 touchPos = touches[0].position;
        if(!IsOverUI(touches[0].fingerId, touchPos)){
            GameObject hitObject = this.GetHitObject(touchPos);
            if(hitObject == null){
                if (this.pickedUpObject != null)
                    this.pickedUpObject = null;
            } else {
                if(hitObject.TryGetComponent<FurniturePickupper>(out var receiver)){
                    if(this.pickedUpObject == hitObject)
                        this.pickedUpObject = null;
                    else 
                        this.pickedUpObject = hitObject;
                } else if (hitObject.TryGetComponent<ARPlane>(out var plane)){
                    if(this.pickedUpObject == null)
                        this.PlaceObject(touchPos);
                    else
                        this.pickedUpObject = null;
                }
            }
        } else
            this.pickedUpObject = null;
    }

    private void CheckRotate(){
        if(this.pickedUpObject == null) return;

        Vector2 prevDifference = this.GetPreviousPoint(this.touches[0]) - this.GetPreviousPoint(this.touches[1]);
        Vector2 currDifference = this.touches[0].position - this.touches[1].position;

        Vector3 cross = Vector3.Cross(prevDifference, currDifference);
        float rotateDir = 1.0f;;
        if(cross.z > 0) rotateDir = -1.0f;

        this.pickedUpObject.GetComponent<FurniturePickupper>().OnRotate(rotateDir * rotateSpeed);
    }

    private void PlaceObject(Vector2 screenPos){
        if(furniturePrefabs[furnitureIndex] == null) return;

        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        Debug.DrawRay(ray.origin, ray.direction, placeColor, rayDrawTime);
        List<ARRaycastHit> hits = new();
        if(raycastManager.Raycast(ray, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon)){
            GameObject newAnchor = new("NewAnchor");
            newAnchor.transform.parent = null;
            newAnchor.transform.position = hits[0].pose.position;
            newAnchor.AddComponent<ARAnchor>();

            GameObject obj = Instantiate(furniturePrefabs[furnitureIndex], newAnchor.transform);
            obj.transform.localPosition = Vector3.zero;
            furniturePrefabs[furnitureIndex] = null;
        }
    }

    private void UpdatePickedUpFurniture(){
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        Debug.DrawRay(ray.origin, ray.direction, pickupColor, 0.1f);
        List<ARRaycastHit> hits = new();
        if(raycastManager.Raycast(ray, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon)){
            this.camCenterWorldPositionOnPlane = hits[0].pose.position;
        }

        this.pickedUpObject.transform.position = this.camCenterWorldPositionOnPlane;
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

    public void OnDropdownChange(TMP_Dropdown dropdown){
        this.furnitureIndex = dropdown.value;
    }
    private Vector2 GetPreviousPoint(Touch finger){
        return finger.position - finger.deltaPosition;
    }
}
