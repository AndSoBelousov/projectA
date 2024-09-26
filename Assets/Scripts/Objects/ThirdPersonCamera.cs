using projectAlfa.player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace projectAlfa.Objects
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        private Transform target;        // ����, �� ������� ����� ������� ������ (��������, ��������)

        [Header("��������� ������������� ������")]
        [SerializeField, Tooltip("���������� �� ������ �� ���������")]
        private float distance = 5.0f;
        [SerializeField, Tooltip("������ �� ������ ���������, �� ������� ����� ���������� ������")]
        private float height = 2.0f;
        //private float rotationDamping = 3.0f;  // ��������� ��������
        [SerializeField, Tooltip("��������� ��������� ������")]
        private float heightDamping = 2.0f;    // ��������� ��������� ������



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

            // ������ �������� ���� �������� ������
            //currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
            // ����������� ���� � ����������
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

            // ��������� �����������
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

