using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARContentPlacement : MonoBehaviour
{
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    private string currentLevelName;
    private GameObject objectToPlace;
    private GameObject spawnedObject;

    private Pose lastPose;

    private ARRaycastManager raycastManager;
    private Vector2 touchPos;

    static List<ARRaycastHit> hits  = new List<ARRaycastHit>();

    private void Awake() 
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }
    bool TryGetTouchPosition(out Vector2 touchPos)  
    {
        if (Input.touchCount > 0 ) 
        {
            touchPos = Input.GetTouch(0).position;
            return true;
        }

        touchPos = default;
        return false;
    }

    public string CurrentLevelName(){
        return currentLevelName;
    }

    public void setLevelToPlace(string level)
    { 
        currentLevelName = level;
        objectToPlace = level1;
        
        if(level == "level2") {
            objectToPlace = level2;
        }
        
        if(level=="level3") {
            objectToPlace = level3;
        }
    }

    void Update()
    {
        if(!TryGetTouchPosition(out Vector2 touchPos))
        {
            return;
        }

        if(raycastManager.Raycast(touchPos,hits,TrackableType.PlaneWithinPolygon)){
            var hitPose = hits[0].pose;
            hitPose.position.z += 2f;
            hitPose.position.y -= 0.5f;
            lastPose = hitPose;

            if (spawnedObject == null)
            {
                spawnedObject = Instantiate(objectToPlace, hitPose.position, hitPose.rotation);
            } 
            else 
            {
                spawnedObject.transform.position = hitPose.position;
                spawnedObject.transform.rotation = hitPose.rotation;
            }

            
        }
    }

    public void RespawnObject() {
        if (spawnedObject != null) {
            Destroy(spawnedObject);
        }
        
        spawnedObject = Instantiate(objectToPlace, lastPose.position, lastPose.rotation);
    }
}
