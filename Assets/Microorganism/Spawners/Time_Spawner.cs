using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Time_Spawner : MonoBehaviour
{
    // ������ � gameObject(����� ��'����) ��� ������
    public List<GameObject> organisms;

    // �����, � ����� ������ ���������� ��'����
    public float radius = 5f;

    // �������� ���� ������ ��'����
    public float delay = 0.3f;

    // ���������� � ���� ����������� ��'���
    private float x_Pos;
    private float z_Pos;
    private void Start()
    {
        StartCoroutine(spawnCreature());
    }
    private IEnumerator spawnCreature()
    {
        while (true)
        {
            // ��������� �������� ���������� �� "x" �� "z" � �������
            x_Pos = Random.Range(-radius, radius);
            z_Pos = Random.Range(-radius, radius);

            // ������� ���������� �����������, � �������
            GameObject objectToSpawn = organisms[
                Random.Range(0, organisms.Count - 1)];

            // ��������� ����� ��������� �� �������� ���
            Vector3 spawnPosition = new Vector3(
                x_Pos,
                objectToSpawn.transform.position.y,
                z_Pos);

            // ��������� ������� ��'���, � �������� ����, ��� ���� ���������
            Instantiate(objectToSpawn, spawnPosition, objectToSpawn.transform.rotation);

            yield return new WaitForSeconds(delay);
        }
    }
}
