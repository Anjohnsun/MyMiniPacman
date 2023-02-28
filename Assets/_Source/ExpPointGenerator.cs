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

        Debug.Log($"{(int)_fillFrom.position.x}, {(int)_fillTo.position.x}");
        Debug.Log($"{(int)_fillFrom.position.z}, {(int)_fillTo.position.z}");

        for(int i = (int)_fillFrom.position.x; i < (int)_fillTo.position.x; i++)
        {
            for (int j = (int)_fillFrom.position.z; j < (int)_fillTo.position.z; j++)
            {
                Debug.Log("point");
                ray.origin = new Vector3(i, 5, j);
                if (!Physics.Raycast(ray, 15))
                    Instantiate(_expPointPrefab, new Vector3(i, 0, j), new Quaternion(), _parent);
            }
        }
    }
}
