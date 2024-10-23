using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public Camera mainCamera; // ��������� �� ������
    private static float cameraDistance = 6f; // ������ ������
    private static float cameraRetreat = -0.01f; // ������� ������
    private static float cameraDistanceMod; // ����������� ������
    private static float cameraForwardMod; // ����������� �������

    public float maxSpeed = 10f; // ����������� ��������
    public float forceTime = 1.0f; // ��� 䳿 �������
    public float forceMultipier = 100f; // ������� ��� ���� 

    private Rigidbody rb;

    public float animTime = 20f;

    public float divider = 2f; // �������, ��� ������

    public static Vector3 scaleMod; // ����������� ������
    private static Vector3 currentScale; // �������� �����
    private static float forwardMod; // ����������� ������������
    private static float sideMod; // ����������� ���������

    void Start()
    {
        // �������� ������ �� ���������� Rigidbody ����� ����� "rb"
        rb = GetComponent<Rigidbody>();
        // ������� ���������� �����, �� ����� "currentScale" 
        currentScale = transform.localScale;
        // ����������� ������, �� ���� ���������� ������ �� ����������� ������
        scaleMod = transform.localScale / divider; // 1 / 2

        // �������� �������� ������ �������� ��� �������
        forwardMod = currentScale.z * 1.3f;
        sideMod = currentScale.x * 0.8f;
    }
    public static void AddSize()
    {
        // �������� "�����" = "�����" + "�����������"
        currentScale = currentScale + scaleMod;

        // �������� �������� ������ �������� ��� �������
        forwardMod = currentScale.z * 1.3f;
        sideMod = currentScale.x * 0.8f;
    }
    public static void AddCameraDistance()
    {
        cameraDistanceMod = currentScale.x;
        cameraForwardMod = currentScale.x / 7f;
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
            cameraDistance + cameraDistanceMod,
            transform.position.z + cameraRetreat + cameraForwardMod);
    }
    private void SlimeMoveAnim()
    {
        // ������ ������� ����� ������
        float forwardScale = Mathf.Lerp(
            transform.localScale.z,
            forwardMod,
            Time.deltaTime / animTime);
        float sideScale = Mathf.Lerp(
            transform.localScale.x,
            sideMod,
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
            currentScale.z,
            (Time.deltaTime / animTime) / 2);
        float sideScale = Mathf.Lerp(
            transform.localScale.x,
            currentScale.x,
            (Time.deltaTime / animTime) / 2);
        // ����������� ���� �� localScale
        transform.localScale = new Vector3(
            sideScale,
            transform.localScale.y,
            forwardScale);
    }
}
