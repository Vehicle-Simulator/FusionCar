using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public float force = 0;
    public float rawacc;
    private float _lastPosition;
    private float _v;
    [SerializeField] private Rigidbody rb;

    private void Start()
    {
        _lastPosition = transform.position.y;
    }

    void FixedUpdate()
    {
        var velocity = rb.velocity;
        rawacc = velocity.magnitude;
        var acc = rawacc;
        force = acc;
        rb.AddForce(force * Vector3.up, ForceMode.VelocityChange);
    }
}