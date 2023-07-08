using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OstManager : MonoBehaviour
{
    GameManagerScript gameManagerScript;

    [SerializeField] AudioSource menuOst;
    [SerializeField] AudioSource gameOst;

    void Awake()
    {
        gameManagerScript=FindObjectOfType<GameManagerScript>();
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        switch(SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                if(Input.anyKey)
                {
                    
                }

                menuOst.volume = Mathf.MoveTowards(menuOst.volume, 1, 1 * Time.deltaTime);
                gameOst.volume = Mathf.MoveTowards(gameOst.volume, 0, 1 * Time.deltaTime);

            break;

            ////////////

            case 1:

                menuOst.volume = Mathf.MoveTowards(menuOst.volume, 0, 1 * Time.deltaTime);
                gameOst.volume = Mathf.MoveTowards(gameOst.volume, 1, 1 * Time.deltaTime);

            break;

            ////////////
        }


    }
}
