using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OstManager : MonoBehaviour
{

    [SerializeField] AudioSource menuOst;
    [SerializeField] AudioSource gameOst;
    [SerializeField] AudioSource deathOst;

    void Awake()
    {   
        DontDestroyOnLoad(gameObject);

        menuOst.volume = 1f;
        gameOst.volume = 0f;
        deathOst.volume = 0f;

    }

    void Update()
    {
        switch(SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                SceneManager.LoadScene(1);
            break;

            case 1:
                menuOst.volume = Mathf.MoveTowards(menuOst.volume, 1, 1 * Time.deltaTime);
                gameOst.volume = Mathf.MoveTowards(gameOst.volume, 0, 1 * Time.deltaTime);
                deathOst.volume = Mathf.MoveTowards(deathOst.volume, 0, 1 * Time.deltaTime);

            break;

            ////////////

            case 2:
                bool gameOver = GameObject.FindWithTag("GameController").GetComponent<GameManagerScript>().gameOver;

                menuOst.volume = Mathf.MoveTowards(menuOst.volume, 0, 1 * Time.deltaTime);
                gameOst.volume = Mathf.MoveTowards(gameOst.volume, gameOver ? 0 : 1, 1 * Time.deltaTime);
                deathOst.volume = Mathf.MoveTowards(deathOst.volume, gameOver ? 1 : 0, 1 * Time.deltaTime);

            break;

            ////////////
        }


    }
}