using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    float speed;
    float angleRadians;
    float angleDegrees;
    [SerializeField] float forceMult = 1;
    [SerializeField] float minAngle = 0;
    [SerializeField] float maxAngle = 360;
    float bounceCooldown = 0f;
    float bounceCooldownMax = 0.1f;
    [SerializeField] Transform startPos;
    [SerializeField] AudioSource bounceSfx;
    [SerializeField] Transform shadowTransform;

    Quaternion initRot;
    Rigidbody2D myBody;


    // Start is called before the first frame update
    void Start()
    {
        initRot = transform.rotation;
        myBody = GetComponent<Rigidbody2D>();
    }   

    // Update is called once per frame
    void Update()
    {
        angleRadians = Mathf.Deg2Rad * angleDegrees;
        Vector2 dir = new Vector2(Mathf.Sin(angleRadians), Mathf.Cos(angleRadians));
        myBody.velocity = dir*speed;

        if(bounceCooldown > 0f)
            bounceCooldown -= Time.deltaTime; 

        //disable shadow turn
        shadowTransform.rotation = initRot;  
    }

    //bounce
    void OnCollisionEnter2D(Collision2D wall)
    {
        if(wall.transform.tag == "Wall" && bounceCooldown <= 0f)
        {
            angleDegrees *= -1;
            bounceCooldown = bounceCooldownMax;
            bounceSfx.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "End")
        {
            // transform.position = startPos.position;
            // Generate();
            Destroy(gameObject);
        }
    }

    public void Generate(float min,float max, float angleRange)
    {
        angleDegrees = Random.Range(180-angleRange, 180 + angleRange);
        speed = forceMult * Random.Range(min,max);
        Debug.Log(speed);
    }

}
