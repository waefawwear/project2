using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    Transform[] holes;

    //int index = 0;

    private void Awake()
    {
        for(int i = 0; i<transform.childCount; i++)
        {
            holes[i] = transform.GetChild(i);
        }
    }

    void PlayerTeleport()
    {

    }


}
