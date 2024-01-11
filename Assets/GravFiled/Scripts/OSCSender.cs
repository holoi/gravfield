// OSC Jack - Open Sound Control plugin for Unity
// https://github.com/keijiro/OscJack

using UnityEngine;
using System;
using System.Reflection;

namespace OscJack
{
    public sealed class OSCSender : MonoBehaviour
    {


        [SerializeField] OscConnection _connection = null;
        [SerializeField] string _oscAddress = "/unity";

        public Transform player1;
        public Transform player2;
        private float distance;




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

        void Update()
        {
            if (_client == null) return;
            distance = Vector3.Distance(player1.position, player2.position);
            distance = Math.Clamp(distance * 30 , 10 , 100);



            _client.Send(_oscAddress, distance);
        }
    }
}
