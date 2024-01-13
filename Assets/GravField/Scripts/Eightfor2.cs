using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public class Eightfor2 : NetworkBehaviour
{
    public Transform target1;
    public Transform target2;
    public float speed = 1f;
    public float lenth = 1f;
    public float width = 1f;
    private float t = 0f;
    private float dis;
    public float dis0_1;
    private Vector3 _position;

    private void Compute()
    {
        dis = Vector3.Distance(target1.position, target2.position) / 5;
        t += Time.deltaTime * speed;

        float x = Mathf.Sin(t);
        float y = Mathf.Sin(t) * Mathf.Cos(t);

        Vector3 midpoint = (target1.position + target2.position) / 2;
        Vector3 direction = (target2.position - target1.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        rotation *= Quaternion.Euler(0, 90, 0); 
        Vector3 localPosition = new Vector3(x * lenth, 0, y * width) * dis;



        Vector3 worldPosition = midpoint + rotation * localPosition;

        dis0_1 = (float)(Vector3.Distance(midpoint, worldPosition) / 0.25 / lenth);
        dis0_1 = Math.Clamp(dis0_1 , 0 , 1);
        _position = worldPosition;
        
        //Debug.Log(Vector3.Distance(midpoint, worldPosition) / 0.25 / lenth);
    }

    [ClientRpc]
    private void PositionGiveClientRpc(Vector3 _position)
    {
        this.transform.position = _position;
    }



    void Update()
    {
        if (IsServer)
        {
            Compute();
            PositionGiveClientRpc(_position);
        }
    }


}