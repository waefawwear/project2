using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dropicicle : MonoBehaviour
{
    public GameObject icicle;
    GameObject obj;

    float respawnTime = 1.2f;   // 고드름을 떨어뜨리는 빈도

    private void Awake()
    {
        MakeIcicle();
    }

    IEnumerator dropIcicle()    // 고드름을 떨어뜨리는 코루틴
    {
        yield return new WaitForSeconds(respawnTime);
        Destroy(obj.gameObject, 0.3f);
        MakeIcicle();
    }

    void MakeIcicle()   // 고드름 만들기
    {
        obj = Instantiate(icicle);
        obj.transform.position = transform.position;
        StartCoroutine(dropIcicle());
    }
}
