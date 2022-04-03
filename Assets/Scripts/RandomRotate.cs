using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotate : MonoBehaviour
{
    int rotation;
    [SerializeField] float rotateSpeed = 10f;


    // Start is called before the first frame update
    void Start()
    {
        rotation = Random.Range(0, 4);
    }

    // Update is called once per frame
    void Update()
    {
        switch (rotation)
        {
            case 0:
                RotateClockwise();
                break;
            case 1:
                RotateCounterClockwise();
                break;
            case 2:
                RotateUp();
                break;
            case 3:
                RotateDown();
                break;
        }
    }

    void RotateClockwise()
    {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * -rotateSpeed);
    }

    void RotateCounterClockwise()
    {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * rotateSpeed);
    }

    void RotateUp()
    {
        transform.RotateAround(transform.position, transform.forward, Time.deltaTime * -rotateSpeed);
    }

    void RotateDown()
    {
        transform.RotateAround(transform.position, transform.forward, Time.deltaTime * rotateSpeed);
    }
}
