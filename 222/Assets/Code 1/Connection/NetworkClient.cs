using Project.Scriptable;
using Project.Player;
using Project.Utility.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using Debug = UnityEngine.Debug;
using Project.Utility;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System;
using System.Collections.Specialized;
using Project.Gameplay;

namespace Project.Networking
{
    public class NetworkClient : SocketIOComponent
    {

        [Header("Network Client")]
        [SerializeField]
        private Transform networkContainer;
        [SerializeField]
        private GameObject playerPrefab;
        [SerializeField]
        private ServerObjects serverSpawnables;

        public static string ClientID { get; private set; }

        private Dictionary<string, NetworkIdentity> serverObjects;


        public override void Start()
        {

            base.Start();
            initialize();
            setupEvents();


        }
        public override void Update()
        {

            base.Update();

        }

        private void initialize()
        {
            serverObjects = new Dictionary<string, NetworkIdentity>();
        }

        private void setupEvents()
        {

            On("open", (E) =>
            {

                Debug.Log("Connection made to the server");



            });

            On("register", (E) =>
            {



                ClientID = E.data["id"].ToString().RemoveQuotes();
                Debug.LogFormat("Our Client's ID ({0})", ClientID);


            });

            On("spawn", (E) =>
            {

                string id = E.data["id"].ToString().RemoveQuotes();

                GameObject go = Instantiate(playerPrefab, networkContainer);
                go.name = string.Format("Player({0})", id);
                NetworkIdentity ni = go.GetComponent<NetworkIdentity>();
                ni.SetControllerID(id);
                ni.SetSocketReference(this);

                serverObjects.Add(id, ni);


            });

            On("disconnected", (E) =>
            {

                string id = E.data["id"].ToString().RemoveQuotes();

                GameObject go = serverObjects[id].gameObject;
                Destroy(go);
                serverObjects.Remove(id);

            });

            On("updatePosition", (E) =>
            {
                Debug.Log(E.data["position"]["y"].str);
                string id = E.data["id"].ToString().RemoveQuotes();

                float x = (float.Parse(E.data["position"]["x"].str));
                //float x = (E.data["position"]["x"].f);
                //float y = (E.data["position"]["y"].f);
                float y = (float.Parse(E.data["position"]["y"].str));

                Debug.Log(x);

                NetworkIdentity ni = serverObjects[id];
                ni.transform.position = new Vector3(x, y, 0);

            });

            On("signIn", (E) =>
            {

            });

            On("updateRotation", (E) =>
            {
                string id = E.data["id"].ToString().RemoveQuotes();
                float tankRotation = E.data["tankRotation"].f;
                float barrelRotation = E.data["barrelRotation"].f;

                NetworkIdentity ni = serverObjects[id];
                ni.transform.localEulerAngles = new Vector3(0, 0, tankRotation);
                ni.GetComponent<PlayerManager>().SetRotation(barrelRotation);
            });

            On("serverSpawn", (E) =>
            {
                string name = E.data["name"].str;
                string id = E.data["id"].ToString().RemoveQuotes();
                float x = (E.data["position"]["x"].f);
                float y = (E.data["position"]["y"].f);



                //float x = (float.Parse(E.data["position"]["x"].str));
                //float y = (float.Parse(E.data["position"]["y"].str));
                Debug.LogFormat("Server wants us to spawn a '{0}'", name);

                if (!serverObjects.ContainsKey(id))
                {
                    ServerObjectData sod = serverSpawnables.GetObjectByName(name);
                    var spawnedObject = Instantiate(sod.Prefab, networkContainer);
                    spawnedObject.transform.position = new Vector3(x, y, 0);
                    var ni = spawnedObject.GetComponent<NetworkIdentity>();
                    ni.SetControllerID(id);
                    ni.SetSocketReference(this);

                    if (name == "Bullet")
                    {
                        float directionX = (E.data["direction"]["x"].f) / 100;
                        float directionY = (E.data["direction"]["y"].f) / 100;
                        string activator = E.data["activator"].ToString().RemoveQuotes();

                        float rot = Mathf.Atan2(directionY, directionX) * Mathf.Rad2Deg;
                        Vector3 currentRotation = new Vector3(0, 0, rot - 90);
                        spawnedObject.transform.rotation = Quaternion.Euler(currentRotation);

                        WhoActivatedMe whoActivatedMe = spawnedObject.GetComponent<WhoActivatedMe>();
                        whoActivatedMe.SetActivator(activator);

                        
                    }

                    serverObjects.Add(id, ni);
                }
            });
            On("serverUnspawn", (E) =>
            {
                string id = E.data["id"].ToString().RemoveQuotes();
                NetworkIdentity ni = serverObjects[id];
                serverObjects.Remove(id);
                DestroyImmediate(ni.gameObject);
            });

            On("playerDied", (E) =>
            {

                string id = E.data["id"].ToString().RemoveQuotes();
                NetworkIdentity ni = serverObjects[id];
                ni.gameObject.SetActive(false);

            });

            On("playerRespawn", (E) =>
            {

                string id = E.data["id"].ToString().RemoveQuotes();
                float x = (E.data["position"]["x"].f);
                float y = (E.data["position"]["y"].f);
                NetworkIdentity ni = serverObjects[id];
                ni.transform.position = new Vector3(x, y, 0);
                ni.gameObject.SetActive(true);

            });

        }

    }


    [Serializable]
    public class Player
    {
        public string id;
        public Position position;

    }
    [Serializable]
    public class Position
    {
        public string x;
        public string y;
    }
    [Serializable]
    public class PlayerRotation
    {
        public float tankRotation;
        public float barrelRotation;
    }

    [Serializable]
    public class BulletData
    {
        public string id;
        public string activator;
        public Position position;
        public Position direction;
    }
    [Serializable]
    public class IDData
    {
        public string id;
    }
}