using UnityEngine;

namespace Core
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] float speed;
        [SerializeField] private LayerMask _clickableLayerMask;
        [SerializeField] private int _damage;
        [SerializeField] private ParticleSystem _hitParticles;
        private Ground _ground;
        private Camera _camera;
        private bool _firstClick;
        private Vector3 _firstPos;
        private Vector3 _currPos;
        private Vector3 _diffDir;

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
            RaycastHit hit = default;
            if (RayCastUnderMouse(ref hit)) return;
            var worldCurrPos = _camera.ScreenToWorldPoint(_currPos);
            var worldFirstPos = _camera.ScreenToWorldPoint(_firstPos);
            worldCurrPos.z = 10;
            worldFirstPos.z = 10;
            _diffDir = -(worldCurrPos - worldFirstPos).normalized;
            if (Input.GetMouseButtonDown(0) && _firstClick == false)
            {
                _firstClick = true;
                _firstPos = mousePos;
            }
            else if (Input.GetMouseButton(0) && _firstClick)
            {
                _currPos = mousePos;
            }

            if (Input.GetMouseButton(0) && _firstClick)
            {
                transform.Translate(_diffDir * speed * Time.deltaTime);
            }

            if (Input.GetMouseButtonUp(0) && _firstClick)
            {
                _firstClick = false;
            }
        }

        private bool RayCastUnderMouse(ref RaycastHit hit)
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, _clickableLayerMask))
            {
                return true;
            }

            return false;
        }

        private void GetClicks()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // raycast to check if it is enemy
                RaycastHit hit = default;
                if (RayCastUnderMouse(ref hit))
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
                        Destroy(particles.gameObject, 1f);
                    }
                }
            }
        }
    }
}