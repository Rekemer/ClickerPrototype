using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Camera _camera;
    private float _maxHealth;
    private float _fillValue;
    private Image _image;

    public void SetMaxHealth(float max)
    {
        _maxHealth = max;
        _image = GetComponent<Image>();
    }

    public void UpdateHealth(float health)
    {
        _fillValue = health / _maxHealth;
        _image.fillAmount = _fillValue;
    }

    private void Awake()
    {
        _camera = FindObjectOfType<Camera>();
    }

    void Update()
    {
        if (_camera != null)
        {
            transform.rotation =
                Quaternion.LookRotation(-(_camera.transform.position - transform.position), Vector3.up);
        }
    }
}