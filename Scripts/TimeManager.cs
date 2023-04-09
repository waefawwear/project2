using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI time;
    public GameObject[] destroyBlock;

    public float timer = 10.0f;

    private void Awake()
    {
        timer = Mathf.Clamp(timer, 0.0f, timer);
    }

    private void Update()
    {
        if(timer >= 0.0f)
        {
            timer -= Time.deltaTime;
            if (time != null)
            {
                time.text = $"{timer:N1}";
            }
        }

        if(timer <= 0.0f)
        {
            if (time != null)
            {
                time.text = "0.0";
            }
            if (destroyBlock != null)
            {
                foreach(var block in destroyBlock)
                {
                    Destroy(block);
                }
            }
        }
    }
}
