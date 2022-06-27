using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = FindObjectOfType<Camera>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_camera != null)
        {
           transform.rotation = Quaternion.LookRotation(-(_camera.transform.position - transform.position),Vector3.up);
        }
    }
}
