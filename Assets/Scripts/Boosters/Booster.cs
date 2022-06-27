using System;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Boosters
{
    public abstract class Booster : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Vector3 startPos;

        private RectTransform rect;
        protected Camera _camera;
        protected Ground _ground;
        protected CameraController _cameraController;
         protected AudioSource _audio;
        private void Awake()
        {
            _audio = GetComponent<AudioSource>();
            rect = GetComponent<RectTransform>();
            _camera = FindObjectOfType<Camera>();
            _cameraController = _camera.transform.parent.GetComponent<CameraController>();
            _ground = FindObjectOfType<Ground>();
            // var objects = FindSceneObjects("MainLevel");
            // foreach (var obj in objects)
            // {
            //     if (obj.transform.childCount == 1)
            //     {
            //         var camera = obj.transform.GetChild(0);
            //         if (camera)
            //         {
            //             _camera = camera.GetComponent<Camera>();
            //             _cameraController = obj.GetComponent<CameraController>();
            //         }
            //     }
            //   
            //     var ground = obj.GetComponent<Ground>();
            //    
            //
            //     if (ground)
            //     {
            //         _ground = ground;
            //     }
            // }
        }

        // Start is called before the first frame update
        void Start()
        {
       
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        protected abstract void ApplyBooster();

        public void OnBeginDrag(PointerEventData eventData)
        {
            startPos = transform.position;
            _cameraController.CanMove = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        
            Vector3 onScreen;
            //RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, eventData.position,_camera, out onScreen);
            onScreen = _camera.ScreenToWorldPoint(transform.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.position = startPos;
            _cameraController.CanMove = true;
            ApplyBooster();
            // apply bonus
        }
        List<GameObject> FindSceneObjects(String sceneName)
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
}