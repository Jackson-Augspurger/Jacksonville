using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawning : MonoBehaviour
{

    public Transform[] spawnLocation;
    public GameObject[] whattoSpawn;
    public GameObject[] whattoSpawnclone;

    public float timer = 5.0f;
    // Start is called before the first frame update
  

     void Update()
    {

        timer += Time.deltaTime;
        if (timer >= 50.0f)
        {
            timer = 0.0f;
        }

        if (timer <= 0.01f)
        {
            whattoSpawnclone[0] = Instantiate(whattoSpawn[0], spawnLocation[0].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            whattoSpawnclone[1] = Instantiate(whattoSpawn[0], spawnLocation[0].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            whattoSpawnclone[2] = Instantiate(whattoSpawn[0], spawnLocation[0].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        }
    }

  

  
}
