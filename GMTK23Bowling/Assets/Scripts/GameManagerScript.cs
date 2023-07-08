using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] Transform[] pinStartingPositions = new Transform[10];
    [SerializeField] GameObject pinPrefab;
    [SerializeField] BallSpawnerScript ballSpawner;
    [SerializeField] int pinsRemainNum = 11;
    [SerializeField]GameObject panel;
    public List<GameObject> pins = new List<GameObject>();

private void Awake() {
    Application.targetFrameRate = 60;
}
    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(panel.activeInHierarchy)
        {
            if(Input.anyKey)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
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
            var pin = Instantiate(pinPrefab);
            pin.transform.position = pinStartingPositions[i].position;
            pins.Add(pin);
        }
    }

    private void GameOver()
    {
        ballSpawner.enabled= false;
        panel.SetActive(true);
    } 
}
