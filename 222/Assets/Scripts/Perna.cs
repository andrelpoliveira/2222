using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perna : MonoBehaviour
{
    private Animator animacorre;
    private Player Player;

    // Start is called before the first frame update
    void Start()
    {
        animacorre = GetComponent<Animator>();
        Player = FindObjectOfType(typeof(Player)) as Player;
    }

    // Update is called once per frame
    void Update()
    {
        //animacorre.SetInteger("anda", (int)Player.anda); //faz a animação anda, mas converte para inteiro devido no unity ser configurado como int e na programação é float
        if(Player.anda != 0)
        {
            animacorre.SetBool("corre", true);
        }
        else
        {
            animacorre.SetBool("corre", false);
        }
        
    }
}
