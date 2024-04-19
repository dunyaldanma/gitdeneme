using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraaScript : MonoBehaviour
{
    public Transform player;
    public int mesafe;
    void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.y = player.position.y + mesafe;
        transform.position = newPosition;
    }
}
