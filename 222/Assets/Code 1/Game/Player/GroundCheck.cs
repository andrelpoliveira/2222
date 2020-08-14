using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player
{
    public class GroundCheck : MonoBehaviour
    {

        PlayerManager Player;


        void Start()
        {
            Player = gameObject.transform.parent.gameObject.GetComponent<PlayerManager>();
        }


        void OnCollisionEnter2D(Collision2D collisor)
        {
            if (collisor.gameObject.layer == 8)
            {
                Player.isGrounded = false;
            }
        }

        public void OnCollisionExit2D(Collision2D collisor)
        {
            if (collisor.gameObject.layer == 8)
            {
                Player.isGrounded = true;
            }
        }
    }
}