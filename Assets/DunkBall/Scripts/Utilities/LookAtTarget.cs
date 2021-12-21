using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DunkBall.Utilities
{
    public class LookAtTarget : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float damping;
        private void Update()
        {
            LookAt();
        }

        private void LookAt()
        {
            var lookPos = target.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
        }
    }
}

