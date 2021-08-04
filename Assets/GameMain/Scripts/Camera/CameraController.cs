using GameFramework.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
    public sealed class CameraController : MonoBehaviour
    {
        public Transform Target
        {
            get; set;
        }

        public float MoveSpeed = 10;
        // Start is called before the first frame update
        private void OnEnable()
        {

        }

        private void OnDisable()
        {

        }

        private void Update()
        {
            if (Target != null)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    new Vector3(Target.position.x, Target.position.y, transform.position.z), MoveSpeed * Time.deltaTime);
            }
        }

    }
}