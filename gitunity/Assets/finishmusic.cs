using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishmusic : MonoBehaviour
{
    [SerializeField] Player bitart�k;
    [SerializeField] AudioClip bit;
    AudioSource audio;
    bool bitti= false;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    private void Update()
    {
        bitti = bitart�k.isFinish;
        if( bitti== true)
        {
            audio.PlayOneShot(bit);
        }
    }
}
