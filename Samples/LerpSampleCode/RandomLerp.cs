using System;
using Moein.Core;
using UnityEngine;

public class RandomLerp : MonoBehaviour
{
    public Transform target;
    public float speed = 1;
    public Vector3 addictive = Vector3.one;
    public bool isMoving = false;

    private Vector3 primaryPosition;

    void Start()
    {
        primaryPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            isMoving = !isMoving;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {

            transform.position = primaryPosition;
            addictive = addictive.Randomize(Vector3.zero, Vector3.one, false).normalized;
            t = 0;
        }

        if (isMoving) Move();
    }
    float t = 0;
    private void Move()
    {
        transform.position = transform.position.Lerp(target.position, addictive * speed * Time.deltaTime);
    }
}
