using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinScript : MonoBehaviour
{
    Rigidbody2D rb;
    public float force;
    public bool isMoving= false;
    void Start()
    {
      rb=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(rb.velocity==new Vector2(0,0))
        {
            isMoving=false;
        }
        else
        {
            isMoving=true;
        }
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Wall"))
        {
        }
        else if(other.gameObject.CompareTag("Player"))
        {
        Vector2 targetDir = other.gameObject.GetComponent<Transform>().position - transform.position;
        rb.AddForce(targetDir*force,ForceMode2D.Impulse);
        }
        else if(other.gameObject.CompareTag("Pin"))
        {
            if(isMoving==false)
            {
            Vector2 targetDir = other.gameObject.GetComponent<Transform>().position - transform.position;
            rb.AddForce(targetDir*force,ForceMode2D.Impulse);
            }
            else
            {
              rb.velocity= new Vector2(0,0);
            }
        }
      }
}
