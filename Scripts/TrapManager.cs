using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    protected Player player;

    protected virtual void OnTrapTrigger()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<Player>();
            player.onTrigger += OnTrapTrigger;
            player.KeyBoard.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(player != null)
            {
                player.KeyBoard.SetActive(false);
                player.onTrigger -= OnTrapTrigger;
                player = null;
            }
        }
    }
}
