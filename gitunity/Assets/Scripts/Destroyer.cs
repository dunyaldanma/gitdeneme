using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public Transform player;
    public int mesafe;
    void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.y = player.position.y + mesafe;
        transform.position = newPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Border" && other.gameObject.tag != "Fall")
        {
            Destroy(other.gameObject);
        }
    }
}
