using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    [SerializeField] private int iCurrentScore = 0;

    [SerializeField] TextMeshProUGUI ScoreUIText;
    
    

    void Awake()
    {
        //singleton boilerplate 
        DontDestroyOnLoad(gameObject);

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        
        UpdateTextOBJ();

    }
    
    public void AddScore(int iScoreToAdd)
    {
        iCurrentScore += iScoreToAdd;

        //change the text
        UpdateScore();
    }
    void UpdateTextOBJ()
    {
        
        Debug.Log("Updating TextOBJ");
        ScoreUIText = GameObject.FindWithTag("ScoreUI").GetComponent<TextMeshProUGUI>();
        Debug.Log($"{ScoreUIText.name} is the new text UI");
        UpdateScore();
    }
    public void UpdateScore()
    {
        if (!ScoreUIText)
            UpdateTextOBJ();
        
        ScoreUIText.text = $"SCORE:{iCurrentScore}";
    }

    public int GetScore() => iCurrentScore;

}