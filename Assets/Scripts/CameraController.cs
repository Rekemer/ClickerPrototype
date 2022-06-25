using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Assertions.Comparers;


public class CameraController : MonoBehaviour
{
    
    
    private Ground _ground;
    private Camera _camera;
    
    [SerializeField] float speed;
    private bool firstClick;
    private Vector3 firstPos;
    private Vector3 currPos;
    private Vector3 diffDir;
    private void Awake()
    {
        _ground = FindObjectOfType<Ground>();
        _camera = GetComponentInChildren<Camera>();
    
        
    }

    void Start()
    {
        if (_ground != null)
        {
            transform.position = new Vector3(_ground.GroundCenter.x, 0f, _ground.GroundCenter.y);
        }
        else
        {
            Debug.LogError("CAMERA Failed To Initialize: Cannot find groundObject");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
    }

   
    void Update()
    {
        var mousePos = Input.mousePosition;
        var worldCurrPos = _camera.ScreenToWorldPoint(currPos);
        var worldFirstPos = _camera.ScreenToWorldPoint(firstPos);
        worldCurrPos.z = 10;
        worldFirstPos.z = 10;
         diffDir = -(worldCurrPos - worldFirstPos).normalized;
        if (Input.GetMouseButtonDown(0) && firstClick == false)
        {
            firstClick = true;
            firstPos = mousePos;

        }
        else  if (Input.GetMouseButton(0) &&  firstClick == true)
        {
            currPos = mousePos;
        }

        if (Input.GetMouseButton(0) && firstClick)
        {
            transform.Translate(diffDir* speed* Time.deltaTime);
        }

        if (Input.GetMouseButtonUp(0) && firstClick == true) 
        {
            firstClick = false;
        }
    }
}