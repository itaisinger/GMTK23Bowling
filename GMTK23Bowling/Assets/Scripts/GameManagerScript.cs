using System;
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
    [SerializeField] TMPro.TextMeshProUGUI plusText;
    [SerializeField]TMPro.TextMeshProUGUI pinText;
    [SerializeField]TMPro.TextMeshProUGUI timeText;

    [Header("Score")] 
    [SerializeField] int currentScore=0;
    [SerializeField] int highScore=0;

    protected float Timer;
     protected float fixedTimer;
    private float levelTimer = 0f;
    public ScoreSaver scoreSaver;
    
    private float gameOverTimer = 4.5f;
    public List<GameObject> pins = new List<GameObject>();
    public bool gameOver = false;

    private void Awake() 
    {
        Application.targetFrameRate = 60;
        highScoreText.SetText("HIGH SCORE: " + scoreSaver.highScore);
        StartGame();
    }

    private void Update()
    {   
        //return to main menu
        if(gameOver)
        {
            gameOverTimer -= Time.deltaTime;
            if(Input.anyKey && gameOverTimer <= 0f)
                SceneManager.LoadScene(1);
        }

        //timer and score
        Timer += Time.deltaTime;
        fixedTimer += Time.deltaTime;
        double timeCon =Convert.ToDouble(fixedTimer*1000);
        TimeSpan time = TimeSpan.FromMilliseconds(timeCon);
        string displayTime = time.ToString("mm':'ss':'ff");
        timeText.SetText(displayTime);
        pinText.SetText(pins.Count.ToString()+" X");

        //add score
		if (Timer >= 1f)
		{
			Timer = 0f;
			currentScore= currentScore+pins.Count;
            scoreText.SetText("SCORE: "+currentScore.ToString());
            plusText.SetText("+"+pins.Count.ToString());
		}

        //level up
        levelTimer += Time.deltaTime;
        if(levelTimer >= 10f)
        {
            levelTimer = 0f;
            ballSpawner.LevelUp();
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
        // levelTimer = 4f;

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
