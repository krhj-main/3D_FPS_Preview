using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAction : MonoBehaviour
{
    /*
    [SerializeField] GameObject go_Bomb;
    [SerializeField] Transform handT;
    bool hasBomb= false;

    GameObject myBomb;
    */


    public GameObject bombEffect;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnCollisionEnter(Collision _col)
    {
        GameObject eff = Instantiate(bombEffect);

        eff.transform.position = transform.position;

        Destroy(gameObject);
    }

    /*
     * void SelectBomb()
    {
        // Ư�� Ű�� ���� ����ź�� ���
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            myBomb = Instantiate(go_Bomb, handT);
            hasBomb = true;
        }
    }
    void ThrowBomb()
    {
        // ����ź�� ��� ���� ���� ���콺�� ������ ������ ���� ��ġ�� �����ϰ�
        // ���콺�� ���� �� ���� ��ġ�� ����ź�� ������.
        if (hasBomb)
        {
            if (Input.GetMouseButton(0))
            {

                //Camera.main.transform.forward;
            }
            if (Input.GetMouseButtonUp(0))
            {

            }
        }
    }
     */
}
