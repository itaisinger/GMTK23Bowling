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
      rb=GetComponent<Rigidbody2D>();
      gameManagerScript = FindObjectOfType<GameManagerScript>();
    }
    void Update()
    {
    
        if(bounceCooldown > 0f)
            bounceCooldown -= Time.deltaTime;   
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
    if(other.gameObject.CompareTag("Player")&& bounceCooldown <= 0f)
        {
        Vector2 targetDir = transform.position - other.gameObject.GetComponent<Transform>().position;
        rb.AddForce(targetDir*force,ForceMode2D.Impulse);
         bounceCooldown = bounceCooldownMax;
        }    
      }
      private void OnCollisionEnter2D(Collision2D other) 
      {
        if(other.gameObject.CompareTag("Ball"))
        {
          gameManagerScript.PinDown();
          Destroy(this.gameObject);
        }
          
      }
}