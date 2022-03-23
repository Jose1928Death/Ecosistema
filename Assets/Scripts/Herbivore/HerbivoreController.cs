using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HerbivoreController : MonoBehaviour
{
    private GameObject[] ojos;
    private GameObject cuerpo;
    private GameObject disparo;
    public GameObject bala;
    private NavMeshAgent nv;
    private Vector3 PosicionObjetivo;
    private Vector3 PosicionPatrulla;
    private bool encontrado;

    Animator animator;

    private int vida = 10;

    private void Awake()
    {
        vision();
    }

    // Start is called before the first frame update
    void Start()
    {
        PosicionObjetivo = Vector3.zero;
        PosicionPatrulla = Vector3.zero;
        encontrado = false;
        //cuerpo.GetComponent<Renderer>().material.color = Color.blue;

        //controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        StartCoroutine("patrullar");
    }

    private void vision()
    {
        ojos = new GameObject[9];
        ojos[0] = transform.GetChild(2).transform.GetChild(0).transform.gameObject;
        ojos[1] = transform.GetChild(2).transform.GetChild(1).transform.gameObject;
        ojos[2] = transform.GetChild(2).transform.GetChild(2).transform.gameObject;
        ojos[3] = transform.GetChild(2).transform.GetChild(3).transform.gameObject;
        ojos[4] = transform.GetChild(2).transform.GetChild(4).transform.gameObject;
        ojos[5] = transform.GetChild(2).transform.GetChild(5).transform.gameObject;
        ojos[6] = transform.GetChild(2).transform.GetChild(6).transform.gameObject;
        ojos[7] = transform.GetChild(2).transform.GetChild(7).transform.gameObject;
        ojos[8] = transform.GetChild(2).transform.GetChild(8).transform.gameObject;

        //cuerpo = transform.GetChild(0).gameObject;

        disparo = transform.GetChild(0).GetChild(1).GetChild(2).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).transform.gameObject;

        nv = GetComponent<NavMeshAgent>();
    }

    IEnumerator patrullar()
    {
        animator.SetFloat("Run", 0);
        //cuerpo.GetComponent<Renderer>().material.color = Color.blue;

        destinoPatrulla();
        nv.SetDestination(PosicionPatrulla);

        while (true)
        {
            yield return new WaitForFixedUpdate();

            if (encontrado==true)
            {
                nv.ResetPath();
                StopAllCoroutines();
                StartCoroutine("perseguir");
                break;
            }

            if (Vector3.Distance(transform.position, PosicionPatrulla) < 4.0f)
            {
                nv.ResetPath();
                StopAllCoroutines();
                StartCoroutine("buscar");
                break;
            }
        }


    }

    IEnumerator perseguir()
    {
        animator.SetFloat("Run", 0);
        //cuerpo.GetComponent<Renderer>().material.color = Color.red;

        while (true)
        {
            yield return new WaitForFixedUpdate();

            if (encontrado == true)
            {
                nv.SetDestination(PosicionObjetivo);

                if (Vector3.Distance(transform.position, PosicionObjetivo) < 5.0f)
                {
                    nv.ResetPath();
                    StopAllCoroutines();
                    StartCoroutine("disparar");
                    break;
                }
            }
            else
            {
                nv.ResetPath();
                StopAllCoroutines();
                StartCoroutine("buscar");
                break;
            }
        }
    }

    IEnumerator buscar()
    {
        animator.SetFloat("Run", 1);
        //cuerpo.GetComponent<Renderer>().material.color = Color.blue;

        float rotacion = 0;
        
        while(rotacion<360)
        {
            yield return new WaitForFixedUpdate();

            transform.Rotate(Vector3.up, 1);

            rotacion++;

            if(encontrado == true)
            {
                break;
            }
        }

        if(encontrado == true)
        {
            nv.ResetPath();
            StopAllCoroutines();
            StartCoroutine("perseguir");
        }
        else
        {
            nv.ResetPath();
            StopAllCoroutines();
            StartCoroutine("patrullar");
        }
    }

    IEnumerator disparar()
    {

        animator.SetFloat("Run", 1);

        nv.ResetPath();
        
        int rafaga = 10;

        while(rafaga>0)
        {
            yield return new WaitForSeconds(0.2f);

            GameObject b = Instantiate(bala, disparo.transform.position, Quaternion.identity);
            b.GetComponent<Rigidbody>().AddForce(disparo.transform.forward*500);
            rafaga--;
        }

        StopAllCoroutines();
        StartCoroutine("buscar");
    }

    private void FixedUpdate()
    {
        mirar();
    }


    private void mirar()
    {
        vision();
        encontrado = false;

        for (int i=0;i<7;i++)
        {
            RaycastHit hit;

            if (Physics.Raycast(ojos[i].transform.position, ojos[i].transform.forward, out hit, 30))
            {
                if(hit.transform.gameObject.tag == "Spider")
                {
                    PosicionObjetivo = hit.point;
                    encontrado = true;
                    Debug.DrawRay(ojos[i].transform.position, ojos[i].transform.forward * hit.distance, Color.yellow);
                }
                else
                {
                    Debug.DrawRay(ojos[i].transform.position, ojos[i].transform.forward * hit.distance, Color.white);
                }
            }
            else
            {
                Debug.DrawRay(ojos[i].transform.position, ojos[i].transform.forward * 30, Color.white);
            }
        }
    }

    private void destinoPatrulla()
    {
        int iteraciones = 0;
        bool existeDestino = false;

        while (iteraciones < 1000)
        {
            iteraciones++;

            int CoorX = Random.Range(0, 100) * 2;
            int CoorZ = Random.Range(0, 100) * 2;

            Vector3 InicioRayo = new Vector3(CoorX, 6, CoorZ);

            if (Physics.Raycast(InicioRayo, Vector3.down, 10))
            {
                PosicionPatrulla = new Vector3(CoorX, 0, CoorZ);
                existeDestino = true;
                break;
            }
        }
        if (existeDestino == false)
        {
            Destroy(this.gameObject);
        }
    }

    public void quitarVida()
    {
        vida--;

        if (vida == 0)
        {
            nv.ResetPath();
            nv = null;
            Destroy(this.gameObject);
        }
    }
}
