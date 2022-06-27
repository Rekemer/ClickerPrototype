using Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Boosters
{
    public abstract class Booster : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Vector3 _startPos;
        protected Camera _camera;
        protected Ground _ground;
        protected CameraController _cameraController;
        protected AudioSource _audio;

        private void Awake()
        {
            _audio = GetComponent<AudioSource>();
            _camera = FindObjectOfType<Camera>();
            _cameraController = _camera.transform.parent.GetComponent<CameraController>();
            _ground = FindObjectOfType<Ground>();
        }


        protected abstract void ApplyBooster();

        public void OnBeginDrag(PointerEventData eventData)
        {
            _startPos = transform.position;
            _cameraController.CanMove = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.position = _startPos;
            _cameraController.CanMove = true;
            // apply booster
            ApplyBooster();
        }
    }
}