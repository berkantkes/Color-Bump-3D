using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float cameraSpeed = 0.15f;

    [HideInInspector] public Vector3 camVelocity;

    private void Update()
    {
        if (FindObjectOfType<PlayerController>().canMove)
        {
            transform.position += Vector3.forward * cameraSpeed * Time.deltaTime;
        }
        camVelocity = Vector3.forward * cameraSpeed * Time.deltaTime;

    }

}
