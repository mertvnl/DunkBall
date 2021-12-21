using DunkBall.Ball;
using DunkBall.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DunkBall.Basket
{
    public class ScoreAreaController : MonoBehaviour
    {
        private bool isScored;

        private void OnTriggerEnter(Collider other)
        {
            if (isScored)
                return;

            BallController ball = other.GetComponentInParent<BallController>();

            if (ball != null)
            {
                //Check if its coming from up or bottom
                if (IsBasket(ball.Rb))
                {
                    isScored = true;
                    Debug.Log("Basket, nice!");
                    EventManager.OnBasketScore.Invoke();
                    GameManager.Instance.CompleteStage(true);
                }
            }
        }

        private bool IsBasket(Rigidbody r)
        {
            if (r.velocity.y < 0)
                return true;
            else
                return false;
        }
    }

}
