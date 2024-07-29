using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Transform arm;
    [SerializeField] Transform character;

    Animator anim;
    CharacterController mychar;

    public int hp;
    [SerializeField] public int maxHp = 100;

    public Slider hpSlider;
    public GameObject hitEffect;

    float gravity = -20f;
    float yVelocity = 0;
    [SerializeField] float jumpForce;
    bool isJump = false;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        anim = GetComponentInChildren<Animator>();
        mychar = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gm.gState != GameManager.GameState.Run) return;
        hpSlider.value = ((float)hp / (float)maxHp);
        ActiveMove();
        ActiveJump();
    }
    void ActiveMove()
    {
        Vector2 _moveAxis = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        Vector3 _moveFoward = new Vector3(arm.forward.x,0,arm.forward.z).normalized;
        Vector3 _moveRIght = new Vector3(arm.right.x, 0, arm.right.z).normalized;

        Vector3 dir = new Vector3(_moveAxis.x ,0,_moveAxis.y);
        yVelocity += gravity * Time.deltaTime;
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = yVelocity;


        //Vector3 _moveDir = (_moveFoward * _moveAxis.y) + (_moveRIght * _moveAxis.x);

        //character.forward = _moveFoward;

        //transform.position += dir * moveSpeed * Time.deltaTime;
        anim.SetFloat("MoveMotion",_moveAxis.magnitude);
        mychar.Move(dir * moveSpeed * Time.deltaTime);
    }
    void ActiveJump()
    {
        if (isJump && mychar.collisionFlags == CollisionFlags.Below)
        {
            isJump = false;
            yVelocity = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            yVelocity = jumpForce;
            isJump = true;
        }
        /*
        pos.y += gravity * Time.deltaTime;

        transform.position = new Vector3(transform.position.x,pos.y,transform.position.z);
        //mychar.SimpleMove(pos);

        gravity -= 0.5f;

        if (pos.y < 1f)
        {
            pos.y = 1f;
            gravity = 0;
        }
        */
    }

    public void Damaged(int _damage)
    {
        hp -= _damage;
        if (hp > 0)
        {
            StartCoroutine(PlayHitEffect());
        }
        Debug.Log("데미지 입음");
    }
    IEnumerator PlayHitEffect()
    {
        Debug.Log(hp);
        hitEffect.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        hitEffect.SetActive(false);
    }
}
