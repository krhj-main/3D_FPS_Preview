using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    [SerializeField] Transform arm;
    public float mouseSensitivity = 400f; //���콺����

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

        MouseY = Mathf.Clamp(MouseY, -90f, 90f); //Clamp�� ���� �ּҰ� �ִ밪�� ���� �ʵ�����

        transform.localRotation = Quaternion.Euler(MouseY, MouseX, 0f);// �� ���� �Ѳ����� ���
        // ������ ���� ������ ���ʹϾ� ���Ϸ��� �����
        // �׳� Rotate �� ���� ����� ��� ���� �������� ���� ���ڿ��������� ���� �� ����
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
