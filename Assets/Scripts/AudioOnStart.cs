using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOnStart : MonoBehaviour
{
    [SerializeField] private AudioClip audio;
    void Start()
    {
        Destroy(gameObject,audio.length);
    }


}
