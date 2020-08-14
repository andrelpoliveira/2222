using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using UnityEngine.UI;
using Project.Networking;
using System;

namespace Project.Menu
{
    public class MenuNetworking : SocketIOComponent
    {

        [Header("Sign In")]
        [SerializeField]
        private GameObject signInContainer;

        private string username;
        private string password;

        private SignInData signInData;

        static SocketIOComponent socketReference;
        

        void Start()
        {

            signInData = new SignInData();
            socketReference = GetComponent<SocketIOComponent>();
            username = "felipe";
            password = "jose";
            //socket.On("open", signInData);

            //signInData = new SignInData();

        

            //base.Start();
            signInContainer.SetActive(true);
            //OnSignIn();

        }

        public void OnSignIn()
        {
            socketReference.Emit("signIn", new JSONObject(JsonUtility.ToJson(signInData)));



            //   { 

            //     username = "jose",
            //     password = "dias"

            //  })));
        }

        public void OnSighInComplete()
        {

            

        }

        public void OnCreateAccount()
        {

            socketReference.Emit("createAccount", new JSONObject(JsonUtility.ToJson(new SignInData()
            {

                username = username,
                password = password

            })));

        }

        public void EditUsername(string text)
        {
            username = text;
        }

        public void EditPassword(string text)
        {
            password = text;
        }
    }
    [Serializable]
    public class SignInData
    {
        public string username;
        public string password;
    }
}