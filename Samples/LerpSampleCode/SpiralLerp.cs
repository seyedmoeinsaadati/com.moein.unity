using System;
using Moein.Core;
using UnityEngine;

public class SpiralLerp : MonoBehaviour
{
    public Transform target;
    public float spiralSpeed;
    public float rotationSpeed;

    float angleSpeed = 0;
    float dstToTarget;
    public bool isMoving = false;

    void Start()
    {
        if (target != null)
        {
            dstToTarget = Vector3.Distance(transform.position, target.position);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            isMoving = !isMoving;
        }

        if (isMoving) Move();
    }

    private void Move()
    {
        transform.position = transform.position.SpiralLerpXZ(target.position, angleSpeed, dstToTarget);
        dstToTarget -= spiralSpeed * Time.deltaTime;
        angleSpeed += rotationSpeed * Time.deltaTime;
    }
}
