using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DunkBall.Basket
{
    public class BasketEffectController : MonoBehaviour
    {
        [SerializeField] private GameObject poofParticle;

        private void OnEnable()
        {
            EventManager.OnBasketScore.AddListener(OnBasket);
        }

        private void OnDisable()
        {
            EventManager.OnBasketScore.RemoveListener(OnBasket);
        }

        private void OnBasket()
        {
            //Basket visual effect
            StartCoroutine(PlayParticlesCo());
        }

        private IEnumerator PlayParticlesCo()
        {
            float particleDelay = 0.75f;
            while (true)
            {
                Instantiate(poofParticle, GetRandomPosition(), poofParticle.transform.rotation, transform);
                yield return new WaitForSeconds(particleDelay);
            }
        }

        private Vector3 GetRandomPosition()
        {
            Vector3 pos = (Random.insideUnitSphere * 3) + transform.position + Vector3.up;
            pos.z = transform.position.z;

            return pos;
        }
    }
}

