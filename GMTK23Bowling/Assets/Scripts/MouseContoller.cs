using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseContoller : MonoBehaviour
{
    [SerializeField] Dictionary<GameObject,bool> collisions = new Dictionary<GameObject, bool>();     
    Animation anim;

    void Awake()
    {
        anim = GetComponent<Animation>();
    }

    void FixedUpdate()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
        transform.position = mousePosition;
    }

    void Update()
    {   
        //start push animation
        if(Input.GetMouseButtonDown(0))
        {
            anim.Rewind();
            anim.Play();
        }
    }

    //push called from the animation event
    public void Push()
    {
        foreach (var pin in collisions)
        {
            Vector3 pinPos = pin.Key.transform.position;
            PinScript pinScript = pin.Key.GetComponent<PinScript>();
            Rigidbody2D pinBody = pin.Key.GetComponent<Rigidbody2D>();

            var from = new Vector2(transform.position.x, transform.position.y);
            var to = new Vector2(pinPos.x, pinPos.y);

            float dis = Vector2.Distance(from, to);
            float disMult = Mathf.Clamp(1/dis, 0.4f, 1f);
            float dirDegrees = FindDegree(to - from);
            float dirRadians = dirDegrees * Mathf.Deg2Rad;
            Vector2 vec = disMult * pinScript.force * new Vector2(Mathf.Sin(dirRadians), Mathf.Cos(dirRadians));
            pinBody.AddForce(vec);
            
        }
    }

    //add pin to collision
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Pin" && !collisions.ContainsKey(col.gameObject))
            collisions.Add(col.gameObject, true);
    }
    //remove pin from collisions
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Pin")
            collisions.Remove(col.gameObject); 
    }

    private float FindDegree(Vector2 vector){
        float value = (float)((Mathf.Atan2(vector.x, vector.y) / Mathf.PI) * 180f);
        if(value < 0) value += 360f;

        return value;
    }
}
