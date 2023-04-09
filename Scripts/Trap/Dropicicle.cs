using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dropicicle : MonoBehaviour
{
    public GameObject icicle;
    GameObject obj;

    float respawnTime = 1.2f;   // ��帧�� ����߸��� ��

    private void Awake()
    {
        MakeIcicle();
    }

    IEnumerator dropIcicle()    // ��帧�� ����߸��� �ڷ�ƾ
    {
        yield return new WaitForSeconds(respawnTime);
        Destroy(obj.gameObject, 0.3f);
        MakeIcicle();
    }

    void MakeIcicle()   // ��帧 �����
    {
        obj = Instantiate(icicle);
        obj.transform.position = transform.position;
        StartCoroutine(dropIcicle());
    }
}
