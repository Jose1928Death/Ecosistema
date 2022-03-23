using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class automata : Agent
{
    private Vector3 startA;
    public GameObject comida;
    public int velocidad;

    public override void Initialize()
    {
        startA = transform.position;
    }
    
    public override void OnEpisodeBegin()
    {
        
        // posicionamos al agente
        transform.position = startA;
        transform.rotation = Quaternion.Euler(Vector3.up * Random.Range(0f, 360f));

        // Posicionamos la comida
        float CCx = Random.Range(-5.0f, 5.0f);
        float CCz = Random.Range(-5.0f, 5.0f);
        comida.transform.position = new Vector3(startA.x+CCx,1,startA.z+CCz);
    }
    
    public override void OnActionReceived(ActionBuffers actions)
    {                
        int movimiento = 0;
        int giro = 0;

        switch(actions.DiscreteActions[0])
        {
            case 1: //adelante
                movimiento = 1;
            break;
            case 2: // atras
                movimiento = -1;
            break;
        }

        switch (actions.DiscreteActions[1])
        {
            case 1: // derecha
                giro = 1;
                break;
            case 2: // izquierda
                giro = -1;
                break;
        }
        
        if(movimiento!=0)
        {
            transform.position = transform.position + transform.forward * movimiento * velocidad * Time.fixedDeltaTime;
        }

        if(giro!=0)
        {
            transform.Rotate(Vector3.up, giro);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {                
        if (collision.gameObject.tag == "Herbivore")
        {
            Debug.Log("acierta");
            AddReward(1f);
            EndEpisode();
        }
        else if (collision.gameObject.tag == "fail")
        {
            Debug.Log("falla");
            AddReward(-1f);
            EndEpisode();
        }
        
    }
}
