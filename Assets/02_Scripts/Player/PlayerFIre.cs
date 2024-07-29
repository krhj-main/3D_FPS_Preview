using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFIre : MonoBehaviour
{
    public GameObject firePosition;

    public GameObject bombFactory;

    public GameObject bulletEffect;     // 총알 효과
    ParticleSystem ps;              // 총기 발사 파티클

    Animator anim;

    public float throwPower = 15f;
    public int weaponPower = 5;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        ps = bulletEffect.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gm.gState != GameManager.GameState.Run) return;
        if (Input.GetMouseButtonDown(1))
        {
            GameObject _bomb = Instantiate(bombFactory);

            _bomb.transform.position = firePosition.transform.position;

            Rigidbody rb = _bomb.GetComponent<Rigidbody>();

            rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);
        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            RaycastHit hitInfo = new RaycastHit();

            if (Physics.Raycast(ray, out hitInfo))
            {
                /*
                GameObject _bullet = Instantiate(bulletEffect);
                _bullet.transform.position = hitInfo.point;
                _bullet.transform.forward = hitInfo.normal;
                */
                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    EnemyFSM eFSM = hitInfo.transform.GetComponent<EnemyFSM>();
                    eFSM.Damaged(weaponPower);
                }
                else
                {
                    bulletEffect.transform.position = hitInfo.point;
                    bulletEffect.transform.forward = hitInfo.normal;
                }
                ps.Play();
            }
            if (anim.GetFloat("MoveMotion") == 0)
            {
                anim.SetTrigger("Shoot");
            }
        }
    }
}
