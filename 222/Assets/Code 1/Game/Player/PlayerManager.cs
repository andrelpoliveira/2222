using Project.Utility;
using Project.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
using Debug = UnityEngine.Debug;


namespace Project.Player {

    public class PlayerManager : MonoBehaviour
    {

        public bool isGrounded = false;

        public float JumpForce;
        public float Speed;
        public Rigidbody2D rigidbody;
        public SpriteRenderer spriteRenderer;

        const float BARREL_PIVOT_OFFSET = 90.0f;
        [Header("Data")]
        
        //[SerializeField]
        //private float rotation = 60;


        [Header("Object References")]
        [SerializeField]
        private Transform barrelPivot;
        [SerializeField]
        private Transform bulletSpawnPoint;


        [Header("Class References")]
        [SerializeField]
        private NetworkIdentity networkIdentity;

        private float lastRotation;

        private BulletData bulletData;
        private Cooldown shootingCooldown;


        public void Start()
        {
            shootingCooldown = new Cooldown(1);
            bulletData = new BulletData();
            bulletData.position = new Position();
            bulletData.direction = new Position();


            rigidbody = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Update()
        {
            if (networkIdentity.IsControlling())
            {
                checkMovement();

            }
            // checkAiming();
            // checkShooting();

            if (Input.GetKeyDown(KeyCode.Space) && !isGrounded)
            {
                checkJump();
            }
        }

    


        public float GetLastRotation()
        {
            return lastRotation;
        }
        public void SetRotation(float Value)
        {
            barrelPivot.rotation = Quaternion.Euler(0, 0, Value + BARREL_PIVOT_OFFSET);
        }

    private void checkMovement()
    {
      float horizontal = Input.GetAxis("Horizontal");
      //float vertical = Input.GetAxis("Vertical");


            // transform.position += new Vector3(horizontal, 0, 0) * speed * Time.deltaTime; 

            //transform.position += -transform.up * vertical * speed * Time.deltaTime;
          //transform.Rotate(new Vector3(0, 0, -horizontal * rotation * Time.deltaTime));

         //  float movimento = Input.GetAxis("Horizontal");

            if (horizontal < 0)
            {

                spriteRenderer.flipX = true;

            }
            else if (horizontal > 0)
            {

                spriteRenderer.flipX = false;

            }

              rigidbody.velocity = new Vector2(horizontal * Speed, rigidbody.velocity.y);

         //   transform.position += new Vector3(horizontal, vertical, 0) * speed * Time.deltaTime; 

       //     transform.position += new Vector3(movimento, 0, 0) * velocidadeMaxima * Time.deltaTime;

        }

        private void checkJump()
        {
            
            rigidbody.AddForce(new Vector2(0, JumpForce));

        }
        private void checkAiming()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 dif = mousePosition - transform.position;
            dif.Normalize();
            float rot = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;

            lastRotation = rot;

            barrelPivot.rotation = Quaternion.Euler(0, 0, rot + BARREL_PIVOT_OFFSET);
        }
        private void checkShooting()
        {
            shootingCooldown.CooldownUpdate();

            if(Input.GetMouseButton(0) && !shootingCooldown.IsOnCooldown())
            {
                shootingCooldown.StartCooldown();

               // bulletData.activator = NetworkClient.ClientID;
               // bulletData.position.x = string.Format("{0:N2}", bulletSpawnPoint.position.x.TwoDecimals());
                //bulletData.position.y = string.Format("{0:N2}", bulletSpawnPoint.position.y.TwoDecimals());
               // bulletData.direction.x = string.Format("{0:N2}", bulletSpawnPoint.up.x);
                //bulletData.direction.y = string.Format("{0:N2}", bulletSpawnPoint.up.y);

               

                networkIdentity.GetSocket().Emit("fireBullet", new JSONObject(JsonUtility.ToJson(bulletData)));

            }
        }
}
}
