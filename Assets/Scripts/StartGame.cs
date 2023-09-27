using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip theme;
    private AudioSource src;
    private void Start() {
        src = GetComponent<AudioSource>();
        src.clip = theme;
        src.Play();
    }
    public void PlayButtonClicked()
    {
        // Load the game scene by name when the "Play" button is clicked
        SceneManager.LoadScene("GameScene"); // Replace with your actual game scene name
    }
}
