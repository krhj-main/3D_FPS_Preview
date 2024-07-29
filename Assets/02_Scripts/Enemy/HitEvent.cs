using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEvent : MonoBehaviour
{
    EnemyFSM eFSM;

    // Start is called before the first frame update
    void Start()
    {
        eFSM = GetComponentInParent<EnemyFSM>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AttackEvent()
    {
        eFSM.AttackAction();
    }
}
