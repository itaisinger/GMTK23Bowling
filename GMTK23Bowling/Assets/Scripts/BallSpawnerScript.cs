using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnerScript : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform startPos;
    [SerializeField] float minForce = 20;
    [SerializeField] float maxForce = 100;
    [SerializeField] float cooldownRemain = 2f;
    [SerializeField] float cooldownMax = 2f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldownRemain -= Time.deltaTime;
        if(cooldownRemain <= 0)
        {
            SpawnBall();
            cooldownRemain = cooldownMax;
        }
    }

    public void SpawnBall()
    {
        var ball = Instantiate(ballPrefab,startPos);
        ball.GetComponent<BallScript>().Generate(minForce,maxForce);

        minForce += 1f;
        maxForce += 1.2f;
    }

    public void Reset()
    {


    }
}
