using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicDanger : MonoBehaviour
{
    public float needToGo = 3f; // �������� � ��� ����� 䳺�
    public float enemySpeed = 5f; // �������� ������ / ��'����
    
    private void Start()
    {
        StartCoroutine(rotateEnemy()); // ��������� ����� ���������
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
    }
    private IEnumerator rotateEnemy()
    {
        // ���������� �������� ����� ���������� ���������� ����� ����
        yield return new WaitForSeconds(needToGo);
        // �������� ������ �� 90 ������� �� �� "y".
        transform.rotation = Quaternion.Euler(
            transform.rotation.x, // x
            90,                   // y
            90);                  // z
    }
    
}
