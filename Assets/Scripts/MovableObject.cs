using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _borders;
    private Vector2 _objectSize;

    private Vector3 _posByCursor;
    private Rigidbody2D _rb;

    private bool MouseMoving = false;


    private void Start()
    {
        _objectSize = GetComponent<SpriteRenderer>().size;
        _rb = GetComponent<Rigidbody2D>();
    }

    public void OnMouseDown()
    {
        _posByCursor = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MouseMoving = true;
        _rb.isKinematic = true;
    }

    private void OnMouseUp()
    {
        MouseMoving = false;
        _rb.isKinematic = false;
    }

    private void Update()
    {
        if (MouseMoving)
        {
            Vector2 clampPosition;
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + _posByCursor;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);

            if (_borders != null)
            {
                clampPosition = transform.localPosition;
                clampPosition.x = Mathf.Clamp(clampPosition.x, -_borders.size.x / 2 + (_objectSize.x * transform.localScale.x / 2), _borders.size.x / 2 - (_objectSize.x * transform.localScale.x / 2));
                clampPosition.y = Mathf.Clamp(clampPosition.y, -_borders.size.y / 2 + (_objectSize.y * transform.localScale.y / 2), _borders.size.y / 2 - (_objectSize.y * transform.localScale.y / 2));
                transform.localPosition = clampPosition;
                //_rb.MovePosition(clampPosition);
            }
        }
    }
}
