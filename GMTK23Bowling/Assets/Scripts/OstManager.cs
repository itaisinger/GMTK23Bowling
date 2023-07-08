using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OstManager : MonoBehaviour
{

    [SerializeField] AudioSource menuOst;
    [SerializeField] AudioSource gameOst;

    void Awake()
    {   
        DontDestroyOnLoad(gameObject);

        menuOst.volume = 1f;
        gameOst.volume = 0f;

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

            break;

            ////////////

            case 2:

                menuOst.volume = Mathf.MoveTowards(menuOst.volume, 0, 1 * Time.deltaTime);
                gameOst.volume = Mathf.MoveTowards(gameOst.volume, 1, 1 * Time.deltaTime);

            break;

            ////////////
        }


    }
}