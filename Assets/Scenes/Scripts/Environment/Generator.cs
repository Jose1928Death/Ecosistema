using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject seed;
    public GameObject soldier;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("generation");
    }

    IEnumerator generation()
    {
        int totalSeeds = 3;
        int totalSoldiers = 3;

        while (totalSeeds > 0)
        {
            float x = Random.Range(-35.0F, 35.0F);
            float z = Random.Range(-35.0F, 35.0F);

            Vector3 pos = new Vector3(x, 10, z);

            Instantiate(seed, pos, Quaternion.identity);

            totalSeeds--;
            yield return new WaitForSeconds(0.5f);
        }
        while (totalSoldiers > 0)
        {
            float x = Random.Range(-38.0F, 38.0F);
            float z = Random.Range(-38.0F, 38.0F);

            Vector3 pos = new Vector3(x, 1, z);

            Instantiate(soldier, pos, Quaternion.identity);

            totalSoldiers--;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
