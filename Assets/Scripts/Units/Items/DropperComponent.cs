using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropperComponent : MonoBehaviour
{
    public DropTable dropTable;
    private List<GameObject> GuarenteedDrops;
    private WeightedChanceExecutor weightedChanceExecutor;
    void Start()
    {
        GuarenteedDrops = new List<GameObject>();
        weightedChanceExecutor = new WeightedChanceExecutor();
        AddDrops();
        EventManager.StartListening(Events.UnitDiedTrigger, ExecuteDropTable);
    }

    void OnDestroy()
    {
        EventManager.StopListening(Events.UnitDiedTrigger, ExecuteDropTable);
    }

    private void ExecuteDropTable(Dictionary<string, object> message)
    {
        if (message["source"] != null && (GameObject)message["source"] == gameObject)
        {
            var source = (GameObject)message["source"];
            weightedChanceExecutor.Execute();
            foreach (var drop in GuarenteedDrops)
            {
                Instantiate(drop, transform.position, Quaternion.identity);
            }
        }
    }

    private void AddDrops()
    {
        if (dropTable.Drops != null)
        {
            foreach (var drop in dropTable.Drops)
            {
                if (drop.isGuarenteed)
                {
                    GuarenteedDrops.Add(drop.drop);
                }
                else
                {
                    weightedChanceExecutor.AddChance(
                        new WeightedChanceParam(() =>
                        {
                            if (drop.drop != null)
                                Instantiate(drop.drop, transform.position, Quaternion.identity);
                        }, drop.dropWeight)
                    );
                }
            }
        }
    }
}
