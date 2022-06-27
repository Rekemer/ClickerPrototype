using System.Collections;
using UnityEngine;


namespace Core
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        [SerializeField] private HealthBar _healthBar;
        private Ground _ground;
        private Animator _animator;
        private float _health;
        private float _speed;
        private float _timeOfWaitingAfterReachingNewPos;
        private bool _isMoving;
        private State _state;


        protected virtual void Awake()
        {
            _ground = FindObjectOfType<Ground>();
            _animator = GetComponent<Animator>();
        }

        protected virtual void Start()
        {
            _state = State.ACTIVE;
            _ground.AddEnemy(this);
            transform.position = new Vector3(transform.position.x, .75f, transform.position.z);
            Move();
        }

        public abstract float GetPoints();

        public void SetTimeOfWaiting(float time)
        {
            _timeOfWaitingAfterReachingNewPos = time;
        }

        private void SetIsMoving(bool state)
        {
            _isMoving = state;
        }

        public void SetHealth(int health)
        {
            _healthBar.SetMaxHealth(health);
            _health = health;
        }

        public void SetSpeed(int speed)
        {
            _speed = speed;
        }


        protected virtual void Move()
        {
            StartCoroutine(MoveRoutine());
        }

        public State GetState()
        {
            return _state;
        }

        public void SetState(State newState)
        {
            _state = newState;
        }

        IEnumerator MoveRoutine()
        {
            _isMoving = true;
            bool isStanding = false;
            while (_isMoving)
            {
                Vector3 newPos = _ground.GetRandomPosition();
                transform.rotation = Quaternion.LookRotation((newPos - transform.position).normalized, Vector3.up);

                int iter = 0;
                while ((transform.position - newPos).sqrMagnitude > 4f && iter < 200)
                {
                    isStanding = false;
                    if (_animator)
                    {
                        _animator.SetBool("isStanding", isStanding);
                    }

                    iter++;
                    transform.position = Vector3.MoveTowards(transform.position, newPos, _speed * Time.deltaTime);
                    yield return null;
                }

                isStanding = true;
                if (_animator)
                {
                    _animator.SetBool("isStanding", isStanding);
                }

                yield return new WaitForSeconds(_timeOfWaitingAfterReachingNewPos);
            }
        }

        public void GetDamage(int damage)
        {
            _health -= damage;
            _healthBar.UpdateHealth(_health);
            if (_health <= 0)
            {
                if (_animator)
                {
                    _animator.SetBool("isDead", true);
                    SetIsMoving(false);
                }

                StartCoroutine(SetStateAfterTimeRoutine(State.DEAD, 2.5f));
            }
        }

        IEnumerator SetStateAfterTimeRoutine(State state, float time)
        {
            yield return new WaitForSeconds(time);
            SetState(state);
        }

        private void OnDestroy()
        {
            _ground.EraseEnemy(this);
        }
    }
}