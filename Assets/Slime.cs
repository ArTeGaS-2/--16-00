using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public Camera mainCamera; // ��������� �� ������
    public float maxSpeed = 10f; // ����������� ��������
    public float forceTime = 1.0f; // ��� 䳿 �������
    public float forceMultipier = 100f; // ������� ��� ���� 

    private Rigidbody rb;

    public float animTime = 20f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        // ������ ������� ���� � ������� �����������
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targetPosition = hit.point;

            // ��������� �������� �� ������� �� ����� �������
            Vector3 direction = (
                targetPosition - transform.position).normalized;
            direction.y = 0;

            // ��������� ������ � �������� �������
            if (direction.magnitude > 0.1f) // ��� �������� ������� ���������
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation, targetRotation, Time.deltaTime * 5f);
            }

            // ������ ��������� � �������� �������
            if (Input.GetMouseButton(0))
            {
                rb.AddForce(direction * forceMultipier * Time.deltaTime,
                    ForceMode.Acceleration);
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

                // ����� �������
                SlimeMoveAnim();
            }
            else
            {
                // ������� �������
                SlimeStopAnim();
            }
        }
        // ������ ����� �� ��'����� ������
        mainCamera.transform.position = new Vector3(
            transform.position.x,
            7,
            transform.position.z - 1f);
    }
    private void SlimeMoveAnim()
    {
        // ������ ������� ����� ������
        float forwardScale = Mathf.Lerp(
            transform.localScale.z,
            1.3f,
            Time.deltaTime / animTime);
        float sideScale = Mathf.Lerp(
            transform.localScale.z,
            0.8f,
            Time.deltaTime / animTime);
        // ����������� ���� �� localScale
        transform.localScale = new Vector3(
            sideScale,
            transform.localScale.y,
            forwardScale);
    }
    private void SlimeStopAnim()
    {
        // ������ ������� ����� ������
        float forwardScale = Mathf.Lerp(
            transform.localScale.z,
            1f,
            Time.deltaTime / animTime);
        float sideScale = Mathf.Lerp(
            transform.localScale.z,
            1f,
            Time.deltaTime / animTime);
        // ����������� ���� �� localScale
        transform.localScale = new Vector3(
            sideScale,
            transform.localScale.y,
            forwardScale);
    }
}
