using UnityEngine;

namespace Core
{
    public class CameraController : MonoBehaviour
    {
    
    
        private Ground _ground;
        private Camera _camera;
    
        [SerializeField] float speed;
        private bool firstClick;
        private Vector3 firstPos;
        private Vector3 currPos;
        private Vector3 diffDir;
        [SerializeField] private LayerMask ClickableLayerMask;
        [SerializeField] private int _damage;
        [SerializeField] private ParticleSystem _hitParticles;
        public bool CanMove { get; set; } = true;
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
            if (CanMove)
            {
                Move();
            }

            GetClicks();
        }

        private void Move()
        {
            var mousePos = Input.mousePosition;
            RaycastHit hit;
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, ClickableLayerMask))
            {
                return;
            }
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
            else if (Input.GetMouseButton(0) && firstClick == true)
            {
                currPos = mousePos;
            }

            if (Input.GetMouseButton(0) && firstClick)
            {
                transform.Translate(diffDir * speed * Time.deltaTime);
            }

            if (Input.GetMouseButtonUp(0) && firstClick == true)
            {
                firstClick = false;
            }
        }

        private void GetClicks()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // raycast to check if it is enemy
                RaycastHit hit;
                var ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit,ClickableLayerMask))
                {
                    var enemy = hit.transform.GetComponent<BaseEnemy>();
                    if (enemy)
                    {
                        enemy.GetDamage(_damage);
                        var particlesPos = enemy.transform.position +
                                           (_camera.transform.position - enemy.transform.position).normalized;
                        var particles =
                            Instantiate(_hitParticles, particlesPos, Quaternion.identity);
                        particles.Play(true);
                        Destroy(particles.gameObject,1f);
                        
                    }
                }
            }
        }
    }
}