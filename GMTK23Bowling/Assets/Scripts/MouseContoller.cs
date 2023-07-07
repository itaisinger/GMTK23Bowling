using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseContoller : MonoBehaviour
{
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
         Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
        transform.position = mousePosition;
    }
}
