using UnityEngine;
using System.Collections.Generic;

public class ItemComponent : MonoBehaviour
{
    // Public Vars
    public ScriptableBuff itemBuff;
    [Range(0, 5)]
    public int Range;

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
            EventManager.TriggerEvent(Events.AddBuffTrigger, new Dictionary<string, object> {
                {
                    "buff",
                    new BuffContext(
                        source: gameObject,
                        target: GameManager.Instance.player,
                        buff: itemBuff.InitializeBuff(GameManager.Instance.player)
                    )
                }
            });
            Destroy(gameObject);
        }
    }
}
