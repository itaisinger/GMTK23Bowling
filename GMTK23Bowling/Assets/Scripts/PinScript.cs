using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinScript : MonoBehaviour
{
    Rigidbody2D rb;
    public float force;
    [SerializeField] float bounceCooldown = 0f;
    [SerializeField] float bounceCooldownMax = 0.1f;
    GameManagerScript gameManagerScript;

    void Start()
    {
      rb = GetComponent<Rigidbody2D>();
      gameManagerScript = FindObjectOfType<GameManagerScript>();
    }


    void Update()
    {
        //bounce cooldown
        if(bounceCooldown > 0f)
            bounceCooldown -= Time.deltaTime;   
    }

    //pushback
    private void OnTriggerStay2D(Collider2D other) 
    {

      if(other.tag == "PushBack")
      {
        float baseForce = other.GetComponent<PushBackScript>().force;
        float angleDegrees = other.GetComponent<PushBackScript>().angle;
        float angleRadians = angleDegrees * Mathf.Deg2Rad;
        var forceMult = Mathf.Max(1f, rb.velocity.magnitude);

        Vector2 vec = baseForce * forceMult * new Vector2(Mathf.Sin(angleRadians), Mathf.Cos(angleRadians));

        rb.AddForce(vec);
      }

      //SCARPPED!
      return;
      if(other.gameObject.CompareTag("Player")&& bounceCooldown <= 0f)
      {
          //niv code
          // Vector2 targetDir = transform.position - other.gameObject.GetComponent<Transform>().position;
          // rb.AddForce(targetDir*force,ForceMode2D.Impulse);
          // bounceCooldown = bounceCooldownMax;
      }  
      if(other.gameObject.CompareTag("Ball"))
      {
          gameManagerScript.PinDown(this.gameObject);
          Destroy(this.gameObject);
      }  
    }

    //die
    private void OnCollisionEnter2D(Collision2D other) 
    {
      if(other.gameObject.CompareTag("Ball"))
      {
          gameManagerScript.PinDown(this.gameObject);
          Destroy(this.gameObject);
      }
        
    }
}