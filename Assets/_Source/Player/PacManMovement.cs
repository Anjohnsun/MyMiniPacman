using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacManMovement : MonoBehaviour
{
    [SerializeField] private float _moveDuration;
    private float _moveDelay;

    [SerializeField] private Direction? _nextDirection;
    [SerializeField] private Direction? _currentDirection;

    [SerializeField] private LayerMask _obstacleMask;


    private void Start()
    {
        _moveDelay = _moveDuration / 10;
    }

    private void Update()
    {
        /*#region Input
        if (Input.GetAxis("Horizontal") != 0)
            UpdateNextDirection(Input.GetAxis("Horizontal") > 0 ? Direction.Right : Direction.Left);
        if (Input.GetAxis("Vertical") != 0)
            UpdateNextDirection(Input.GetAxis("Vertical") > 0 ? Direction.Up : Direction.Down);
        #endregion */

        if (Input.GetKeyDown(KeyCode.UpArrow)) UpdateNextDirection(Direction.Up);
        if (Input.GetKeyDown(KeyCode.DownArrow)) UpdateNextDirection(Direction.Down);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) UpdateNextDirection(Direction.Left);
        if (Input.GetKeyDown(KeyCode.RightArrow)) UpdateNextDirection(Direction.Right);
    }

    private void UpdateNextDirection(Direction newDirection)
    {
        if (_currentDirection != null)
        {
            if (newDirection != _currentDirection)
            {
                _nextDirection = newDirection;
            }

            else
                _nextDirection = null;
        }
        else
        {

            if (IsDirectionOpen(newDirection))
            {
                _currentDirection = newDirection;
                StartCoroutine(MoveInCurrentDirection());
            }
        }
    }

    private bool IsDirectionOpen(Direction? direction)
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        switch (direction)
        {
            case Direction.Up:
                ray.direction = transform.forward;
                break;
            case Direction.Down:
                ray.direction = transform.forward * -1;
                break;
            case Direction.Left:
                ray.direction = transform.right * -1;
                break;
            case Direction.Right:
                ray.direction = transform.right;
                break;
            default:
                return false;
        }

        if (Physics.Raycast(ray, 1.1f, _obstacleMask))
            return false;

        return true;
    }

    private void ManageNextMove()
    {
        if (_nextDirection == null)
        {
            if (IsDirectionOpen(_currentDirection))
                StartCoroutine(MoveInCurrentDirection());
            else
                _currentDirection = null;
        }

        else
        {
            if (IsDirectionOpen(_nextDirection))
            {
                _currentDirection = _nextDirection;
                _nextDirection = null;
                StartCoroutine(MoveInCurrentDirection());
            }
            else
            {
                if (IsDirectionOpen(_currentDirection))
                    StartCoroutine(MoveInCurrentDirection());
                else
                {
                    _currentDirection = null;
                    _nextDirection = null;
                }
            }
        }
    }

    private IEnumerator MoveInCurrentDirection()
    {
        Vector3 actualStep = new Vector3();
        switch (_currentDirection)
        {
            case Direction.Up:
                actualStep = new Vector3(0, 0, 0.1f);
                break;
            case Direction.Down:
                actualStep = new Vector3(0, 0, -0.1f);
                break;
            case Direction.Left:
                actualStep = new Vector3(-0.1f, 0, 0);
                break;
            case Direction.Right:
                actualStep = new Vector3(0.1f, 0, 0);
                break;
        }

        for (int i = 0; i < 10; i++)
        {
            transform.position += actualStep;
            yield return new WaitForSeconds(_moveDelay);
        }

        ManageNextMove();
        yield return null;
    }

}

public enum Direction
{
    Left,
    Right,
    Up,
    Down
}