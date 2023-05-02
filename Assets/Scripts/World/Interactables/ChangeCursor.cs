using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    public Texture2D cursor;
    public bool Enabled;
    void Awake()
    {
        if (Enabled)
        {
            DefaultCur();
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
    public void ChangeCrsr(Texture2D cursorType)
    {
        Vector2 hotspot = new Vector2(cursorType.width / 2, cursorType.height / 2);
        Cursor.SetCursor(cursorType, Vector2.zero, CursorMode.Auto);
    }
    public void DefaultCur()
    {
        ChangeCrsr(cursor);
    }
}
