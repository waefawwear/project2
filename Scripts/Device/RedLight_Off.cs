using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RedLight_Off : TrapManager
{
    SpriteRenderer rend;
    private bool switchOn = false;

    public Sprite lightOn;
    public Sprite lightOff;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = lightOff;
    }

    protected override void OnTrapTrigger()
    {
        if (rend.sprite == lightOn)
        {
            SwitchOff();
        }
        else
        {
            SwitchOn();
        }
    }

    public void SwitchOn()
    {
        rend.sprite = lightOn;
        switchOn = false;
        player.jumpForce = 7.0f;
    }

    public void SwitchOff()
    {
        rend.sprite = lightOff;
        switchOn = true;
        player.jumpForce = 15.0f;
    }
}
