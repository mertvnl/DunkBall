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

        public float h = 25;
        public float gravity = -18;

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
                    Launch(swipeData.swipeVelocity);
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

        void Launch(float swipeVelocity)
        {
            Physics.gravity = Vector3.up * gravity;
            Rb.velocity = CalculateLaunchData().initialVelocity * swipeVelocity;
        }

        LaunchData CalculateLaunchData()
        {
            float displacementY = target.position.y - Rb.position.y;
            Vector3 displacementXZ = new Vector3(target.position.x - Rb.position.x, 0, target.position.z - Rb.position.z);
            float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
            Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
            Vector3 velocityXZ = displacementXZ / time;

            return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
        }

        struct LaunchData
        {
            public readonly Vector3 initialVelocity;
            public readonly float timeToTarget;

            public LaunchData(Vector3 initialVelocity, float timeToTarget)
            {
                this.initialVelocity = initialVelocity;
                this.timeToTarget = timeToTarget;
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

