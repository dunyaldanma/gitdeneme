using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isaliceses : MonoBehaviour
{
    [SerializeField] Player lamk;
    [SerializeField] AudioClip öll;
    AudioSource audio;
    bool life = true;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    private void Update()
    {
        life = lamk.isAlive;
        if (life == false)
        {
            audio.PlayOneShot(öll);
        }
    }
}
