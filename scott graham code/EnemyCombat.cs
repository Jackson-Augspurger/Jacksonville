using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public GameObject battle1;
    public GameObject battle2;
    public GameObject battle3;


    public float timer = 5.0f;
    // Start is called before the first frame update


    void Update()
    {

        timer += Time.deltaTime;
        if (timer >= 150.0f)
        {
            timer = 0.0f;
        }

        if (timer <= 40f && timer >= 0f)
        {
            battle1.SetActive(true);
            battle2.SetActive(false);
            battle3.SetActive(false);
        }
        else if (timer <= 80f && timer >= 41f)
        {
            battle1.SetActive(false);
            battle2.SetActive(false);
            battle3.SetActive(true);
        }
        else if (timer <= 120f && timer >= 81f)
        {
            battle1.SetActive(false);
            battle2.SetActive(true);
            battle3.SetActive(false);
        }
        else
        {
            battle1.SetActive(true);
            battle2.SetActive(true);
            battle3.SetActive(true);
        }
    }
}
