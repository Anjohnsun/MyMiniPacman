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
    [SerializeField] private LayerMask _bonusMask;
    [SerializeField] private LayerMask _enemyMask;

    [SerializeField] private PlayerUIDrawer _uiDrawer;

    private PlayerData _playerData;
    [SerializeField] private int _startHp;

    private void Start()
    {
        _moveDelay = _moveDuration / 10;
        _playerData = new PlayerData(_startHp, 0);
        _uiDrawer.PlayerData = _playerData;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) UpdateNextDirection(Direction.Up);
        if (Input.GetKeyDown(KeyCode.DownArrow)) UpdateNextDirection(Direction.Down);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) UpdateNextDirection(Direction.Left);
        if (Input.GetKeyDown(KeyCode.RightArrow)) UpdateNextDirection(Direction.Right);
    }

    private void OnTriggerEnter(Collider other)
    {
        //ахахахах следущая строчка это что???
        if (_bonusMask == (_bonusMask | (1 << other.gameObject.layer)))
        {
            _playerData.Points++;
            _uiDrawer.Refresh();
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.layer == _enemyMask.value)
        {

        }
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