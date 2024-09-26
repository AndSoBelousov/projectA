using projectAlfa.Input;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace projectAlfa.player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMover : MonoBehaviour
    {
        private const float GravityValue = -19f;
        [Header("Настройка характеристик персонажа")]
        [SerializeField, Range(1, 10)] private float _moveSpeed = 5;
        [SerializeField, Range(0.8f, 3)] private float _jumpPower = 1.2f;

            
        private CharacterController _characterController;
        private NewInputService _input;
        private Vector3 _direction;




        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();

            _input = new NewInputService();
        }

        private void Update()
        {
            _input.Update();

            _direction = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);

            if (_direction != Vector3.zero)
            {
                // Поворачиваем персонажа в направлении движения
                Vector3 lookAtTarget = transform.position + _direction;
                transform.LookAt(lookAtTarget);
            }

            this._direction.x = _direction.x;
            this._direction.z = _direction.z;

            if (_characterController.isGrounded)
            {
                this._direction.y = 0;

                //if (_input.IsJumpPressed)
                //{
                //    this._direction.y = Mathf.Sqrt(_jumpPower * -2f * GravityValue);
                //}
            }

            //this._direction.y += GravityValue * Time.deltaTime;
            _characterController.Move(this._direction * _moveSpeed * Time.deltaTime);

        }


        private void OnEnable()
        {
            _input.SpacePressed += OnSpacePressed;
        }

        private void OnDisable()
        {
            _input.SpacePressed -= OnSpacePressed;
        }

        private void OnSpacePressed()
        {
          // добавлю в потом  
        }

    }
}