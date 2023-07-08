using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] Transform[] pinStartingPositions = new Transform[10];
    [SerializeField] GameObject pinPrefab;
    [SerializeField] BallSpawnerScript ballSpawner;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] AudioSource pinSfx;

    [Header("UI")] 
    [SerializeField] TMPro.TextMeshProUGUI scoreText;
    [SerializeField] TMPro.TextMeshProUGUI highScoreText;
    [SerializeField] TMPro.TextMeshProUGUI pinsText;

    [Header("Score")] 
    [SerializeField] int currentScore=0;
    [SerializeField] int highScore=0;
    protected float Timer;
    protected float levelTimer = 0f;
    private float gameOverTimer = 4.5f;
    public ScoreSaver scoreSaver;
    
    public List<GameObject> pins = new List<GameObject>();
    private bool gameOver = false;

    private void Awake() 
    {
        Application.targetFrameRate = 60;
        highScoreText.SetText("HIGH SCORE: "+scoreSaver.highScore);   
        StartGame();
    }

    private void Update()
    {
        if(gameOver)
        {
            gameOverTimer -= Time.deltaTime;
            
            if(Input.anyKey && gameOverTimer <= 0)
                SceneManager.LoadScene(1);
        }
        Timer += Time.deltaTime;
        levelTimer += Time.deltaTime;

		if (Timer >= 1)
		{
			Timer = 0f;
			currentScore= currentScore+pins.Count;
            scoreText.SetText("SCORE: "+ currentScore.ToString());
            pinsText.SetText("+"+pins.Count.ToString());
		}

        if(levelTimer >= 10)
        {
            ballSpawner.LevelUp();
            levelTimer = 0f;
        }
    }

    public void PinDown(GameObject pin)
    {
        pinSfx.Play();
        pins.Remove(pin);
        if(pins.Count <= 0)
            GameOver();
    }

    private void StartGame()
    {
        Debug.Log("Starting game");
        //create pins
        for(var i=0; i < 10; i++)
        {
            var pin = Instantiate(pinPrefab);
            pin.transform.position = pinStartingPositions[i].position;
            pins.Add(pin);
        }
    }

    private void GameOver()
    {
        gameOver = true;
        ballSpawner.enabled = false;
        gameOverPanel.SetActive(true);
        gameOverPanel.GetComponent<Animation>().Play("Lost");
        if(currentScore>scoreSaver.highScore)
        {
            scoreSaver.highScore= currentScore;
            highScoreText.SetText("HIGH SCORE: "+scoreSaver.highScore);         
        }
    } 
}
