using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _template;
    [SerializeField] private float _coolDown;

    private Transform _path;
    private Transform[] _points;

    private void Start()
    {
        _path = GetComponent<Transform>();

        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
        {
            _points[i] = _path.GetChild(i);
        }

        StartCoroutine(Create());
    }

    private IEnumerator Create()
    {
        var waitForSeconds = new WaitForSeconds(_coolDown);

        for (int i = 0; i < _points.Length; i++)
        {
            Instantiate(_template, _points[i].position, Quaternion.identity);

            yield return waitForSeconds;
        }
    }
}
