using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPointGenerator : MonoBehaviour
{
    [SerializeField] private Transform _fillFrom;
    [SerializeField] private Transform _fillTo;

    [SerializeField] private GameObject _expPointPrefab;
    [SerializeField] private Transform _parent;

    void Start()
    {
        Ray ray = new Ray(new Vector3(), Vector3.down);

        for(int i = (int)_fillFrom.position.x; i < (int)_fillTo.position.x; i++)
        {
            for (int j = (int)_fillFrom.position.z; j < (int)_fillTo.position.z; j++)
            {
                ray.origin = new Vector3(i, 5, j);
                if (!Physics.Raycast(ray, 15))
                    Instantiate(_expPointPrefab, new Vector3(i, 0.5f, j), new Quaternion(), _parent);
            }
        }
    }
}
