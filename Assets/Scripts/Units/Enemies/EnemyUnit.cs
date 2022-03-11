using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    public StatBlock stats;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.AddEnemy(this.gameObject);
        stats = GetComponent<StatBlockComponent>().stats;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
