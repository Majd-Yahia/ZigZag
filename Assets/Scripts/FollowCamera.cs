using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    [SerializeField] private Transform target;
    private Vector3 offset;
    private void Awake()
    {
        offset = (transform.position - target.position);
    }


    // usually used for cameras
    private void LateUpdate()
    {
        transform.position = (target.position + offset);
    }
}
