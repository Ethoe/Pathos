using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIndicator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.onPlayerClick += moveIndicator;
        EventManager.instance.onPlayerExitMove += exitMove;
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void OnDestroy()
    {
        EventManager.instance.onPlayerClick -= moveIndicator;
        EventManager.instance.onPlayerExitMove -= exitMove;
    }

    void moveIndicator(Vector2 location)
    {
        this.gameObject.transform.position = location;
        this.gameObject.SetActive(true);
    }

    void exitMove()
    {
        this.gameObject.SetActive(false);
    }
}
