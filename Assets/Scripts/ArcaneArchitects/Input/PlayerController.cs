using System;
using ArcaneArchitects.Core;
using UnityEngine;

namespace ArcaneArchitects.Input
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private new Camera camera;
        
        public Vector2 LookDir { get; private set; }
        public Vector2 MovementDir { get; private set; }
        public Vector2 CursorPos { get; private set; }
        
        private InputActions m_Input;
        
        private void Awake()
        {
            m_Input = new InputActions();
        }

        private void Update()
        {
            // For look input, assume mouse position
            var rawLookInput = m_Input.Playing.Look.ReadValue<Vector2>();
            var projectedLookInput = camera.ScreenToWorldPoint(new Vector3(rawLookInput.x, 0, rawLookInput.y));
            var lookDir = projectedLookInput - transform.position;
            LookDir = lookDir;

            var rawMoveInput = m_Input.Playing.Move.ReadValue<Vector2>();
            MovementDir = rawMoveInput;
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
