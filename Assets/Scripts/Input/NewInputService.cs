using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace projectAlfa.Input
{
    public class NewInputService : IInputSevice
    {
        private CustomInput _input;

        public Vector2 InputVector { get; private set; }


        public event Action SpacePressed;

        public NewInputService()
        {
            _input = new CustomInput();
            _input.Enable();

            //_input.Player.Jumping.performed += OnSpacePressed;
        }


        public void Update()
        {
            InputVector = _input.Player.Movement.ReadValue<Vector2>();
        }


        private void OnSpacePressed(InputAction.CallbackContext context)
        {
            SpacePressed?.Invoke();
        }
    }

}
