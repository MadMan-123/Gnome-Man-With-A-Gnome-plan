using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private GameObject PickUpSoundOBj;
    [SerializeField] private int iScoreToAdd = 1;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player") ) return;
        
        //add score
        if (ScoreManager.Instance)
        {
            ScoreManager.Instance.AddScore(iScoreToAdd);
        }
        
        Instantiate(PickUpSoundOBj,transform.position,Quaternion.identity, null);

        
        Destroy(gameObject);
        
        
    }
}
