using UnityEngine;

public class TimedStunBuff : TimedBuff
{
    private PlayerController player;
    private BaseStateMachine enemy;

    public TimedStunBuff(ScriptableBuff buff, GameObject obj) : base(buff, obj)
    {
        if (obj == GameManager.Instance.player)
        {
            player = obj.GetComponent<PlayerController>();
        }
        else
        {
            enemy = obj.GetComponent<BaseStateMachine>();
        }
    }

    protected override void ApplyEffect()
    {
        if (player)
        {
            player.playerInControl = false;
        }
        else
        {

        }
    }

    protected override void TickEffect() { }

    public override void End()
    {
        if (player)
        {
            player.playerInControl = true;
        }
        else
        {

        }
    }
}
