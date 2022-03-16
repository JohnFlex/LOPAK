using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AllSoundManager : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField]
    AudioClip uiNavigationClip;

    EventSystem eventSystem;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    
}
