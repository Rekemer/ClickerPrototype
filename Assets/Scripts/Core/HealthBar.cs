using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Camera _camera;
    private float _maxHealth;
    private float value;
     private Image _image;

    public void SetMaxHealth(float max)
    {
        _maxHealth = max;
        _image = GetComponent<Image>();
    }

    public void UpdateHealth(float health)
    {
        value = health / _maxHealth;
        _image.fillAmount = value;
    }
    
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
