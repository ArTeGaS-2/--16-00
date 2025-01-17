using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicDanger : MonoBehaviour
{
    public float needToGo = 3f; // �������� � ��� ����� 䳺�
    public float objectSpeed = 5f; // �������� ������ / ��'����

    private Rigidbody rb; // ����� ��� ������� �� ����������

    private float currentAngleMod = 0f; // �������� ����������� ��������
    public float anglePerIteration = 90f; // ���� ���� �� ��������

    private void OnTriggerEnter(Collider other) // �������� ��������
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject); // ����� ��'��� � ���� ��������
        }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // ����'����� Rigidbody �� ����� "rb"
        StartCoroutine(RotateDynamicDanger());
    }
    private void Update()
    {
        // �������� ��'��� � ��������
        rb.velocity = Vector3.left * // �������� - ������(forward)
                      objectSpeed * // ��������
                      Time.deltaTime; // ���������� �������� ����
    }
    private IEnumerator RotateDynamicDanger()
    {
        while (true)
        {
            yield return new WaitForSeconds(needToGo); // �������� � ��������
            // ���� ��������� ����
            
            Vector3 currenEulerAngles = transform.rotation.eulerAngles;

            currenEulerAngles.z += currentAngleMod;

            transform.rotation = Quaternion.Euler(currenEulerAngles);

            currentAngleMod += anglePerIteration; // ���� �������� ������������
        }
    }
}
