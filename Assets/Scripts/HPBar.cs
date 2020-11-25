using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{

    public int HP = 100;
    public Text HP_bar;

    //Вход в зону
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Damage")
        {
            if(HP > 0)
            {
                HP -= 25;

            }
        }


        if (other.transform.tag == "HP_Add")
        {
            if (HP < 100)
            {
                HP += 25;
                Destroy(other.gameObject);
            }
        }
    }

    //Смерть и восполнение
    void Update()
    {
        if (HP < 1)
        {
            transform.parent.gameObject.SetActive(false);
        }
        if (HP > 100)
        {
            HP = 100;
        }
        HP_bar.text = "Здоровье: " + HP;
    }

}
