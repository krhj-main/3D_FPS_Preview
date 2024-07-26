using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 mouseAxis;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mouseAxis = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X")) ;

        Camera.main.transform.eulerAngles = mouseAxis;
    }
}
