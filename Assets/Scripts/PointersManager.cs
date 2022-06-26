using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class PointersManager : MonoBehaviour
    {
        [SerializeField] private Pointer _prefabPointer;
        protected Camera _camera;
        protected Ground _ground;
        public List<BaseEnemy> enemiesBeyondScreen ;
        private List<Pointer> _pointers = new List<Pointer>();
        
        private void Awake()
        {
            _camera = FindObjectOfType<Camera>();
            _ground = FindObjectOfType<Ground>();
        }

        private void Update()
        { 
            enemiesBeyondScreen = FindEnemiesBeyondScreen();
            SetPointers(enemiesBeyondScreen);
            UpdatePointers();
        }

        private void UpdatePointers()
        {
            
        }

        void SetPointers(List<BaseEnemy> enemiesToPointTo)
        {
            var focusCamera = _camera.WorldToScreenPoint(_camera.transform.parent.position);
            foreach (var enemy in enemiesToPointTo)
            {
                var enemyPos = _camera.WorldToScreenPoint(enemy.transform.position);
                var dir = (enemyPos - focusCamera).normalized;
                float halfHeight = _camera.pixelHeight/2f;
                float halfWidth = _camera.aspect * halfHeight;
                var relPosOfPointer =(dir * halfWidth);
                var instance = Instantiate(_prefabPointer, relPosOfPointer, Quaternion.identity);
                instance.GetComponent<RectTransform>().anchoredPosition = relPosOfPointer+ new Vector3(halfWidth,_camera.pixelHeight/2f,0);
                instance.transform.SetParent(transform);
                instance.SetEnemy(enemy);
                _pointers.Add(instance);
            }
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