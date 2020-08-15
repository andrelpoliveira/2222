using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{    
    private Camera cam;

    public Transform playertransform;

    public Transform tirotransform;

    private Player Player;

    private Inimigo Inimigo;

    public float velocidadeCam;
    public Transform LimiteCamEsq, LimiteCamDir, LimiteCamCima, LimiteCamBaixo;

    public Text segundostxt;
    public float segundos = 15;

    [Header("Audio")]
    public AudioSource sfxsource;
    public AudioSource musicasource;

    public AudioClip sfxbotao;
    public AudioClip sfxcolisaotiro;
    public AudioClip sfxtempo1;
    public AudioClip sfxtempo2;
    public AudioClip sfxpassoninja;
    public AudioClip sfxvezjogar;
    public AudioClip sfxkunay;
    public AudioClip[] sfxpasso;


    public GameObject musicaoff;

    public bool pausatempo = true;

    public bool vezplayer = true;
    public bool vezinimigo = false;

    public bool tocou3 = true;
    public bool tocou2 = false;
    public bool tocou1 = false;

    public bool tocavez;

    private CameraTouch CameraTouch;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        Player = FindObjectOfType(typeof(Player)) as Player;

        Inimigo = FindObjectOfType(typeof(Inimigo)) as Inimigo;
        CameraTouch = FindObjectOfType(typeof(CameraTouch)) as CameraTouch;

    }

    // Update is called once per frame
    void Update()
    {
        if (currentstate == gamestate.TITULO)
        {
            musicaoff.SetActive(false);
        }
        else
        {
            musicaoff.SetActive(true);
        }

        if (currentstate == gamestate.TITULO && CrossPlatformInputManager.GetButtonDown("botaoinicio"))
        {
            Playsfx(sfxbotao, 0.5f);
            currentstate = gamestate.GAMEPLAY;
            paineltitulo.SetActive(false);
            pausatempo = false;
        }
        else if (currentstate == gamestate.FIMJOGO && CrossPlatformInputManager.GetButtonDown("botaoreinicia"))
        {
            Playsfx(sfxbotao, 0.5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            pausatempo = true;
        }

        if (segundos <= 15 && pausatempo == false)
        {
            segundos -= Time.deltaTime;
            segundostxt.text = segundos.ToString("F0");

        }

        if (segundos < 4 && tocou3 == true) //se segundos for 3
        {
            tocou3 = false;
            Playsfx(sfxtempo1, 1f);
            tocou2 = true;

        }
        if (segundos < 3 && tocou2 == true) //se segundos for 2
        {
            tocou2 = false;
            Playsfx(sfxtempo2, 1f);
            tocou1 = true;
        }
        if (segundos < 2 && tocou1 == true) //se segundos for 1
        {
            tocou1 = false;
            Playsfx(sfxtempo1, 1f);

        }

        if (segundos < 0) //se segundos for 0
        {
            segundos = 15;
            segundostxt.text = segundos.ToString("F0");
            tocou3 = true;

            if (vezinimigo == false)
            {
                vezinimigo = true;
                vezplayer = false;
                tocavez = false;
                Player.podeatacar = false;
            }
            else
            {
                vezinimigo = false;
                vezplayer = true;
                tocavez = true;
            }

        }

        if (vezplayer == true && tocavez == true)
        {
            Playsfx(sfxvezjogar, 0.9f);
            tocavez = false;
        }

        if (vezinimigo == true || Inimigo.inimigovaiatacar == true)
        {
            Player.velocidade = 0;
        }

        if (vezplayer == true)
        {
            Player.velocidade = 2;
        }

    }

    void LateUpdate()
    {
        if (tirotransform != null)
        {
            if (Player.vaiatacar == true && Player.podeatacar == true && CameraTouch.tocounatela == false) //camera do tiro player
            {

                float posCamArmaX = tirotransform.position.x;
                float posCamArmaY = tirotransform.position.y;

                if (cam.transform.position.x < LimiteCamEsq.position.x && tirotransform.transform.position.x < LimiteCamEsq.position.x)
                {
                    posCamArmaX = LimiteCamEsq.position.x;
                }
                else if (cam.transform.position.x > LimiteCamDir.position.x && tirotransform.transform.position.x > LimiteCamDir.position.x)
                {
                    posCamArmaX = LimiteCamDir.position.x;
                }

                if (cam.transform.position.y < LimiteCamBaixo.position.y && tirotransform.transform.position.y < LimiteCamBaixo.position.y)
                {
                    posCamArmaY = LimiteCamBaixo.position.y;
                }
                else if (cam.transform.position.y > LimiteCamCima.position.y && tirotransform.transform.position.y > LimiteCamCima.position.y)
                {
                    posCamArmaY = LimiteCamCima.position.y;
                }

                Vector3 CamArma = new Vector3(posCamArmaX, posCamArmaY, cam.transform.position.z);
                cam.transform.position = Vector3.Lerp(cam.transform.position, CamArma, velocidadeCam * Time.deltaTime);

            }
        }

        if (Player.vaiatacar == false && vezplayer == true && Inimigo.inimigovaiatacar == false && Player.podeatacar == true && CameraTouch.tocounatela == false) //camera do player
        {

            float posCamX = playertransform.position.x;
            float posCamY = playertransform.position.y;

            if (cam.transform.position.x < LimiteCamEsq.position.x && playertransform.transform.position.x < LimiteCamEsq.position.x)
            {
                posCamX = LimiteCamEsq.position.x;
            }
            else if (cam.transform.position.x > LimiteCamDir.position.x && playertransform.transform.position.x > LimiteCamDir.position.x)
            {
                posCamX = LimiteCamDir.position.x;
            }

            if (cam.transform.position.y < LimiteCamBaixo.position.y && playertransform.transform.position.y < LimiteCamBaixo.position.y)
            {
                posCamY = LimiteCamBaixo.position.y;
            }
            else if (cam.transform.position.y > LimiteCamCima.position.y && playertransform.transform.position.y > LimiteCamCima.position.y)
            {
                posCamY = LimiteCamCima.position.y;
            }
            Vector3 posCam = new Vector3(posCamX, posCamY, cam.transform.position.z);

            cam.transform.position = Vector3.Lerp(cam.transform.position, posCam, velocidadeCam * Time.deltaTime);
        }
        //Debug.Log(vezinimigo);
        //Debug.Log(Player.vaiatacar);
        //Debug.Log(Inimigo.inimigovaiatacar);
        //Debug.Log(Player.podeatacar);
        if (vezinimigo == true && Player.vaiatacar == false && Inimigo.inimigovaiatacar == false && Player.podeatacar == false && CameraTouch.tocounatela == false) //camera inimigo
        {
            //Debug.Log("mudou de camera");
            float posCamInimigoX = inimigoTransform.position.x;
            float posCamInimigoY = inimigoTransform.position.y;

            if (cam.transform.position.x < LimiteCamEsq.position.x && inimigoTransform.transform.position.x < LimiteCamEsq.position.x)
            {
                posCamInimigoX = LimiteCamEsq.position.x;
            }
            else if (cam.transform.position.x > LimiteCamDir.position.x && inimigoTransform.transform.position.x > LimiteCamDir.position.x)
            {
                posCamInimigoX = LimiteCamDir.position.x;
            }

            if (cam.transform.position.y < LimiteCamBaixo.position.y && inimigoTransform.transform.position.y < LimiteCamBaixo.position.y)
            {
                posCamInimigoY = LimiteCamBaixo.position.y;
            }
            else if (cam.transform.position.y > LimiteCamCima.position.y && inimigoTransform.transform.position.y > LimiteCamCima.position.y)
            {
                posCamInimigoY = LimiteCamCima.position.y;
            }
            Vector3 posInimigoCam = new Vector3(posCamInimigoX, posCamInimigoY, cam.transform.position.z);

            cam.transform.position = Vector3.Lerp(cam.transform.position, posInimigoCam, velocidadeCam * Time.deltaTime);
        }

        if (inimigoTirotransform != null)
        {
            if (Inimigo.inimigovaiatacar == true && Player.podeatacar == false && CameraTouch.tocounatela == false) //camera kunay
            {

                float posCamKunayX = inimigoTirotransform.position.x;
                float posCamKunayY = inimigoTirotransform.position.y;

                if (cam.transform.position.x < LimiteCamEsq.position.x && inimigoTirotransform.transform.position.x < LimiteCamEsq.position.x)
                {
                    posCamKunayX = LimiteCamEsq.position.x;
                }
                else if (cam.transform.position.x > LimiteCamDir.position.x && inimigoTirotransform.transform.position.x > LimiteCamDir.position.x)
                {
                    posCamKunayX = LimiteCamDir.position.x;
                }

                if (cam.transform.position.y < LimiteCamBaixo.position.y && inimigoTirotransform.transform.position.y < LimiteCamBaixo.position.y)
                {
                    posCamKunayY = LimiteCamBaixo.position.y;
                }
                else if (cam.transform.position.y > LimiteCamCima.position.y && inimigoTirotransform.transform.position.y > LimiteCamCima.position.y)
                {
                    posCamKunayY = LimiteCamCima.position.y;
                }

                Vector3 CamKunay = new Vector3(posCamKunayX, posCamKunayY, cam.transform.position.z);
                cam.transform.position = Vector3.Lerp(cam.transform.position, CamKunay, velocidadeCam * Time.deltaTime);

            }
        }

    }

    public void Playsfx(AudioClip sfxclip, float volume)
    {
        sfxsource.PlayOneShot(sfxclip, volume);
    }
}
