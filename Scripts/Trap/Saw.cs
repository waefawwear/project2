using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Saw : MonoBehaviour
{
    Transform[] points;
    GameObject obj;

    int index = 0;
    float arrivalDistance = 0.3f;

    public GameObject saw;
    public float sawSpeed = 2.0f;

    private void Awake()    // ���� �� ���� ����
    {
        points = new Transform[transform.childCount];

        for(int i=0; i<transform.childCount; i++)
        {
            points[i] = transform.GetChild(i);
        }
        obj = Instantiate(saw);
        obj.transform.position = transform.position;
    }

    void RepeatPoint()  // ��� ������
    {
        obj.transform.position = Vector2.MoveTowards(obj.transform.position, points[index].position, Time.deltaTime * sawSpeed);
    }

    void GoNextPoint()  // ���� ����
    {
        index++;
        index %= transform.childCount;
    }

    private void Update() // ���� �ݺ�
    {
        RepeatPoint();
        if((points[index].position - obj.transform.position).magnitude <= arrivalDistance)
        {
            GoNextPoint();
        }
    }
}
