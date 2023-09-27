using UnityEngine;
using System.Collections;

public class BrickCollision : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private Transform shardsChild;
    public GameManagerScript gameManagerScript;
    public float strength = 6f;
    private CubeAudio cubeAudio;
    private bool shouldDie = false;
    public Transform cubeAudioObj;
    private void Start()
    {
        shardsChild = transform.Find("Shards");
        particleSystem = shardsChild.GetComponent<ParticleSystem>();
        gameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>(); 
        cubeAudioObj = transform.Find("CubeAudioObj");
        cubeAudio = cubeAudioObj.GetComponent<CubeAudio>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check for collisions with the bird or any other object that can break the brick
        if (collision.relativeVelocity.magnitude > strength && !shouldDie)
        {
            shouldDie = true;
            StartCoroutine(Break());
        } else if (collision.relativeVelocity.magnitude > 1f && !shouldDie) {
            cubeAudio.Roll();
        }
    }

    private IEnumerator Break()
    {   
        cubeAudio.Damage();
        cubeAudioObj.SetParent(null);
        shardsChild.SetParent(null);
        if (gameManagerScript != null)
        {
            gameManagerScript.IncrementScore(500);
        }
        Destroy(gameObject);
        particleSystem.Play();
        yield return new WaitForSeconds(particleSystem.main.startLifetime.constantMax);
    }
}
