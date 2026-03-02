using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerDragControls : MonoBehaviour
{
    private Camera _camera;
    private Vector3 _offset;

    void Start()
    {
        _camera = Camera.main;
    }

    void OnMouseDown()
    {
        Vector3 _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        _mousePos.z = 0f;
        _offset = transform.position - _mousePos;
    }

    void OnMouseDrag()
    {
        Vector3 _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        _mousePos.z = 0f;
        transform.position = _mousePos + _offset;
    }
}
