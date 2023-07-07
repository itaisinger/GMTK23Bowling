using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float angleRadians;
    [SerializeField] float angleDegrees;
    [SerializeField] float minForce = 20;
    [SerializeField] float maxForce = 100;
    [SerializeField] float minAngle = 0;
    [SerializeField] float maxAngle = 360;
    [SerializeField] float bounceCooldown = 0f;
    [SerializeField] float bounceCooldownMax = 0.1f;
    [SerializeField] Transform startPos;

    Rigidbody2D myBody;


    // Start is called before the first frame update
    void Start()
    {
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
    }

    //bounce
    void OnCollisionEnter2D(Collision2D wall)
    {
        if(wall.transform.tag == "Wall" && bounceCooldown <= 0f)
        {
            angleDegrees *= -1;
            bounceCooldown = bounceCooldownMax;
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

    public void Generate(float min,float max)
    {
        angleDegrees = Random.Range(minAngle,maxAngle);
        speed = Random.Range(min,max);
    }

}
