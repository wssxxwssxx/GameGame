using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{

    public int HP = 100;
    public Text HP_bar;
    public GameObject HP_Add_Module;
    //Вход в зону
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "damage")
        {
            HP = HP - 25;
        }
        if (other.tag == "HP_Add")
        {
            HP = HP + 25;
            HP_Add_Module.SetActive(false);//удаление аптечки
        }
    }

    //Смерть и восполнение
    void Update()
    {
        if (HP < 1)
        {
            gameObject.SetActive(false);
        }
        if (HP > 100)
        {
            HP = 100;
        }
        HP_bar.text = "Здоровье: " + HP;
    }

}
