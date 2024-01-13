// OSC Jack - Open Sound Control plugin for Unity
// https://github.com/keijiro/OscJack

using UnityEngine;
using System;
using System.Reflection;

namespace OscJack
{
    public sealed class OSCSenderLowPass : MonoBehaviour
    {


        [SerializeField] OscConnection _connection = null;
        [SerializeField] string _oscAddress = "/unity";

        public Eight eight;
        private float lowPass;




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

            lowPass = 1 - eight.dis0_1;




            _client.Send(_oscAddress, lowPass);
        }
    }
}
