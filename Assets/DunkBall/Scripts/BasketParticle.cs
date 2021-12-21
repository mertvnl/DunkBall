using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DunkBall.Basket
{
    public class BasketParticle : MonoBehaviour
    {
        private Material material;
        public Material Material { get { return material == null ? material = GetComponent<Renderer>().material : material; } }

        private Color32 currentColor;
        private Color32 targetColor;
        private Vector3 targetScale;

        private bool isActivated;
        public void Activate()
        {
            currentColor = Material.color;
            targetColor = new Color32(currentColor.r, currentColor.g, currentColor.b, 0);
            targetScale = transform.localScale + (Vector3.one * 0.2f);

            isActivated = true;
        }

        private void Update()
        {
            if (!isActivated)
                return;

            Material.color = Color.Lerp(Material.color, targetColor, 4 * Time.deltaTime);
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, 2 * Time.deltaTime);

            if (Material.color.a < 0.005f)
            {
                isActivated = false;
                Destroy(gameObject);
            }
        }
    }
}

