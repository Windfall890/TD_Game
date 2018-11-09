using System;
using UnityEngine;

public class CameraController : MonoBehaviour {


    public float panSpeed = 30f;
    public float panBorder = 10f;
    public float zoomSpeed = 300f;
	// Update is called once per frame
	void Update ()
    {
        HandlePanForwardBack();
        HandlePanLeftRight();
        HandleZoom();

    }

    private void HandleZoom()
    {
        var scroll = Input.mouseScrollDelta.normalized.y;
        var distance = Vector3.up * scroll * zoomSpeed * Time.deltaTime;

        transform.Translate(distance, Space.World);
    }

    private void HandlePanForwardBack()
    {
        if (KeyMatches(KeyCode.W, KeyCode.UpArrow) || Input.mousePosition.y > Screen.height - panBorder)
        {
            Pan(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y < 0 + panBorder)
        {
            Pan(Vector3.back);
        }
    }

    private bool KeyMatches(params KeyCode[] keys)
    {

        foreach( KeyCode code in keys) {
            if (Input.GetKey(code)) return true;
        }

        return false;
    }

    private void HandlePanLeftRight()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x > Screen.width - panBorder)
        {
            Pan(Vector3.right);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x < 0 + panBorder)
        {
            Pan(Vector3.left);
        }
    }

    private void Pan(Vector3 v)
    {
        transform.Translate(v * panSpeed * Time.deltaTime, Space.World);
    }
}
