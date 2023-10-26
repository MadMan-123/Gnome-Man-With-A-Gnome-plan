using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform PointA, PointB;
    [SerializeField] private float fSpeed;
    [SerializeField] private Transform Target = null;
    private bool bSwitch = true ;
    public bool bIgnore = false;


    private void Start()
    {
        transform.position = PointA.position;

        Target = PointB;
    }



    void Update()
    {
        //lerp Between B And A
        transform.position = Vector3.Lerp( transform.position ,Target.position, Time.time / fSpeed);

        if ((Target.position - transform.position).sqrMagnitude < 0.5f)
        {
            if (bSwitch)
            {
                Target = PointA;
                bSwitch = false;
            }
            else
            {
                Target = PointB;
                bSwitch = true;
            }
        }
    }

  
    
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(PointA.position,1f);
            Gizmos.DrawWireSphere(PointB.position,1f);

        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            EnemyDeathHandler deathHandler;
            if (!bIgnore &&  other.CompareTag("Player") &&
                other.TryGetComponent<HealthHandler>(out HealthHandler HH) && 
                other.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            {
                if (transform.GetChild(0).TryGetComponent<EnemyDeathHandler>(out deathHandler ))
                {
                    deathHandler.bIgnore = true;
                }
                rb.AddForce(new Vector2(0,1000),ForceMode2D.Force);
                HH.TakeDamage(1);

                deathHandler.bIgnore = false;
            }
        
        }
}
