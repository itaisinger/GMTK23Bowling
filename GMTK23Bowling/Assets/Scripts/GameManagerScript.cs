using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] Transform[] pinStartingPositions = new Transform[10];
    [SerializeField] GameObject pinPrefab;
    [SerializeField] int pinsRemainNum = 10;

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
        //create pins
        for(var i=0; i < 10; i++)
        {
            Instantiate(pinPrefab,pinStartingPositions[i]);
        }

    }

    private void GameOver()
    {


    }

    
}
