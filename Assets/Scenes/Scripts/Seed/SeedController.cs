using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedController : MonoBehaviour
{
    private int vida = 20;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void quitarVida()
    {
        vida--;

        if (vida <= 20 && vida > 15)
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else if (vida <= 15 && vida > 5)
        {
            GetComponent<Renderer>().material.color = Color.cyan;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.gray;
        }

        if (vida == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
