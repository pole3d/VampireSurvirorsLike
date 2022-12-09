using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject Target;
    public float Speed = 0.3f;
    public Transform TopRight;
    public Transform BottomLeft;

    Transform _transform;
    Vector3 _velocity;
    Camera _camera;

    void Awake()
    {
        _transform = transform;
        _camera = GetComponent<Camera>();
    }


    // Update is called once per frame
    void LateUpdate()
    {
        float orthoSizeY = _camera.orthographicSize;
        float orthoSizeX = (Screen.width * _camera.orthographicSize)/Screen.height;
        
        
        var targetPosition = Target.transform.position;
        Vector3 cameraDestination = new Vector3(targetPosition.x,targetPosition.y, transform.position.z);

        cameraDestination.y = Mathf.Clamp(cameraDestination.y, BottomLeft.transform.position.y + orthoSizeY, TopRight.transform.position.y - orthoSizeY);
        cameraDestination.x= Mathf.Clamp(cameraDestination.x, BottomLeft.transform.position.x + orthoSizeX, TopRight.transform.position.x - orthoSizeX);

        _transform.position = Vector3.SmoothDamp(transform.position, cameraDestination, ref _velocity, Speed);
    }
}
