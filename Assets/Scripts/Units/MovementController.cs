using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public bool activelyMoving = false;
    private bool _CheckDelay;
    private float _Delay;
    private Vector2 _Target;
    public Vector2 Target
    {
        get { return _Target; }
        set
        {
            _CheckDelay = true;
            _Delay = .01f;
            SetMoveSpeed = false;
            activelyMoving = true;
            _Target = value;
        }
    }
    private Vector2 _previousPosition;
    public Vector2 Direction
    {
        get
        {
            var direction = ((Vector2)transform.position - _previousPosition).normalized;
            return direction;
        }
    }
    public float Distance { get { return Vector2.Distance((Vector2)transform.position, Target); } }
    public bool IsMoving
    {
        get
        {
            if (activelyMoving)
            {
                if (_CheckDelay)
                {
                    return Vector2.Distance((Vector2)transform.position, Target) > .1;
                }
                return (Vector2.Distance((Vector2)transform.position, Target) > .1) && Speed > .01;
            }
            return activelyMoving;
        }
    }
    public float Speed
    {
        get
        {
            var speed = Mathf.Abs(Vector2.Distance((Vector2)transform.position, _previousPosition));
            return speed;
        }
    }
    public bool SetMoveSpeed = false;
    private float _MoveSpeed;
    public float MoveSpeed
    {
        get
        {
            if (SetMoveSpeed)
                return _MoveSpeed;
            return stats.MoveSpeed.Value;
        }
        set
        {
            _MoveSpeed = value;
            SetMoveSpeed = true;
        }
    }
    private StatBlock stats;
    private Rigidbody2D rigidbody2d;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        stats = GetComponent<StatBlockComponent>().stats;
        _previousPosition = transform.position;
    }
    void FixedUpdate()
    {
        if (activelyMoving)
        {
            if (_CheckDelay)
            {
                _Delay -= Time.deltaTime;
                _CheckDelay = false;
            }
            _previousPosition = transform.position;
            rigidbody2d.MovePosition(Vector2.MoveTowards(transform.position, Target, Time.deltaTime * MoveSpeed));
        }
    }
}
