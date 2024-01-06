using System;
using ArcaneArchitects.Input;
using UnityEngine;

namespace ArcaneArchitects.Core
{
    [RequireComponent(typeof(Rigidbody))]
    public class Motor : MonoBehaviour
    {
        private Rigidbody m_Rigidbody;

        [SerializeField] private PlayerController controller;
        [SerializeField] private float movementSpeed;

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            var lookDir = ToTopDownVector3(controller.LookDir);
            var movementDir = ToTopDownVector3(controller.MovementDir);

            var position = transform.position;
            Debug.DrawLine(position, position + lookDir, Color.cyan);
            Debug.DrawLine(position, position + movementDir, Color.blue);

            if (lookDir != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(lookDir);
            }
        }

        private void FixedUpdate()
        {
            var movementDir = ToTopDownVector3(controller.MovementDir);
            MoveMotor(transform.position + movementDir * (Time.fixedDeltaTime * movementSpeed));
        }
        
        private void MoveMotor(Vector3 position)
        {
            var oldVelocity = m_Rigidbody.velocity;
            var delta = position - m_Rigidbody.position;
            var velocity = delta / Time.fixedDeltaTime;

            velocity.y = oldVelocity.y;

            m_Rigidbody.velocity = velocity;
        }
        
        private static Vector3 ToTopDownVector3(Vector2 vector)
        {
            return new Vector3(vector.x, 0, vector.y);
        }
    }
}
