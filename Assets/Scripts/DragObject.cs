using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SharedValues;

public class DragObject : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;
    private readonly CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    public Texture2D cursorTexture;
    public DragNames dragName;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mOffset;
    }

    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

    private void OnMouseUp()
    {
        PlayerPrefs.SetFloat(dragName + "X", rectTransform.anchoredPosition.x);
        PlayerPrefs.SetFloat(dragName + "Y", rectTransform.anchoredPosition.y);
    }
}
