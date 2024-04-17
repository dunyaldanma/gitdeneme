using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetses : MonoBehaviour
{
    [SerializeField] AudioClip endses;

    AudioSource dioSource;

    private void Start()
    {
        dioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            dioSource.PlayOneShot(endses);
        }
    }
}
