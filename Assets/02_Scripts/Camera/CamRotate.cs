using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    [SerializeField] Transform arm;
    public float mouseSensitivity = 400f; //마우스감도

    private float MouseY;
    private float MouseX;

    void Update()
    {
        if (GameManager.gm.gState != GameManager.GameState.Run) return;
        Rotate();
    }
    private void Rotate()
    {

        MouseX += Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;

        MouseY -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        MouseY = Mathf.Clamp(MouseY, -90f, 90f); //Clamp를 통해 최소값 최대값을 넘지 않도록함

        transform.localRotation = Quaternion.Euler(MouseY, MouseX, 0f);// 각 축을 한꺼번에 계산
        // 짐벌락 현상 때문에 쿼터니언 오일러를 사용함
        // 그냥 Rotate 로 값을 사용할 경우 각도 움직임이 심히 부자연스러움을 느낄 수 있음
    }

    /*
    // Update is called once per frame
    void Update()
    {
        Vector3 mouseAxis = new Vector3(Input.GetAxis("Mouse Y") * -1, Input.GetAxis("Mouse X"),0);

        mouseAxis.x = Mathf.Clamp(mouseAxis.x,-30f, 30f);
        mouseAxis.y = Mathf.Clamp(mouseAxis.y,-90f, 90f);

        transform.localRotation = Quaternion.Euler(mouseAxis * Time.deltaTime * camSpeed);
    }
    */
}
