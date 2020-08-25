using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiro : MonoBehaviour
{
    private Player Player;
    private Rigidbody2D tirorb;    

    public Vector2 direcao;
    private Vector2 posAngulo;

    public SpriteRenderer tiroimagem;

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType(typeof(Player)) as Player;
        tirorb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direcao = tirorb.velocity;
        float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angulo, Vector3.forward);

        tirorb.isKinematic = false;

        if (Player.olhaesquerdo == false)
        {

        }

        else
        {
            tiroimagem.flipY = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Tirocolide"))
        {
            //GameController.Playsfx(GameController.sfxcolisaotiro, 0.4f);

            this.gameObject.SetActive(false);

            Destroy(gameObject, 0.1f);
        }

        if (collision.gameObject.tag.Equals("Player"))
        {
            //GameController.Playsfx(GameController.sfxcolisaotiro, 0.4f);

            //TakeDamage(20);

            this.gameObject.SetActive(false);

            Destroy(gameObject, 0.1f);

            //Player.podeatacar = false;   

        }
        /*
        void TakeDamage(int damage)
        {
            Inimigo.currentHealth -= damage;
            Inimigo.BarraVida.SetHealth(Inimigo.currentHealth);
        }
        */
    }
}
