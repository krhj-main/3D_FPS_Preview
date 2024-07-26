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
        // 특정 키를 눌러 수류탄을 들고
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            myBomb = Instantiate(go_Bomb, handT);
            hasBomb = true;
        }
    }
    void ThrowBomb()
    {
        // 수류탄을 들고 있을 때에 마우스가 눌리고 있을때 던질 위치를 설정하고
        // 마우스를 뗐을 때 던질 위치에 수류탄을 날린다.
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
