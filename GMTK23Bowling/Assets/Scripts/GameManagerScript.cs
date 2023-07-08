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
    [SerializeField] TMPro.TextMeshProUGUI pinsText;

    [Header("")] 
    [SerializeField] int score=0;
    
    public List<GameObject> pins = new List<GameObject>();
    private bool gameOver = false;

    private void Awake() 
    {
        Application.targetFrameRate = 60;
        StartGame();
    }

    private void Update()
    {
        if(gameOver && Input.anyKey)
        {
            SceneManager.LoadScene(0);
        }

        //update ui
        pinsText.text = pins.Count.ToString();
        scoreText.text = score.ToString();
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
    } 
}
