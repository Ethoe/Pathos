using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    public MovementController movement;
    public Dictionary<string, int> animationParam;

    void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<MovementController>();
        animationParam = new Dictionary<string, int>();
        animationParam["Ability"] = Animator.StringToHash("AbilityBlendTree");
        animationParam["Move"] = Animator.StringToHash("MoveBlendTree");
        animationParam["Idle"] = Animator.StringToHash("Idle");
    }

    void Update()
    {
        if (movement != null)
        {
            Vector2 moving = movement.Direction;
            animator.SetFloat("Move X", moving.x);
            animator.SetFloat("Move Y", moving.y);
        }
    }

    public void TriggerAnimation(string param)
    {
        if (animationParam.ContainsKey(param))
            animator.Play(animationParam[param], 0, 0.0f);
    }


    public void TriggerAnimation(int param)
    {
        animator.Play(param, 0, 0.0f);
    }
}
