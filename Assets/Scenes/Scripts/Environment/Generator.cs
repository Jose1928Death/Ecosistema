using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject egg;
    public GameObject soldier;
    public int totalEggs;
    public int totalSoldiers;
    // Start is called before the first frame update
    void Update()
    {
        while (GameObject.FindGameObjectsWithTag("Egg").Length == 0)
        {
            StartCoroutine("generationEgg");
        }
        while (GameObject.FindGameObjectsWithTag("Soldier").Length == 0)
        {
            StartCoroutine("generationSoldier");
        }
    }

    IEnumerator generationEgg()
    {
        while (totalEggs > 0)
        {
            float x = Random.Range(-35.0F, 35.0F);
            float z = Random.Range(-35.0F, 35.0F);

            Vector3 pos = new Vector3(x, 10, z);

            Instantiate(egg, pos, Quaternion.identity);

            totalEggs--;
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator generationSoldier()
    {
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
