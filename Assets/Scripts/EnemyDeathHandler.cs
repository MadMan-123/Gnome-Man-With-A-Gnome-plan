using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathHandler : MonoBehaviour
{
    [SerializeField] private GameObject DeathAudioObj;
    [SerializeField] private int iScoreToAdd = 5;
    public bool bIgnore = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ( !bIgnore && other.CompareTag("Player") && other.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            //set EnemyAi to ignore damage
            if (transform.parent.TryGetComponent<EnemyAI>(out var enemyAI))
            {
                enemyAI.bIgnore = true;
            }
            //spawn in death noise
            Instantiate(DeathAudioObj,transform.position,Quaternion.identity, null);

            //add score
            ScoreManager.Instance.AddScore(iScoreToAdd);

            //add force
            rb.AddForce(new Vector2(0,1000),ForceMode2D.Force);
            //destroy the enemy
            Destroy(transform.parent.gameObject);
            
        }
    }
}
