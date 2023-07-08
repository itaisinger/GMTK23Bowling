using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseContoller : MonoBehaviour
{
    [SerializeField] Dictionary<GameObject,bool> collisions = new Dictionary<GameObject, bool>();     
    
    //unused
    int stackSize=10;
    List<Vector2> vectors = new List<Vector2>();
    Vector3 lastPos;

    void Awake()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
        lastPos = transform.position;

        vectors.Add(lastPos);
    }

    void FixedUpdate()
    {
        //// unused ////

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
        transform.position = mousePosition;

        //add vector to stack
        Vector2 newVec = mousePosition - lastPos;
        lastPos = mousePosition;
        vectors.Add(newVec);

        //pop
        if(vectors.Count > stackSize)
            vectors.RemoveAt(0);
    }

    void Update()
    {   
        //push!
        if(Input.GetMouseButtonDown(0))
        {
            // Debug.Log(collisions.Count);

            foreach (var pin in collisions)
            {
                Vector3 pinPos = pin.Key.transform.position;
                PinScript pinScript = pin.Key.GetComponent<PinScript>();
                Rigidbody2D pinBody = pin.Key.GetComponent<Rigidbody2D>();

                var from = new Vector2(transform.position.x, transform.position.y);
                var to = new Vector2(pinPos.x, pinPos.y);

                float dis = Vector2.Distance(from, to);
                float dirDegrees = FindDegree(to - from);
                float dirRadians = dirDegrees * Mathf.Deg2Rad;
                Vector2 vec = (1/1) * pinScript.force * new Vector2(Mathf.Sin(dirRadians), Mathf.Cos(dirRadians));
                pinBody.AddForce(vec);
                Debug.Log(dirDegrees);
            }
        }
    }

    //unused
    public Vector2 GetPushVector()
    {
        Vector2 ret = new Vector2(0f,0f);

        //loop through and sum all the vectors
        for(var i=0; i < vectors.Count; i++)
        {
            ret += vectors[i];
        }

        ret /= vectors.Count;

        return ret;
    }

    //add pin to collision
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Pin")
            collisions.Add(col.gameObject, true);
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Pin")
            collisions.Remove(col.gameObject); 
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag == "Pin")
        {
            var vec = transform.position - col.transform.position;
            Debug.Log(FindDegree(vec));
        }

    }

    private float FindDegree(Vector2 vector){
        float value = (float)((Mathf.Atan2(vector.x, vector.y) / Mathf.PI) * 180f);
        if(value < 0) value += 360f;

        return value;
    }
}
