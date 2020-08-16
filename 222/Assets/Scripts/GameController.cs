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
       
    public float velocidadeCam;
    public Transform LimiteCamEsq, LimiteCamDir, LimiteCamCima, LimiteCamBaixo;

    public Text segundostxt;
    public float segundos = 0;
    /*
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
                
    private CameraTouch CameraTouch;
    */

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        Player = FindObjectOfType(typeof(Player)) as Player;
               
        //CameraTouch = FindObjectOfType(typeof(CameraTouch)) as CameraTouch;

    }

    // Update is called once per frame
    void Update()
    {        
        if (segundos <= 59) //se segundos for menor que 59 vai somando
        {
            segundos += Time.deltaTime;
            segundostxt.text = segundos.ToString("F0");
        }
        
        if (segundos >= 59) //se segundos for 59
        {
            segundos = 0;
            segundostxt.text = segundos.ToString("F0");        
            
        }              

    }
    /*
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

        if (Player.vaiatacar == false && vezplayer == true && Player.podeatacar == true && CameraTouch.tocounatela == false) //camera do player
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
    }

    public void Playsfx(AudioClip sfxclip, float volume)
    {
        sfxsource.PlayOneShot(sfxclip, volume);
    }
    */
}
