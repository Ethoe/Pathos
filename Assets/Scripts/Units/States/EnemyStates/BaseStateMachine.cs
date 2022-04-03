using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseStateMachine : MonoBehaviour
{
    [SerializeField] private EnemyBaseState _initialState;
    private Dictionary<Type, Component> _cachedComponents;
    [HideInInspector] public float TimeInState;
    public GameObject Target;
    [SerializeField] private EnemyBaseState CurrentState;
    void Awake()
    {
        CurrentState = _initialState;
        CurrentState.Enter(this);
        _cachedComponents = new Dictionary<Type, Component>();
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

    public new T GetComponent<T>() where T : Component
    {
        if (_cachedComponents.ContainsKey(typeof(T)))
            return _cachedComponents[typeof(T)] as T;

        var component = base.GetComponent<T>();
        if (component != null)
        {
            _cachedComponents.Add(typeof(T), component);
        }
        return component;
    }
}
