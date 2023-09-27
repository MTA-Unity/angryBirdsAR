using UnityEngine;
using System;

public class SlingshotController : MonoBehaviour
{
    public Rigidbody birdPrefab; // Assign your bird prefab in the Inspector
    public Transform launchPosition; // Set this to the position where you want to launch the bird from
    public float maxDragDistance = 3f; // Maximum distance to pull the slingshot
    public float maxRotationAngle = 45f; // Maximum rotation angle in degrees
    public SoundEffectsScript soundEffects;
    private BirdScript birdScript;

    private Rigidbody currentBird;
    private bool isDragging = false;
    private Vector3 dragStartPosition;

    void Start() 
    {
        soundEffects = GameObject.FindGameObjectWithTag("SoundEffects").GetComponent<SoundEffectsScript>();
        soundEffects.Military();
    }

    void Update()
    {
        if (currentBird == null)
        {
            // Create a new bird when there's no bird in the slingshot
            CreateBird();
        }
        else if (currentBird.isKinematic == false) {
            return;
        }
        else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    OnTouchStart(touch);
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    OnTouchMove(touch);
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    OnTouchEnd(touch);
                }
            }
        }
    }

        private void OnTouchStart(Touch touch)
    {
        // Debug.Log("Touch Started");
        // When the user touches the screen, start tracking the drag
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 1f));
        touchPosition.z = 0f;
        isDragging = true;
        dragStartPosition = touchPosition;
        soundEffects.SlinshotStrech();
    }

    private void OnTouchMove(Touch touch)
    {
        // Update the bird's position while dragging
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 1f));
        touchPosition.z = 0f;
        Vector3 birdPosition = launchPosition.position + (touchPosition - dragStartPosition);

        currentBird.transform.position = birdPosition;

        // Limit the drag distance
        float dragDistance = Vector3.Distance(birdPosition, launchPosition.position);
        if (dragDistance > maxDragDistance)
        {
            Vector3 direction = (birdPosition - launchPosition.position).normalized;
            currentBird.transform.position = launchPosition.position + direction * maxDragDistance;
        }
    }

    private void OnTouchEnd(Touch touch)
    {
        // Debug.Log("Touch Ended");
        // On release, calculate the force and launch the bird
        Vector3 releasePosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 1f));
        releasePosition.z = 0f;
        // Debug.Log("launchPosition");
        // Debug.Log(launchPosition.position);
        // Debug.Log("release position");
        // Debug.Log(currentBird.transform.position);

        Vector3 launchDirection = (launchPosition.position-currentBird.transform.position);
        float launchForce = 4f + 20f * Convert.ToSingle(Math.Tanh(launchDirection.magnitude));
        launchDirection.z = 2f*launchDirection.magnitude;
        // Debug.Log("launch direction");
        // Debug.Log(launchDirection);
        // Debug.Log("launch force");
        // Debug.Log(launchForce);
        //Vector3 launchDirection = -(launchPosition.position + (dragStartPosition - releasePosition));//-(launchPosition.position - releasePosition);

        // Set the bird to be non-kinematic before applying the force
        currentBird.isKinematic = false;
        //currentBird.transform.position = launchPosition.position;
        currentBird.AddForce(launchDirection.normalized * launchForce, ForceMode.Impulse);

        // Reset the bird reference and dragging flag
        currentBird.transform.SetParent(null);
        isDragging = false;
        soundEffects.Shoot();
        birdScript.Fly();
    }

    private void CreateBird()
    {
        // Instantiate a new bird at the launch position
        currentBird = Instantiate(birdPrefab, launchPosition.position, Quaternion.identity);
        birdScript = currentBird.GetComponentInParent<BirdScript>();
        currentBird.isKinematic = true; // Set the bird to be kinematic initially
        currentBird.transform.SetParent(transform);
    }
}
