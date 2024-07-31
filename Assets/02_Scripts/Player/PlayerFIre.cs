using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerFIre : MonoBehaviour
{
    public GameObject firePosition;

    public GameObject bombFactory;

    public GameObject bulletEffect;     // 총알 효과
    ParticleSystem ps;              // 총기 발사 파티클

    Animator anim;

    public float throwPower = 15f;
    public int weaponPower = 5;
    public TextMeshProUGUI textWeaponMode;
    public Transform muzzle;
    public GameObject[] muzzleEffect;

    public GameObject weapon1, hud1;
    public GameObject weapon2, hud2;



    //무기 모드에 관한 열거형변수
    enum WeaponMode
    {
        Normal,
        Sniper,
    }
    WeaponMode wMode;

    // 줌 효과 확인변수
    bool zoomMode = false;
    public GameObject zoomView;
    // Start is called before the first frame update
    void Start()
    {
        zoomView.SetActive(false);
        weapon1.SetActive(true);
        hud1.SetActive(true);
        weapon2.SetActive(false);
        hud2.SetActive(false);
        textWeaponMode.text = "Normal";
        wMode = WeaponMode.Normal;
        anim = GetComponentInChildren<Animator>();
        ps = bulletEffect.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gm.gState != GameManager.GameState.Run) return;


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            zoomView.SetActive(false);
            weapon1.SetActive(true);
            hud1.SetActive(true);
            weapon2.SetActive(false);
            hud2.SetActive(false);
            textWeaponMode.text = "Normal";
            wMode = WeaponMode.Normal;
            Camera.main.fieldOfView = 60f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            zoomView.SetActive(false);
            weapon1.SetActive(false);
            hud1.SetActive(false);
            weapon2.SetActive(true);
            hud2.SetActive(true);
            textWeaponMode.text = "Sniper";
            wMode = WeaponMode.Sniper;
        }
        if (Input.GetMouseButtonDown(1))
        {
            switch (wMode)
            {
                case WeaponMode.Normal:
                    

                    GameObject _bomb = Instantiate(bombFactory);

                    _bomb.transform.position = firePosition.transform.position;

                    Rigidbody rb = _bomb.GetComponent<Rigidbody>();

                    rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);

                    break;

                case WeaponMode.Sniper:
                    if (!zoomMode)
                    {
                        zoomView.SetActive(true);
                        Camera.main.fieldOfView = 15f;
                        zoomMode = true;
                    }
                    else
                    {
                        zoomView.SetActive(false);
                        Camera.main.fieldOfView = 60f;
                        zoomMode = false;
                    }
                    break;
            }
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
                StartCoroutine(ShootEffectOn(0.05f));
                anim.SetTrigger("Shoot");
            }
        }
    }
    IEnumerator ShootEffectOn(float _delay)
    {
        int _muzzleIdx = Random.Range(0,muzzleEffect.Length);

        muzzleEffect[_muzzleIdx].SetActive(true);
        yield return new WaitForSeconds(_delay);
        muzzleEffect[_muzzleIdx].SetActive(false);
    }
}
