using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public float rotSpeed;

    float mx = 0;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gm.gState != GameManager.GameState.Run) return;
        Debug.DrawRay(transform.position,Camera.main.transform.forward*10f,Color.red);
        
        float mouse_X = Input.GetAxis("Mouse X");

        mx += mouse_X * rotSpeed * Time.deltaTime;

        //transform.eulerAngles = new Vector3(0,mx,0);
        transform.localRotation = Quaternion.Euler(0,mx,0);
        
    }
}
