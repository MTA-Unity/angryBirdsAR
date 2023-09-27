using UnityEngine;
using System.Collections;

public class PigCollision : MonoBehaviour
{
    public float destroyThreshold = 50f;
    public float minSnoreInterval = 5f;
    public float maxSnoreInterval = 20f;
    public float killPower = 3f;
    public float heartPower = 1f;
    private Vector3 initialPosition;
    private ParticleSystem particleSystem;
    private Transform cloudChild;

    public GameManagerScript gameManagerScript;
    private bool shouldDie = false;

    private PigAudio pigAudio;
    public Transform pigAudioObj;

    private void Start()
    {
        cloudChild = transform.Find("Cloud");
        particleSystem = cloudChild.GetComponent<ParticleSystem>();
        gameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>(); 
        initialPosition = transform.position;
        pigAudioObj = transform.Find("PigAudioObj");
        pigAudio = pigAudioObj.GetComponent<PigAudio>();
        StartCoroutine(SnoreRandomly());
    }

    private void Update() 
    {
        bool isTooFar = Vector3.Distance(initialPosition, transform.position) > destroyThreshold;
        if (isTooFar)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check for collisions with the bird or any other object that can break the brick
        if (collision.relativeVelocity.magnitude > killPower && !shouldDie)
        {
            shouldDie = true;  
            StartCoroutine(Break());
        } else if (collision.relativeVelocity.magnitude > heartPower && !shouldDie) {
            pigAudio.Hert();
        }
    }

    private IEnumerator Break()
    {    
        cloudChild.SetParent(null);
        pigAudioObj.SetParent(null);
        pigAudio.Dead();
        if (gameManagerScript != null)
        {
            gameManagerScript.IncrementScore(5000);
        }
        Destroy(gameObject);
        particleSystem.Play();
        yield return new WaitForSeconds(particleSystem.main.startLifetime.constantMax);        
    }

    private IEnumerator SnoreRandomly()
    {
        while (true)
        {
                // Calculate a random snore interval
                float snoreInterval = Random.Range(minSnoreInterval, maxSnoreInterval);

                // Wait for the calculated interval
                yield return new WaitForSeconds(snoreInterval);
                pigAudio.Snore();
                yield return new WaitForSeconds(1f);
        }
    }
}
