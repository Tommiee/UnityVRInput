using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterRotator : MonoBehaviour
{

    [SerializeField] private Transform target;

    void LateUpdate()
    {
        this.transform.rotation = Quaternion.Euler(target.rotation.x, target.rotation.y, target.rotation.z * -1f);
    }
}
