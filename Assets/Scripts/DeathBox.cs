using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    [SerializeField] private Transform TeleportPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ( other.CompareTag("Player") && other.TryGetComponent<HealthHandler>(out HealthHandler HH) )
        {
            HH.TakeDamage(1);
            TeleportPlayer(other.gameObject);
        }
        
    }

    void TeleportPlayer(GameObject player)
    {
        player.transform.position = TeleportPoint.position;
    }
}
