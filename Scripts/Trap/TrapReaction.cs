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

    private void OnTriggerEnter2D(Collider2D collision) // �÷��̾ ������ Ʈ������
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetTrigger("TrapOn");
        }
    }
}
