using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{   
    private GameController GameController; //cria uma variavel GameController que vamos associar ao script depois no start

    public Transform posicaotiro;
    public GameObject TiroPrefab;
    public GameObject GranadaPrefab;

    public float velocidade; //cria uma variavel publica chamada velocidade (float varia somente números de 0 até infinito)
    public float ForcaPulo; //cria uma varialvel flutuante de nome jumpForce

    public Transform ChecaColisaoChao;
    public bool EstaNoChao;
        
    private Rigidbody2D PlayerRb; //cria uma variavel privada chamada playerRb que vai ter as propriedas do rigidbody lida no unity

    public bool olhaesquerdo; //cria uma variavel public de verdadeiro ou falso(bool) chamdo olhaesquerdo
    public Joystick joystick;    
                
    public float anguloarma = 50;     
        
    public Transform iniciomira;
    public Transform fimmira;
    private Vector2 pontoinicio;
    private Vector2 pontofim;
    private Vector2 direcao;

    public float forcabomba = 20;

    public int ForcaBombamax = 60;
    public int currentVida;

    public BarraVida barravida;
    public GameObject BarraForca;

    public bool podediminuir = false;
    public bool apertou = false;

    public Transform iniciomiratiro;
    public Transform fimmiratiro;
    private Vector2 pontoiniciotiro;
    private Vector2 pontofimtiro;
    private Vector2 direcaotiro;
    public float forcatiro = 20;
    public float angulotiro = 0;

    public float valorjoystickanda;
    public float valorjoystickvertical;

    public float anda; //variavel chamada anda que recebe o aperto do teclado seta < ou > e faz o movimento Horizontal do personagem

    // Start is called before the first frame update
    void Start()
    {            

        PlayerRb = GetComponent<Rigidbody2D>(); //inicializa e associa o rigidbody ao playerRb

        GameController = FindObjectOfType(typeof(GameController)) as GameController; //procura o outro script chamado GameController e incializa
        GameController.playertransform = this.transform; //vai pegar as informações do transform do player e associar ao playertransform do GameController

        currentVida = ForcaBombamax;
        barravida.SetVidaMax(ForcaBombamax);
    }

    // Update is called once per frame
    void Update()
    {        
        pontoinicio = iniciomira.position;
        pontofim = fimmira.position;
        direcao = (pontofim - pontoinicio).normalized;

        pontoiniciotiro = iniciomiratiro.position;
        pontofimtiro = fimmiratiro.position;
        direcaotiro = (pontofimtiro - pontoiniciotiro).normalized;


        anda = joystick.Horizontal;

        valorjoystickanda = anda;

        if (anda > 0 && olhaesquerdo == true) //Se anda é maior que 0 e a caixa de marcar do unity for verdadeiro faz a função abaixo Flip
        {
            Flip(); //chama a função Flip criada             
        }

        if (anda < 0 && olhaesquerdo == false)
        {
            Flip();            
        }
        if(anda == 0)
        {
            angulotiro = 0;
        }
        if(anda >= .2f || anda <= -.2f)
        {
            angulotiro = 0;
        }       
               
        /*
            float verticaljoystick = joystick.Vertical;
        valorjoystickvertical = verticaljoystick;

            if (verticaljoystick >= .8f)
            {
                angulotiro = 90;
            }

            if (verticaljoystick <= -.8f)
            {
                angulotiro = -90;
            }

        if (verticaljoystick > .2f && verticaljoystick < .8f)
        {
            angulotiro = 45;
        }
        if (verticaljoystick < -.2f && verticaljoystick > -.8f)
        {
            angulotiro = 315;
        }

        */
        if (olhaesquerdo == true)
        {
            iniciomira.eulerAngles = new Vector3(0, iniciomira.transform.localRotation.y, -anguloarma);
            iniciomiratiro.eulerAngles = new Vector3(0, iniciomiratiro.transform.localRotation.y, -angulotiro);
        }
        else
        {
            iniciomira.eulerAngles = new Vector3(0, iniciomira.transform.localRotation.y, anguloarma);
            iniciomiratiro.eulerAngles = new Vector3(0, iniciomiratiro.transform.localRotation.y, angulotiro);
        }
        
        PlayerRb.velocity = new Vector2(anda * velocidade, PlayerRb.velocity.y); //acessa a velocidade do playerRb que vai receber o anda vezes a velocidade em X, e a velocidade em Y
       
        /*
        if (currentHealth <= 0)
        {
            PlayerAnimator.SetTrigger("morrendo");
            transform.gameObject.SetActive(false);            
            GameController.painelfim.SetActive(true);            
        }
        */
        if (apertou == true && forcabomba < 60 && podediminuir == false)
        {
            forcabomba += 25 * Time.deltaTime;
            if (forcabomba >= 60)
            {
                podediminuir = true;
            }
        }
        if (apertou == true && podediminuir == true)
        {
            forcabomba += -25 * Time.deltaTime;
            if (forcabomba <= 20)
            {
                podediminuir = false;
            }
        }
        barravida.SetVida(Mathf.RoundToInt(forcabomba));

    }

    private void FixedUpdate()
    {
        EstaNoChao = Physics2D.OverlapCircle(ChecaColisaoChao.position, 0.02f);
    }

    void Flip() //criamos uma função que não tem no unity chamada Flip
    {       
        olhaesquerdo = !olhaesquerdo; //olhaesquerdo recebe a diferença de olhaesquerdo        
        Vector3 tempScale = transform.localScale;
        tempScale.x *= -1;
        transform.localScale = tempScale;        
    }

    public void Pular()
    {
        if (EstaNoChao == true)
        {
            //GameController.playSFX(GameController.sfxJump, 0.5f);
            PlayerRb.AddForce(new Vector2(0, ForcaPulo));

        }        
    }
   

    void AtiraBomba()
    {       
            GameObject bombaPreFab = Instantiate(GranadaPrefab, posicaotiro.position, GranadaPrefab.transform.localRotation);
        
            if (olhaesquerdo)
            {
                bombaPreFab.GetComponent<Rigidbody2D>().AddForce(-iniciomira.transform.right * forcabomba * 15);
            }
            else
            {
                bombaPreFab.GetComponent<Rigidbody2D>().AddForce(iniciomira.transform.right * forcabomba * 15);
            }        
    }

    void AtiraTiro()
    {
        GameObject tiroPreFab = Instantiate(TiroPrefab, posicaotiro.position, TiroPrefab.transform.localRotation);

        if (olhaesquerdo)
        {
            tiroPreFab.GetComponent<Rigidbody2D>().AddForce(-iniciomiratiro.transform.right * forcatiro * 15);
        }
        else
        {
            tiroPreFab.GetComponent<Rigidbody2D>().AddForce(iniciomiratiro.transform.right * forcatiro * 15);
        }
    }
    /*
    void Tocapasso()
    {
        GameController.Playsfx(GameController.sfxpasso[Random.Range(0, GameController.sfxpasso.Length)], 0.3f);
    }
    */
    public void Apertougranada()
    {
        BarraForca.SetActive(true);
        apertou = true;        
    }

    public void Soltougranada()
    {       
            //PlayerAnimator.SetTrigger("ataca"); //faz a animação ataca
            //vaiatacar = true;
        apertou = false;
        AtiraBomba();
        forcabomba = 20;
        barravida.SetVida(Mathf.RoundToInt(forcabomba));
        BarraForca.SetActive(false);
    }

    public void Atirou()
    {
        AtiraTiro();
    }

}
