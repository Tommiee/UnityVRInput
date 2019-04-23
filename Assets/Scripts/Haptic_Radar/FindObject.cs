using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindObject : MonoBehaviour
{
    private GameObject _closest;

    private GameObject[] GetTargets(string _tag)
    {
        GameObject[] _targets = GameObject.FindGameObjectsWithTag(_tag);
        return _targets;
    }

    public GameObject FindClosestTarget(GameObject _player, string _tag)
    {
        GameObject[] _objArr = GetTargets(_tag);
        float _distToClosest = Mathf.Infinity;

        for (int i = 0; i < _objArr.Length; i++)
        {
            float _dist = Vector3.Distance(_player.transform.position, _objArr[i].transform.position);
            if (_dist < _distToClosest)
            {
                _distToClosest = _dist;
                _closest = _objArr[i];
            }
        }

        return _closest;
    }

}
