using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DunkBall.CameraSystem
{
    public class CameraController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform basket;
        [SerializeField] private Transform ball;

        [Header("Camera Settings")]
        [SerializeField] private Vector3 followOffset = Vector3.zero;


        private void LateUpdate()
        {
            HandleRotation();
            HandlePosition();
        }

        private void HandleRotation()
        {
            transform.LookAt(basket);
        }

        private void HandlePosition()
        {
            Vector3 targetPosition = ball.position + followOffset;
            //Lock Y position of camera
            targetPosition.y = transform.position.y;
            transform.position = targetPosition;
        }
    }
}

