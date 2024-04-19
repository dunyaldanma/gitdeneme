using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector3;
    [SerializeField][Range(0, 1)] float movementFactor;

    [SerializeField] float period = 2f;

    void Start()
    {
        startingPosition = transform.position;
    }


    void Update()
    {
        if (period <= Mathf.Epsilon)
        {
            return;
        }

        float cycle = Time.time / period; //contiunally growing over time
        const float tau = Mathf.PI * 2; //tau = constant value of 6.283
        float rawSinWave = Mathf.Sin(cycle * tau); // going from 1- to 1
        movementFactor = (rawSinWave + 1) / 2f; //recalculated to go from 0 to 1 so starting point is at the begining not at the middle
        Vector3 offset = movementFactor * movementVector3;
        transform.position = startingPosition + offset;
    }

}
