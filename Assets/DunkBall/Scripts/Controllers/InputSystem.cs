using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

namespace DunkBall.InputSystems
{
    public class SwipeEvent : UnityEvent<SwipeData> { }
    public enum SwipeDirection
    {
        None,
        Up,
        Down,
        Right,
        Left
    }
    public class SwipeData
    {
        public SwipeDirection direction;
        public float swipeVelocity;

        public SwipeData(SwipeDirection _direciton, float _velocity)
        {
            direction = _direciton;
            swipeVelocity = _velocity;
        }
    }

    public class InputSystem : MonoBehaviour
    {
        [HideInInspector]
        public SwipeEvent OnSwipe = new SwipeEvent();

        [Header("Swipe Settings")]
        [SerializeField] private float swipeHoldThreshold;
        [SerializeField] private float swipeDistanceThreshold;

        Vector2 firstPos;
        Vector2 secondPos;
        float timePassed;

        private void Update()
        {
            GetInputs();
        }

        private void GetInputs()
        {
            if (Input.GetMouseButtonDown(0))
            {
                timePassed = 0;
                firstPos = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                secondPos = Input.mousePosition;

                if (timePassed < swipeHoldThreshold || Vector2.Distance(firstPos, secondPos) < swipeDistanceThreshold)
                    return;

                CalculateDirection();
            }

            if (Input.GetMouseButton(0))
            {
                timePassed += Time.deltaTime;
            }
        }

        private void CalculateDirection()
        {
            SwipeData swipeData = new SwipeData(SwipeDirection.None, 0);

            if (IsVerticalSwipe())
            {
                if (firstPos.y - secondPos.y < 0)
                {
                    swipeData.direction = SwipeDirection.Up;
                    swipeData.swipeVelocity = 1;
                }
                else if (firstPos.y - secondPos.y > 0)
                {
                    swipeData.direction = SwipeDirection.Down;
                    swipeData.swipeVelocity = 1;
                }
            }
            else
            {
                if (firstPos.x - secondPos.x < 0)
                {
                    swipeData.direction = SwipeDirection.Right;
                    swipeData.swipeVelocity = 1;
                }
                else if (firstPos.x - secondPos.x > 0)
                {
                    swipeData.direction = SwipeDirection.Left;
                    swipeData.swipeVelocity = 1;
                }
            }

            Debug.Log(swipeData.direction);
            OnSwipe.Invoke(swipeData);
        }

        private bool IsVerticalSwipe()
        {
            if (VerticalDistance() > HorizontalDistance())
                return true;
            else
                return false;
        }

        private float VerticalDistance()
        {
            return Mathf.Abs(firstPos.y - secondPos.y);
        }

        private float HorizontalDistance()
        {
            return Mathf.Abs(firstPos.x - secondPos.x);
        }
    }
}
