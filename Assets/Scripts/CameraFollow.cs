using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;


    private void Update()
    {
        var pos = player.position;
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }
}
