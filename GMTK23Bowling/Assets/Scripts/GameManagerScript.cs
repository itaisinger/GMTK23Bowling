using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] Transform[] pinStartingPositions = new Transform[10];
    [SerializeField] GameObject pinPrefab;
    [SerializeField] BallSpawnerScript ballSpawner;
    [SerializeField] int pinsRemainNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PinDown()
    {
        pinsRemainNum--;

        if(pinsRemainNum <= 0)
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
        }

        ballSpawner.Reset();
    }

    private void GameOver()
    {


    }

    
}
