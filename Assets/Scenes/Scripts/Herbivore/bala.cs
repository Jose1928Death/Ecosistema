using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bala : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("destruir", 1.5f);
    }

    // Update is called once per frame
    private void destruir()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Spider")
        {
            other.gameObject.GetComponent<SeedController>().quitarVida();
        }
    }
}
