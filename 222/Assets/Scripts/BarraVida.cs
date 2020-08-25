using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image CampoBarra;

    private Player Player;

    void Start()
    {
        Player = FindObjectOfType(typeof(Player)) as Player;
    }

    void Update()
    {
        if (Player.olhaesquerdo == true)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    

    public void SetVidaMax(int vida)
    {
        slider.maxValue = vida;
        slider.value = vida;

        CampoBarra.color = gradient.Evaluate(1f);
    }


    public void SetVida(int vida)
    {
        slider.value = vida;
        CampoBarra.color = gradient.Evaluate(slider.normalizedValue);
    }
        
}
