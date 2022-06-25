using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct Field
{
    public int x;
    public int y;
    public int height;
    public int width;

    public Field(int x, int y, int height, int width)
    {
        this.x = x;
        this.y = y;
        this.height = height;
        this.width = width;
    }
}

public abstract class BaseEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    protected Field allowedArea;
    private Ground _ground;

    [SerializeField] DifficultyEnemySettings _enemySettings;
    private bool _isMoving;

    protected virtual void Awake()
    {
        _ground = FindObjectOfType<Ground>();
    }

    protected virtual void Start()
    {
        var height = _ground.GroundHeight;
        var width = _ground.GroundWidth;
        allowedArea = new Field(0, 0, height, width);
        _ground.AddEnemy(this);
        transform.position = new Vector3(transform.position.x, .75f, transform.position.z);
        Move();
    }

    private void OnDestroy()
    {
        _ground.EraseEnemy(this);
    }

    

    protected virtual void Move()
    {
        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        _isMoving = true;
        float time;
        while (_isMoving)
        {
            Vector3 newPos = _ground.GetRandomPosition();
            time = 0;
            int iter = 0;
            while ((transform.position - newPos).sqrMagnitude > 4f && iter < 200)  
            {
                time += Time.deltaTime;
                iter++;
                float speed =4f;
                transform.position = Vector3.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(_enemySettings.timeOfWaitingAfterReachingNewPos);
        }

    }
}