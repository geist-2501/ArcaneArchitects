using System;
using ArcaneArchitects.Core;
using UnityEngine;

namespace ArcaneArchitects.Input
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Motor motor;
        [SerializeField] private new Camera camera;
        
        private InputActions m_Input;
        
        private void Awake()
        {
            m_Input = new InputActions();
        }

        private void Update()
        {
            // For look input, assume mouse position
            var rawLookInput = m_Input.Default.Look.ReadValue<Vector2>();
            var projectedLookInput = camera.ScreenToWorldPoint(new Vector3(rawLookInput.x, 0, rawLookInput.y));
            var lookDir = projectedLookInput - transform.position;
            
            motor.LookDir = lookDir;

            var rawMoveInput = m_Input.Default.Move.ReadValue<Vector2>();
            motor.MovementDir = rawMoveInput;
        }

        private void OnEnable()
        {
            m_Input.Enable();
        }

        private void OnDisable()
        {
            m_Input.Disable();
        }
    }
}
