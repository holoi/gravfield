using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.VFX;

public class Do : NetworkBehaviour
{
    //public NetworkVariable<Vector3> _positionS2C = new NetworkVariable<Vector3>(Vector3.zero);
    public Transform Camera;
    public Transform player1;
    public Transform player2;

    void Start()
    {
        Camera = GameObject.Find("Main Camera").GetComponent<Transform>();
        if (Camera == null)
            return;
    }

    void Update()
    {
        if (Camera == null)
        {
            Camera = GameObject.Find("Main Camera").GetComponent<Transform>();
            return;
        }
        PositionGiveServerRpc(Camera.position);

    }

    [ServerRpc(RequireOwnership = false)]
    private void PositionGiveServerRpc(Vector3 _cameraPosition , ServerRpcParams serverRpcParams = default)
    {
        ulong Id = serverRpcParams.Receive.SenderClientId;
        if (Id == 1)
        {
            player1.position = _cameraPosition;
        }
        else if (Id == 2)
        {
            player2.position = _cameraPosition;
        }



    }


}
