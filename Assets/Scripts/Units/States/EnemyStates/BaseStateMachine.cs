using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseStateMachine : MonoBehaviour
{
    [SerializeField] private EnemyBaseState _initialState;
    private Dictionary<Type, Component> _cachedComponents;
    [SerializeField] private EnemyBaseState CurrentState;
    [HideInInspector] public int layer = 8;
    [HideInInspector] public float TimeInState;
    public GameObject Target;
    void Awake()
    {
        CurrentState = _initialState;
        _cachedComponents = new Dictionary<Type, Component>();
    }

    void Start()
    {
        CurrentState.Enter(this);
        Target = GameManager.Instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentState.Execute(this);
    }

    public void ChangeState(EnemyBaseState state)
    {
        CurrentState.Exit(this);
        CurrentState = state;
        CurrentState.Enter(this);
    }

    public void ChangeState<T>(EnemyBaseState state, T param)
    {
        CurrentState.Exit(this);
        CurrentState = state;
        CurrentState.Enter(this, param);
    }

    public new T GetComponent<T>() where T : Component
    {
        if (_cachedComponents.ContainsKey(typeof(T)))
        {
            if (_cachedComponents[typeof(T)] == null)
                return null;
            return _cachedComponents[typeof(T)] as T;
        }

        var component = base.GetComponent<T>();
        _cachedComponents.Add(typeof(T), component);
        return component;
    }
}
