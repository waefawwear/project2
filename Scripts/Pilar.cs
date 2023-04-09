using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pilar : MonoBehaviour
{
    GameObject chain;

    bool onTrigger = false;
    float timer = 0.0f;
    float rotateSpeed = 5.0f;

    private void Awake()
    {
        chain = transform.GetChild(1).gameObject;
    }

    public void TriggerOn()
    {
        chain.SetActive(false);
        onTrigger = true;
    }

    private void Update()
    {
        if (onTrigger)
        {
            while(timer <= 90)
            {
                timer += Time.deltaTime;
                transform.Rotate(0, 0, timer * rotateSpeed);
            }
        }
    }
}
