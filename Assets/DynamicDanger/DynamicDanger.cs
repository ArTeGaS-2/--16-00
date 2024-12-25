using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicDanger : MonoBehaviour
{
    public float needToGo = 3f; // �������� � ��� ����� 䳺�
    public float enemySpeed = 5f; // �������� ������ / ��'����

    private float currentAngleY = 90f; // ��������� �� Y
    private float currentAngleZ = 90f; // ��������� �� Z
    
    private void Start()
    {
        StartCoroutine(RotateEnemy()); // ��������� ����� ���������
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
    }
    private IEnumerator RotateEnemy()
    {
        while (true)
        {
            // ���������� �������� ����� ���������� ���������� ����� ����
            yield return new WaitForSeconds(needToGo);
            // �������� ������ �� 90 ������� �� �� "y".
            transform.rotation = Quaternion.Euler(
                transform.rotation.x, // x
                currentAngleY,        // y
                currentAngleZ); // z

            currentAngleY += 90;
        }
    }
    private void Update()
    {
        transform.position = new Vector3(
            transform.position.x + enemySpeed * Time.deltaTime,
            transform.position.y ,
            transform.position.z);
    }
}
