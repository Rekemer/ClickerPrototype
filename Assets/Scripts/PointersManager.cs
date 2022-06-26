using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class PointersManager : MonoBehaviour
    {
        [SerializeField] private GameObject _prefabPointer;
        protected Camera _camera;
        protected Ground _ground;
        public List<BaseEnemy> enemiesBeyondScreen ;
        private void Awake()
        {
            _camera = FindObjectOfType<Camera>();
            _ground = FindObjectOfType<Ground>();
        }

        private void Update()
        { 
            enemiesBeyondScreen = FindEnemiesBeyondScreen();
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