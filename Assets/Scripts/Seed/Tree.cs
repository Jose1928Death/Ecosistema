using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public GameObject seed;
    private GameObject seedCreation;
    // Start is called before the first frame update
    void Start()
    {
        seedCreation = transform.gameObject;
        StartCoroutine("createSeed");
    }
    IEnumerator createSeed()
    {
            while (true){
                yield return new WaitForSeconds(4.0f);

                float x = seedCreation.transform.position.x + Random.Range(-2f, 2f);
                float z = seedCreation.transform.position.z + Random.Range(-2f, 2f);
                float y = seedCreation.transform.position.y;

                Vector3 pos = new Vector3(x, y, z);

                Instantiate(seed, pos, Quaternion.identity);
            }
    }
}
