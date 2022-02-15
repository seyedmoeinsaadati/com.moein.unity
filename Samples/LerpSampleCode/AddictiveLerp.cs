using System;
using Moein.Core;
using UnityEngine;

public class AddictiveLerp : MonoBehaviour
{
    public Transform target;
    public float speed = 1;
    public bool isMoving = false;

    private Vector3 primaryPosition;
    public Curve3 addictive;

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
            t = 0;
        }

        if (isMoving) Move();
    }
    float t = 0;
    private void Move()
    {

        // transform.position = transform.position.Lerp(target.position, addictive * speed * Time.deltaTime);
        Vector3 position = transform.position.Lerp(primaryPosition, target.position, addictive.Evaluate(t));
        transform.position = position;
        t += speed * Time.deltaTime;
        // transform.position = transform.position.Lerp(primaryPosition, target.position, addictive.Evaluate(t));

    }
}
