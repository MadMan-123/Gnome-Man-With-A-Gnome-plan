using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] HealthHandler health;


    public void UpdateText()
    {
        text.text = $"LIVES:{health.fCurrentHealth}";
    }
}
