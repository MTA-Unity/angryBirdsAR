using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    public enum topLeftTooltipEnum
    {
      LOCATION,
       SCORE,
       NONE
    }
    public GameObject arPlacement;
    public GameObject summery;
    public GameObject levelPicker;
    public GameObject confirmLocationButton; // Reference to the Confirm Location button
    public Text topLeftTooltip;
    public Text birdsLeft;
    public int scoreCounter = 0;
    public int birdsCounter = 4;
    private int amo;
    public TextMeshProUGUI winStatus;
    public SoundEffectsScript soundEffects;
    public GameObject gameplayUI; // Reference to the UI elements during gameplay
    private ARContentPlacement planeController;

    private void Start() 
    {
        amo = birdsCounter;
        soundEffects = GameObject.FindGameObjectWithTag("SoundEffects").GetComponent<SoundEffectsScript>();
        planeController = arPlacement.GetComponent<ARContentPlacement>();
        birdsLeft.text = "birds left " + birdsCounter;
    }
    private void Awake()
    {
        chooseLevelState();
    }

    public void placeLevel() {
        // Initialize the game state
        arPlacement.SetActive(true);
        confirmLocationButton.SetActive(true);
        setTooltipPurpose(topLeftTooltipEnum.LOCATION);
        levelPicker.SetActive(false);
    }

    public void chooseLevelState() {
        levelPicker.SetActive(true);
        arPlacement.SetActive(false);
        confirmLocationButton.SetActive(false);
        setTooltipPurpose(topLeftTooltipEnum.NONE);
        summery.SetActive(false);
        gameplayUI.SetActive(false);
    }

    public void ConfirmLocation()
    {
        // Lock the AR objects and transition to the gameplay scene
        arPlacement.SetActive(false);
        confirmLocationButton.SetActive(false);
        setTooltipPurpose(topLeftTooltipEnum.SCORE);
        gameplayUI.SetActive(true);
    }

    public void IncrementScore(int append)
    {
        scoreCounter += append;
        topLeftTooltip.text = "score: " + scoreCounter;
    }

    public void FinishLevel()
    {
        HighScoreManager highScoreManager = FindObjectOfType<HighScoreManager>();
        highScoreManager.SaveHighScore(scoreCounter);
        SceneManager.LoadScene("MenuScene");
    }

    public void RestartLevel() 
    {
       arPlacement.SetActive(true);
       planeController.RespawnObject();
       ConfirmLocation();
       summery.SetActive(false);
       birdsCounter = amo;
       scoreCounter = 0;
    }

    public int DecreaseBirdCount() {
        birdsLeft.text = "birds left " + birdsCounter;
        return birdsCounter--;
    }

    public void Win() {
        soundEffects.Win();
        ShowSummery();
        winStatus.SetText("You Win!");
    }

    public void Lose(){
        soundEffects.Lose();
        ShowSummery();
        winStatus.SetText("You Lose!");
    }

    private void ShowSummery() {
        
        summery.SetActive(true);
        gameplayUI.SetActive(false);
        HighScoreManager highScoreManager = FindObjectOfType<HighScoreManager>();
        highScoreManager.SaveHighScore(scoreCounter);

    }

    private void setTooltipPurpose(topLeftTooltipEnum purpose) {
        if( purpose == topLeftTooltipEnum.LOCATION) {
            topLeftTooltip.text = "pick a plane and lock to start";
            topLeftTooltip.fontSize = 56;
        } 
        else if( purpose == topLeftTooltipEnum.SCORE) {
            topLeftTooltip.text = "score: " + scoreCounter;
        } else {
            topLeftTooltip.text = "";
        }
    }
}
