﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    /*
    private GameController GameController; //cria uma variavel GameController que vamos associar ao script depois no start

    public Transform posicaotiro;
    public GameObject tiro;

    public float velocidade; //cria uma variavel publica chamada velocidade (float varia somente números de 0 até infinito)

    private Animator PlayerAnimator; //lê o animator e da seu nome
    private Rigidbody2D PlayerRb; //cria uma variavel privada chamada playerRb que vai ter as propriedas do rigidbody lida no unity

    public bool olhaesquerdo; //cria uma variavel public de verdadeiro ou falso(bool) chamdo olhaesquerdo

    public bool vaiatacar = false;

    public int maxHealth = 100;
    public int currentHealth;
    public BarraVida BarraVida;

    private Atirar Atirar;
    public Text angulotxt;
    public float anguloarma = 20;

    public bool apertou = false;
    public float forcaataque;
    public BarraVida barraforca;

    public bool podeatacar = true;

    public Transform iniciomira;
    public Transform fimmira;
    private Vector2 pontoinicio;
    private Vector2 pontofim;
    private Vector2 direcao;

    public bool apertoucima;
    public bool apertoubaixo;

    private CameraTouch CameraTouch;

    public float andatouch;

    // Start is called before the first frame update
    void Start()
    {
        Atirar = FindObjectOfType(typeof(Atirar)) as Atirar;

        PlayerAnimator = GetComponent<Animator>(); //inicializa o animator e associa ao nome PlayerAnimator
        PlayerRb = GetComponent<Rigidbody2D>(); //inicializa e associa o rigidbody ao playerRb

        GameController = FindObjectOfType(typeof(GameController)) as GameController; //procura o outro script chamado GameController e incializa
        GameController.playertransform = this.transform; //vai pegar as informações do transform do player e associar ao playertransform do GameController

        currentHealth = maxHealth;
        BarraVida.SetMaxHealth(maxHealth);
        barraforca.SetMaxHealth(100);

        CameraTouch = FindObjectOfType(typeof(CameraTouch)) as CameraTouch;
    }

    // Update is called once per frame
    void Update()
    {
        pontoinicio = iniciomira.position;
        pontofim = fimmira.position;
        direcao = (pontofim - pontoinicio).normalized;

        if (CrossPlatformInputManager.GetButtonDown("Horizontal"))
        {
            //CameraTouch.tocounatela = false;
        }

        float anda = CrossPlatformInputManager.GetAxis("Horizontal"); //variavel chamada anda que recebe o aperto do teclado seta < ou > e faz o movimento Horizontal do personagem

        if (anda > 0 && olhaesquerdo == true) //Se anda é maior que 0 e a caixa de marcar do unity for verdadeiro faz a função abaixo Flip
        {
            Flip(); //chama a função Flip criada            
        }

        if (anda < 0 && olhaesquerdo == false)
        {
            Flip();
        }

        andatouch = anda;
        
        if (CrossPlatformInputManager.GetButtonDown("botaocima")) //se aperta a tecla ctrl o personagem ataca
        {
            apertoucima = true;
            GameController.Playsfx(GameController.sfxbotao, 0.5f);
            //CameraTouch.tocounatela = false;
        }

        if (CrossPlatformInputManager.GetButtonUp("botaocima"))
        {
            apertoucima = false;
        }

        if (CrossPlatformInputManager.GetButtonDown("botaobaixo")) //se aperta a tecla ctrl o personagem ataca
        {
            apertoubaixo = true;
            GameController.Playsfx(GameController.sfxbotao, 0.5f);
            //CameraTouch.tocounatela = false;
        }

        if (CrossPlatformInputManager.GetButtonUp("botaobaixo"))
        {
            apertoubaixo = false;
        }

        if (apertoucima && anguloarma <= 90)
        {
            anguloarma += 10 * Time.deltaTime;
        }

        if (apertoubaixo && anguloarma >= 15)
        {
            anguloarma -= 10 * Time.deltaTime;
        }

        angulotxt.text = anguloarma.ToString("F0"); //txtangulo do tipo texto recebe o anguloY convertido para texto se não da erro 

        //iniciomira.eulerAngles = new Vector3(iniciomira.rotation.x, iniciomira.rotation.y, anguloarma);
        if (olhaesquerdo == true)
        {
            iniciomira.eulerAngles = new Vector3(0, iniciomira.transform.localRotation.y, -anguloarma);

        }
        else
        {
            iniciomira.eulerAngles = new Vector3(0, iniciomira.transform.localRotation.y, anguloarma);
           
        }

        PlayerRb.velocity = new Vector2(anda * velocidade, PlayerRb.velocity.y); //acessa a velocidade do playerRb que vai receber o anda vezes a velocidade em X, e a velocidade em Y
        PlayerAnimator.SetInteger("anda", (int)anda); //faz a animação anda, mas converte para inteiro devido no unity ser configurado como int e na programação é float

        if (currentHealth <= 0)
        {
            PlayerAnimator.SetTrigger("morrendo");
            transform.gameObject.SetActive(false);
            GameController.currentstate = gamestate.FIMJOGO;
            GameController.painelfim.SetActive(true);
            GameController.pausatempo = true;
        }

        if (apertou == true && forcaataque < 100 && podeatacar == true)
        {
            forcaataque += 20 * Time.deltaTime;
        }

        barraforca.SetHealth(Mathf.RoundToInt(forcaataque));


    }

    void Flip() //criamos uma função que não tem no unity chamada Flip
    {       
        olhaesquerdo = !olhaesquerdo; //olhaesquerdo recebe a diferença de olhaesquerdo        
        Vector3 tempScale = transform.localScale;
        tempScale.x *= -1;
        transform.localScale = tempScale;

        
    }

    void Atira()
    {
        if (podeatacar == true)
        {
            GameObject tiroPreFab = Instantiate(tiro, posicaotiro.position, tiro.transform.localRotation);

            if (olhaesquerdo)
            {
                tiroPreFab.GetComponent<Rigidbody2D>().AddForce(-iniciomira.transform.right * forcaataque * 15);
            }
            else
            {
                tiroPreFab.GetComponent<Rigidbody2D>().AddForce(iniciomira.transform.right * forcaataque * 15);
            }
        }
    }

    void Tocapasso()
    {
        GameController.Playsfx(GameController.sfxpasso[Random.Range(0, GameController.sfxpasso.Length)], 0.3f);
    }

    public void Apertouataque()
    {
        if (podeatacar == true)
        {
            apertou = true;
            GameController.pausatempo = true;
        }

    }

    public void Soltouataque()
    {
        if (podeatacar == true)
        {
            PlayerAnimator.SetTrigger("ataca"); //faz a animação ataca
            vaiatacar = true;
            apertou = false;

            CameraTouch.tocounatela = false;
        }
    }
    */
}
