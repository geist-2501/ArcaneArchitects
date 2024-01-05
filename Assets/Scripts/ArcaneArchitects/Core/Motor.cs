using System;
using UnityEngine;

namespace ArcaneArchitects.Core
{
    [RequireComponent(typeof(Rigidbody))]
    public class Motor : MonoBehaviour
    {
        private Rigidbody m_Rigidbody;
        private Vector3 m_MovementDir;
        private Vector3 m_LookDir;
        
        [SerializeField] private float movementSpeed;

        public Vector2 MovementDir
        {
            set => m_MovementDir = ToPlanarVector3(value);
        }

        public Vector2 LookDir
        {
            set => m_LookDir = ToPlanarVector3(value);
        }

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            var position = transform.position;
            Debug.DrawLine(position, position + m_LookDir, Color.cyan);
            Debug.DrawLine(position, position + m_MovementDir, Color.blue);

            if (m_LookDir != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(m_LookDir);
            }
        }

        private void FixedUpdate()
        {
            MoveMotor(transform.position + m_MovementDir * (Time.fixedDeltaTime * movementSpeed));
        }
        
        private void MoveMotor(Vector3 position)
        {
            var oldVelocity = m_Rigidbody.velocity;
            var delta = position - m_Rigidbody.position;
            var velocity = delta / Time.fixedDeltaTime;

            velocity.y = oldVelocity.y;

            m_Rigidbody.velocity = velocity;
        }
        
        private static Vector3 ToPlanarVector3(Vector2 vector)
        {
            return new Vector3(vector.x, 0, vector.y);
        }
    }
}
