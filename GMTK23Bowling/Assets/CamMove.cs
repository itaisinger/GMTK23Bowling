using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CamMove : MonoBehaviour
{
    public float speed;
    private void Awake() {
        Application.targetFrameRate = 60;
    }
    void Update()
    {
        transform.position=transform.position+new Vector3(speed/100,0,0);
        if(Input.anyKey)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
