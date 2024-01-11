using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.VFX;

public class PositionSet : NetworkBehaviour
{
    public Transform Camera;
    private Vector3 _position;

    void Start()
    {
        Camera = GameObject.Find("HoloKit Camera").GetComponent<Transform>();
        if (Camera == null)
            return;
    }

    void Update()
    {
        if (Camera == null)
        {
            Camera = GameObject.Find("HoloKit Camera").GetComponent<Transform>();
            return;

        }
        _position = Camera.position;

    }


}