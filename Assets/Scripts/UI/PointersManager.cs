using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;

namespace UI
{
    public class PointersManager : MonoBehaviour
    {
        [SerializeField] private Pointer _prefabPointer;
        private List<BaseEnemy> _enemiesBeyondScreen;
        private Pointer[] _pointers = new Pointer[10];
        private Vector3 _focusCamera;
        private Camera _camera;
        private Ground _ground;


        private void Awake()
        {
            _camera = FindObjectOfType<Camera>();
            _ground = FindObjectOfType<Ground>();
            for (int i = 0; i != _pointers.Count(); i++)
            {
                _pointers[i] = Instantiate(_prefabPointer, Vector3.zero, Quaternion.identity);
                _pointers[i].transform.SetParent(transform);
            }
        }

        private void Start()
        {
            _focusCamera = _camera.WorldToScreenPoint(_camera.transform.parent.position);
        }

        private void Update()
        {
            _enemiesBeyondScreen = FindEnemiesBeyondScreen();
            SetPointers(_enemiesBeyondScreen);
            UpdatePointers();
        }

        private void UpdatePointers()
        {
            foreach (var pointer in _pointers)
            {
                if (!_enemiesBeyondScreen.Contains(pointer.GetEnemy()))
                {
                    pointer.SetEnemy(null);
                    pointer.gameObject.SetActive(false);
                }
                else
                {
                    var halfWidth =
                        CalculateRelativePointPosition(pointer.GetEnemy(), _focusCamera, out var relPosOfPointer);
                    pointer.GetComponent<RectTransform>().anchoredPosition = relPosOfPointer;
                }
            }
        }

        void SetPointers(List<BaseEnemy> enemiesToPointTo)
        {
            foreach (var enemy in enemiesToPointTo)
            {
                var halfWidth = CalculateRelativePointPosition(enemy, _focusCamera, out var relPosOfPointer);
                Pointer p = GetFreePointer();
                if (p != null)
                {
                    p.SetEnemy(enemy);
                    p.gameObject.SetActive(true);
                    p.GetComponent<RectTransform>().anchoredPosition =
                        relPosOfPointer + new Vector3(halfWidth, _camera.pixelHeight / 2f, 0);
                }
            }
        }

        private float CalculateRelativePointPosition(BaseEnemy enemy, Vector3 focusCamera, out Vector3 relPosOfPointer)
        {
            var enemyPos = _camera.WorldToScreenPoint(enemy.transform.position);
            var dir = (enemyPos - focusCamera).normalized;
            var halfHeight = _camera.pixelHeight / 2f;
            var halfWidth = _camera.aspect * halfHeight;
            relPosOfPointer = (dir * halfWidth);
            return halfWidth;
        }

        private Pointer GetFreePointer()
        {
            foreach (var pointer in _pointers)
            {
                if (pointer.GetEnemy() == null)
                {
                    return pointer;
                }
            }

            return null;
        }

        List<BaseEnemy> FindEnemiesBeyondScreen()
        {
            List<BaseEnemy> allEnemies = _ground.CurrentEnemies;
            List<BaseEnemy> enemiesInScreen = new List<BaseEnemy>();
            foreach (var enemy in allEnemies)
            {
                if (enemy != null)
                {
                    // convert his position to screen space
                    var screenPos = _camera.WorldToViewportPoint(enemy.transform.position);
                    if (screenPos.x <= 1 && screenPos.y <= 1 && screenPos.x > 0 && screenPos.y > 0)
                    {
                        enemiesInScreen.Add(enemy);
                    }
                }
            }

            var enemiesBeyond = allEnemies.Except(enemiesInScreen).ToList();
            return enemiesBeyond;
        }
    }
}