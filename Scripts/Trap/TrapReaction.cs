using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapReaction : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision) // 플레이어가 닿으면 트랩시전
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetTrigger("TrapOn");
        }
    }
}
