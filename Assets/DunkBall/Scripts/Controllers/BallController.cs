using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DunkBall.InputSystems;
using DunkBall.Managers;

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

        [SerializeField] private bool isControlable;
        [SerializeField] private float jumpPower;
        [SerializeField] private Transform target;
        [SerializeField] private float h = 5f;
        [SerializeField] private float gravity = -9.81f;
        [Space]
        [SerializeField] private GameObject jumpParticlePrefab;

        private void OnEnable()
        {
            EventManager.OnLevelStarted.AddListener(() => isControlable = true);
            InputSystem.OnSwipe.AddListener(MoveToDirection);
            EventManager.OnGameWin.AddListener(OnWin);
        }

        private void OnDisable()
        {
            EventManager.OnLevelStarted.RemoveListener(() => isControlable = true);
            InputSystem.OnSwipe.RemoveListener(MoveToDirection);
            EventManager.OnGameWin.RemoveListener(OnWin);
        }

        private void MoveToDirection(SwipeData swipeData)
        {
            if (!isControlable)
                return;

            switch (swipeData.direction)
            {
                case SwipeDirection.Up:
                    Launch(swipeData.swipeVelocity);
                    break;
                case SwipeDirection.Down:
                    Jump(-Camera.main.transform.forward, swipeData.swipeVelocity);
                    break;
                case SwipeDirection.Right:
                    Jump(Camera.main.transform.right, swipeData.swipeVelocity);
                    break;
                case SwipeDirection.Left:
                    Jump(-Camera.main.transform.right, swipeData.swipeVelocity);
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
            if (!isControlable)
                return;

            Ground ground = collision.gameObject.GetComponent<Ground>();

            if (ground != null)
            {
                ConstantJump();
                Instantiate(jumpParticlePrefab, new Vector3(transform.position.x, ground.transform.position.y + 0.1f, transform.position.z), jumpParticlePrefab.transform.rotation);
            }
        }

        private void OnWin()
        {
            Rb.mass = 0.01f;
            isControlable = false;
        }
    }
}

