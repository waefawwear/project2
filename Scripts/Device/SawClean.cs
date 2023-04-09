using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawClean : MonoBehaviour
{
    public float rotateSpeed = 5.0f;

    void Update()   // ≈È¥œ µπ∏Æ±‚
    {
        transform.Rotate(new Vector3(0, 0, Time.deltaTime * rotateSpeed));
    }
}
