using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnerScript : MonoBehaviour
{
    [SerializeField] List<GameObject> ballPrefabs = new List<GameObject>();
    [SerializeField] Transform startPos;
    [SerializeField] float cooldownRemain = 2f;
    [SerializeField] float startCooldown = 4f;

    float baseMin;
    float baseMax;

    [Header ("Stats")]
    [SerializeField] int level = 1;
    [SerializeField] float angleRange = 0;
    [SerializeField] int spawnVariety = 1;
    [SerializeField] float cooldownMax = 2f;
    [SerializeField] float minForce = 20;
    [SerializeField] float maxForce = 100;

    // Start is called before the first frame update
    void Awake()
    {
        cooldownRemain = startCooldown;
        baseMin = minForce;
        baseMax = maxForce;
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Space))
        //     LevelUp();

        cooldownRemain -= Time.deltaTime;
        if(cooldownRemain <= 0)
        {
            SpawnBall();
            cooldownRemain = cooldownMax;
        }
    }

    public void SpawnBall()
    {
        var ball = Instantiate(ballPrefabs[Random.Range(0,spawnVariety)]);
        ball.transform.position = startPos.position;
        ball.GetComponent<BallScript>().Generate(minForce,maxForce,angleRange);

        minForce += 1f;
        maxForce += 1.2f;
        cooldownMax *= 0.99f;
    }

    public void Reset()
    {
        level = 1;
        minForce = baseMin;
        maxForce = baseMax;
        cooldownRemain = cooldownMax;
    }

    public void LevelUp()
    {
        switch(level)
        {
            case 0: angleRange += 30;               break;
            case 1: spawnVariety++;                 break;
            case 2: maxForce += 3; minForce -= 5f;  break;
            case 3: cooldownMax *= 0.6f;            break;
            case 4: maxForce += 5;  minForce += 3;  break;
            case 5: spawnVariety++;                 break;
            case 6: angleRange += 30;               break;
            default: cooldownMax *= 0.9f;           break;
        }

        level++;

    }
}
