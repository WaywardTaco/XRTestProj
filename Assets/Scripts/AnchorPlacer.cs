using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;

public class AnchorPlacer : MonoBehaviour
{
    ARAnchorManager anchorManager;
    private float forwardOffset = 0.1f;
    [SerializeField] private List<GameObject> prefabsToAnchor;
    [SerializeField] private bool debugSpawnPointCloud;
    [SerializeField] private GameObject pointCloudPrefab;
    [SerializeField] private List<GameObject> spawnedObjects = new List<GameObject>();

    private int spawnIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        anchorManager = GetComponent<ARAnchorManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(debugSpawnPointCloud){
            GameObject.Instantiate(pointCloudPrefab);
            debugSpawnPointCloud = false;
        }

        
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){
        
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = touch.position;

            if(!IsOverUI(touch.fingerId, touchPos)){
                GameObject hitObject = this.GetHitObject(touchPos);
                if(hitObject != null){
                    Debug.Log("Hit: " + hitObject.name);
                    TapDeleteReceiver receiver = hitObject.GetComponent<TapDeleteReceiver>();
                    if(receiver != null){
                        receiver.ReceiveTap();
                    }

                } else {
                    Vector3 spawnPos = Camera.main.ScreenPointToRay(touchPos).GetPoint(forwardOffset);
                    AnchorObject(spawnPos);
                    
                }
            }

        }
    }

    private bool IsOverUI(int pointerId, Vector2 screenPos){
        if(EventSystem.current.IsPointerOverGameObject(pointerId)) return false;

        PointerEventData eventPosition = new PointerEventData(EventSystem.current);
        eventPosition.position = new Vector2(screenPos.x, screenPos.y);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventPosition, results);

        return results.Count > 0;
    }

    public void AnchorObject(Vector3 worldPos){
        GameObject newAnchor = new GameObject("NewAnchor");
        newAnchor.transform.parent = null;
        newAnchor.transform.position = worldPos;
        newAnchor.AddComponent<ARAnchor>();

        GameObject obj = Instantiate(prefabsToAnchor[spawnIndex], newAnchor.transform);
        obj.transform.localPosition = Vector3.zero;

        this.spawnedObjects.Add(newAnchor);
    }

    private GameObject GetHitObject(Vector2 screenPoint){
        GameObject hitObject = null;

        Ray ray = Camera.main.ScreenPointToRay(screenPoint);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            hitObject = hit.collider.gameObject;

        return hitObject;
    }

    public void SliderCallback(float value){
        this.forwardOffset = (value * 1.9f) + 0.1f;
        Debug.Log(this.forwardOffset);
    }

    public void ToggleCallback(bool value){
        if(this.gameObject.GetComponent<ARPointCloudManager>() != null)
            this.gameObject.GetComponent<ARPointCloudManager>().enabled = value;
    }

    public void DeleteObjectsCallback(){
        foreach(GameObject obj in this.spawnedObjects){
            Destroy(obj);
        }
    }

    public void SwitchSpawnObjectCallback(TMP_Dropdown change){
        this.spawnIndex = change.value;
        Debug.Log(change.value);
    }
}
