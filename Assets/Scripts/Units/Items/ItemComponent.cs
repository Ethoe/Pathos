using UnityEngine;
using System;

public class ItemComponent : MonoBehaviour
{
    // Public Vars
    public ScriptableBuff itemBuff;

    // Private Vars
    private Collider2D collider2d;


    // Start is called before the first frame update
    void Start()
    {
        collider2d = GetComponent<Collider2D>();

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == GameManager.Instance.player)
        {
            BuffableEntity buffableEntity = GameManager.Instance.player.GetComponent<BuffableEntity>();
            buffableEntity.AddBuff(itemBuff.InitializeBuff(GameManager.Instance.player));
            Destroy(gameObject);
        }
    }
}
