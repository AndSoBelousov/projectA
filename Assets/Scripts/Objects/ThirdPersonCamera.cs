using projectAlfa.player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace projectAlfa.Objects
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        private Transform target;        // Цель, за которой будет следить камера (например, персонаж)

        [Header("Настройка характеристик камеры")]
        [SerializeField, Tooltip("Расстояние от камеры до персонажа")]
        private float distance = 5.0f;
        [SerializeField, Tooltip("Высота от центра персонажа, на которой будет находиться камера")]
        private float height = 2.0f;
        //private float rotationDamping = 3.0f;  // Плавность вращения
        [SerializeField, Tooltip("Плавность изменения высоты")]
        private float heightDamping = 2.0f;    // Плавность изменения высоты



        private void Start()
        {
            target = FindFirstObjectByType<PlayerMover>().transform;
        }

        
        void LateUpdate()
        {
            if (!target) return;

            DeltaRay(target);

            float wantedRotationAngle = target.eulerAngles.y;
            float wantedHeight = target.position.y + height;

            float currentRotationAngle = transform.eulerAngles.y;
            float currentHeight = transform.position.y;

            // Плавно изменяем угол поворота камеры
            //currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
            // Преобразуем угол в кватернион
            Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

            transform.position = target.position;
            transform.position -= currentRotation * Vector3.forward * distance;
            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
            transform.LookAt(target);
        }

        private void DeltaRay(Transform target)
        {
            if (!target) return;

           
            Ray ray = new Ray(transform.position, target.position);
            RaycastHit hit;

            // Проверяем пересечение
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit: " + hit.collider.name);
            }
        }

        private void OnDrawGizmos()
        {
            if(!target) return;

            Gizmos.color = Color.yellow;

            Gizmos.DrawLine(transform.position, target.position);
            Gizmos.DrawSphere(target.position, 0.3f);
        }
    }
}

