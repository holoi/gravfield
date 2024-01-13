// OSC Jack - Open Sound Control plugin for Unity
// https://github.com/keijiro/OscJack

using UnityEngine;
using System;
using System.Reflection;

namespace OscJack
{
    public sealed class OSCSenderDistancefor2 : MonoBehaviour
    {


        [SerializeField] OscConnection _connection = null;
        private string _oscAddress = "/distance";
        private string _oscAddress_rotation = "/rotation";
        private string _oscAddress_down1 = "/down1";
        private string _oscAddress_down2 = "/down2";
        private string _oscAddress_up1 = "/up1";
        private string _oscAddress_up2 = "/up2";
        

        public Transform player1;
        public Transform player2;
        private float distance;
        private float rotation1;
        private float rotation2;
        private float totalRotation;        
        private float height1;
        private float height2;
        private bool isDown1 = false;
        private bool isDown1Last = false;
        private bool startDown1 = false;
        private bool endDown1 = false;
        private bool isDown2 = false;
        private bool isDown2Last = false;
        private bool startDown2 = false;
        private bool endDown2 = false;

        public float standardHeight = -0.1f;



        OscClient _client;

        void UpdateSettings()
        {
            if (_connection != null)
                _client = OscMaster.GetSharedClient(_connection.host, _connection.port);
            else
                _client = null;
        }

        void Start()
        {
            UpdateSettings();
        }

        void OnValidate()
        {
            if (Application.isPlaying) UpdateSettings();
        }

        private void Compute()
        {
            distance = Vector3.Distance(player1.position, player2.position);
            distance = Math.Clamp(70 - distance * 30 , 10 , 100);

            rotation1 = Math.Clamp(Vector3.Angle(player1.up, Vector3.up) / 80 , 0 , 1);
            rotation2 = Math.Clamp(Vector3.Angle(player2.up, Vector3.up) / 80 , 0 , 1);
            totalRotation = (rotation1 + rotation2) / 2;
        }

        private void ComputeHeight()
        {
            isDown1Last = isDown1;
            isDown2Last = isDown2;
            height1 = player1.position.y;
            height2 = player2.position.y;
            isDown1 = height1 < standardHeight;
            isDown2 = height2 < standardHeight;
            startDown1 = isDown1 && !isDown1Last;
            endDown1 = !isDown1 && isDown1Last;
            startDown2 = isDown2 && !isDown2Last;
            endDown2 = !isDown2 && isDown2Last;
        }


        void Update()
        {
            if (_client == null) return;

            Compute();
            ComputeHeight();

            if (startDown1)
                _client.Send(_oscAddress_down1, 1);
            if (endDown1)
                _client.Send(_oscAddress_up1, 1);
            if (startDown2)
                _client.Send(_oscAddress_down2, 1);
            if (endDown2)
                _client.Send(_oscAddress_up2, 1);


            _client.Send(_oscAddress, distance);
            _client.Send(_oscAddress_rotation, totalRotation);
        }
    }
}
