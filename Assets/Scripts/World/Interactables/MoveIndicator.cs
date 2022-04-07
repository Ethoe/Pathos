using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIndicator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        EventManager.StartListening(Events.PlayerClick, moveIndicator);
        EventManager.StartListening(Events.PlayerExitMove, exitMove);
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void OnDestroy()
    {
        EventManager.StopListening(Events.PlayerClick, moveIndicator);
        EventManager.StopListening(Events.PlayerExitMove, exitMove);

    }

    void moveIndicator(Dictionary<string, object> message)
    {
        var location = (Vector2)message["target"];
        this.gameObject.transform.position = location;
        this.gameObject.SetActive(true);
    }

    void exitMove(Dictionary<string, object> message)
    {
        this.gameObject.SetActive(false);
    }
}
