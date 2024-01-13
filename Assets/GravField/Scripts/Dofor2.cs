using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.VFX;

public class Dofor2 : NetworkBehaviour
{
    //public NetworkVariable<Vector3> _positionS2C = new NetworkVariable<Vector3>(Vector3.zero);
    public Transform Camera;
    public Transform player1;
    public Transform player2;

    void Start()
    {
        Camera = GameObject.Find("Main Camera").GetComponent<Transform>();

    }

    void Update()
    {
        if (Camera == null)
        {
            Camera = GameObject.Find("Main Camera").GetComponent<Transform>();
            return;
        }
        PositionGiveServerRpc(Camera.position , Camera.rotation.eulerAngles);



    }

    [ServerRpc(RequireOwnership = false)]
    private void PositionGiveServerRpc(Vector3 _cameraPosition , Vector3 _cameraRotation , ServerRpcParams serverRpcParams = default)
    {
        ulong Id = serverRpcParams.Receive.SenderClientId;
        if (Id == 1)
        {
            player1.position = _cameraPosition;
            player1.rotation = Quaternion.Euler(_cameraRotation);
        }
        else if (Id == 2)
        {
            player2.position = _cameraPosition;
            player2.rotation = Quaternion.Euler(_cameraRotation);
        }



    }


}
