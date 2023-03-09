using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AEnemy : MonoBehaviour
{
    [SerializeField] protected float _moveDuration;

    [SerializeField] protected Transform _spawnPosition;
    [SerializeField] protected Transform _stayPosition;


    protected IEnumerator MoveInDirection(Direction direction)
    {
        yield return null;
    }
    private IEnumerator StayOnBase()
    {
        yield return null;
    }
}
