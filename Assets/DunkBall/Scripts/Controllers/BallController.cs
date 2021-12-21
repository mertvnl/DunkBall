using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DunkBall.InputSystems;

namespace DunkBall.Ball
{
    public class BallController : MonoBehaviour
    {
        #region Getters
        private Rigidbody rb;
        public Rigidbody Rb { get { return rb == null ? rb = GetComponent<Rigidbody>() : rb; } }

        private InputSystem inputSystem;
        public InputSystem InputSystem { get { return inputSystem == null ? inputSystem = GetComponent<InputSystem>() : inputSystem; } }
        #endregion

        [SerializeField] private float jumpPower;
        [SerializeField] private Transform target;

        private void OnEnable()
        {
            InputSystem.OnSwipe.AddListener(JumpToDirection);
        }

        private void OnDisable()
        {
            InputSystem.OnSwipe.RemoveListener(JumpToDirection);
        }

        private void JumpToDirection(SwipeData swipeData)
        {
            switch (swipeData.direction)
            {
                case SwipeDirection.Up:
                    Rb.velocity = Vector3.zero;
                    Jump(Vector3.up + (target.position - transform.position).normalized, swipeData.swipeVelocity);
                    break;
                case SwipeDirection.Down:
                    Jump(Vector3.back, swipeData.swipeVelocity);
                    break;
                case SwipeDirection.Right:
                    Jump(Vector3.right, swipeData.swipeVelocity);
                    break;
                case SwipeDirection.Left:
                    Jump(Vector3.left, swipeData.swipeVelocity);
                    break;
            }
        }

        private void Jump(Vector3 dir, float vel)
        {
            Rb.AddForce(dir * jumpPower * vel);
        }

        private void ConstantJump()
        {
            Rb.AddForce(Vector3.up * jumpPower);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Ground ground = collision.gameObject.GetComponent<Ground>();

            if (ground != null)
            {
                ConstantJump();
            }
        }
    }
}

