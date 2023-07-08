using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] Transform[] pinStartingPositions = new Transform[10];
    [SerializeField] GameObject pinPrefab;
    [SerializeField] BallSpawnerScript ballSpawner;
    [SerializeField] int pinsRemainNum = 0;
    [SerializeField]GameObject gameOverPanel;
    public List<GameObject> pins = new List<GameObject>();

    private void Awake() 
    {
        Application.targetFrameRate = 60;
        StartGame();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void PinDown(GameObject pin)
    {
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
            pinsRemainNum++;
            var pin = Instantiate(pinPrefab);
            pin.transform.position = pinStartingPositions[i].position;
            pins.Add(pin);
        }
    }

    private void GameOver()
    {
        ballSpawner.enabled = false;
        gameOverPanel.SetActive(true);
    } 
}
