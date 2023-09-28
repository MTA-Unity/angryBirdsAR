using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public float destroyThreshold = 20f;
    private Vector3 initialPosition;
    private Rigidbody rb;
    private Transform redAudioObj;
    private BirdAudio birdAudio;
    private GameManagerScript gameManagerScript;
    private bool hasCollided = false;
    private TrailRenderer trailRenderer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        redAudioObj = transform.Find("RedAudioObj");
        birdAudio = redAudioObj.GetComponent<BirdAudio>();
        initialPosition = transform.position;
        var pigs = GameObject.FindGameObjectsWithTag("Pig");
        gameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>(); 
        var birdsLeft = gameManagerScript.DecreaseBirdCount();
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.enabled = false;

        if(pigs.Length == 0) 
        {
            gameManagerScript.Win();
        } else if(birdsLeft == 0) {
            gameManagerScript.Lose();
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool stopped = rb.velocity.magnitude == 0f && hasCollided; 
        bool isTooFar = Vector3.Distance(initialPosition, transform.position) > destroyThreshold;
        if (stopped || isTooFar)
        {
            Die();
        }
        var pigs = GameObject.FindGameObjectsWithTag("Pig");
        if(pigs.Length == 0) 
        {
            gameManagerScript.Win();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        hasCollided = true;
        // Check for collisions with level objects (e.g., bricks, pigs)
        if (collision.gameObject.CompareTag("Bolder"))
        {
            StartCoroutine(waitAndDie());
        }
    }

    private IEnumerator waitAndDie()
    {
        yield return new WaitForSeconds(0.5f);
        Die();
    }

    public void Die() {
        redAudioObj.SetParent(null);
        birdAudio.Dead();
        Destroy(gameObject);
    }

    public void Fly() {
        birdAudio.Fly();
        trailRenderer.enabled = true;
    }
}
