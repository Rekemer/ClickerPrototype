using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Booster : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPos;

    private RectTransform rect;
    private Camera _camera;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        var objects = FindSceneObjects_Old("MainLevel");
        foreach (var obj in objects)
        {
            var camera = obj.GetComponentInChildren<Camera>();
            if (camera != null)
            {
                _camera = camera;
                break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPos = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        
        Vector3 onScreen;
        //RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, eventData.position,_camera, out onScreen);
        onScreen = _camera.ScreenToWorldPoint(transform.position);
        Debug.Log( onScreen);
        // cast a raycast 
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = startPos;
    }
    List<GameObject> FindSceneObjects_Old(String sceneName)
    {
        List<GameObject> objs = new List<GameObject>();
        foreach (GameObject obj in GameObject.FindObjectsOfType(typeof(GameObject)))
        {
            if (obj.scene.name == sceneName)
            {
                objs.Add(obj);
            }
        }

        return objs;
    }
}
