using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class SC_Move : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    
    private Rigidbody2D _rigidbody2D;
    private Camera _camera;
    private LineRenderer _lineRenderer;
    private Touch _touch;
    
    private Vector3 _tapPosition, _moveDirection, _currentMovePosition;
    private float _oldTapPosDist, _currentTapPosDist;
    private bool _isMoving = false;
    
    private readonly Queue<Vector3> _tapQueue = new Queue<Vector3>();
    
    void Awake()
    {

        _camera = Camera.main;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 1;
        _lineRenderer.numCapVertices = 16;
        _lineRenderer.numCornerVertices = 16;

    }

    void Update()
    {

        Vector3[] pathPoints;
        Vector3 transformPosition;

        
        if (_isMoving)
        {
            transformPosition = transform.position;
            _currentTapPosDist = (_currentMovePosition - transformPosition).magnitude;
            _lineRenderer.SetPosition(0, transformPosition);
        }

        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Began)
            {
                _tapPosition = _camera.ScreenToWorldPoint(_touch.position);
                _tapPosition.z = 0;

                if (_tapQueue.Count == 0 && !_isMoving)
                {
                    _currentMovePosition = _tapPosition;
                    _moveDirection = (_currentMovePosition - transform.position).normalized;
                    _rigidbody2D.velocity = new Vector2(_moveDirection.x * moveSpeed, _moveDirection.y * moveSpeed);
                    _isMoving = true;
                }
                

                _tapQueue.Enqueue(_tapPosition);

                
                if (_tapQueue.Peek() == _currentMovePosition)
                {
                    _lineRenderer.positionCount = _tapQueue.Count + 1;
                    pathPoints = new Vector3[_tapQueue.Count + 1];
                    pathPoints[0] = transform.position;
                    Array.Copy(_tapQueue.ToArray(), 0, pathPoints, 1, _tapQueue.Count);
                    _lineRenderer.SetPositions(pathPoints);
                    
                }
                else
                {
                    _lineRenderer.positionCount = _tapQueue.Count + 2;
                    pathPoints = new Vector3[_tapQueue.Count + 2];
                    pathPoints[0] = transform.position;
                    pathPoints[1] = _currentMovePosition;
                    Array.Copy(_tapQueue.ToArray(), 0, pathPoints, 2, _tapQueue.Count);
                    _lineRenderer.SetPositions(pathPoints);
                }


            }
        }

        if (_currentTapPosDist > _oldTapPosDist)
        {
            _oldTapPosDist = 0;
            _currentTapPosDist = 0;

            if (_tapQueue.Count != 0)
            {
                transformPosition = transform.position;
                _currentMovePosition = _tapQueue.Dequeue();
                _moveDirection = (_currentMovePosition - transformPosition).normalized;
                _rigidbody2D.velocity = new Vector2(_moveDirection.x * moveSpeed, _moveDirection.y * moveSpeed);

                _lineRenderer.positionCount = _tapQueue.Count + 2;
                pathPoints = new Vector3[_tapQueue.Count + 2];
                pathPoints[0] = transformPosition;
                pathPoints[1] = _currentMovePosition;
                Array.Copy(_tapQueue.ToArray(), 0, pathPoints, 2, _tapQueue.Count);
                _lineRenderer.SetPositions(pathPoints);
                
            }
            else
            {
                _isMoving = false;
                _rigidbody2D.velocity = Vector2.zero;
                _lineRenderer.positionCount = 1;
            }
        }

        if (_isMoving)
        {
            _oldTapPosDist = (_currentMovePosition - transform.position).magnitude;
        }
    }
}
