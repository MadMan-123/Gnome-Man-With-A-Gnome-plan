using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    public float fMaxHealth  = 3;
    public float fCurrentHealth = 0;
    public bool bIsPlayer  = false;
    public bool bShouldDestroy = true;
    public bool bActiveSet = false;
    public Action<HealthHandler> OnDeath;

    [SerializeField] HealthUIManager UIManager;
    [Header("Death")] [SerializeField] private GameObject DeathAudioObject;
    [SerializeField] private GameObject HurtAudio;
    [SerializeField] private GameObject DeathScreen;
    
  

    private void Start()
    {
        ResetHealth();
        if(UIManager)
            UIManager.UpdateText();
    }

    public void ResetHealth()
    {
        fCurrentHealth = fMaxHealth;
    }

    public void TakeDamage(float fDamage)
    {
        
        Instantiate(HurtAudio,transform.position,Quaternion.identity, null);

        fCurrentHealth -= fDamage;
        fCurrentHealth = Mathf.Clamp(fCurrentHealth, 0, fMaxHealth);

        
        if(bIsPlayer) 
            UIManager.UpdateText();
        
        //if health less than 0, Run Death Method
        if (fCurrentHealth <= 0)
        {
            Instantiate(DeathAudioObject,transform.position,Quaternion.identity, null);
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        if(OnDeath != null)
            OnDeath?.Invoke(this);

        if(bIsPlayer)
            DeathScreen.SetActive(true);
        
        if (bShouldDestroy)
        {
            Destroy(gameObject);
        }
        else if(bActiveSet)
        {
            gameObject.SetActive(false);
        }


    }

    public void Heal(float fHeal)
    {
        fCurrentHealth += Mathf.Clamp(fHeal, 0, fMaxHealth);
    }
    



}
