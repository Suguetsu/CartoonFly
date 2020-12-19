using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] powerUps;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // reponsavel por gerenciar a chance de pegar loot
   public void SpawnLoot(Transform enemy)
    {
        int chance = Random.Range(0, 100);
        int index;

        if (chance > 50)
        {
            int percent = Random.Range(0, 100);


            if(percent > 85)
            {
                index = 2;
                
                // bomb
            }else if(percent > 65)
            {
                index = 1;
            }
            else
            {
                index = 0;
            }

            Instantiate(powerUps[index].transform, enemy.position, enemy.transform.localRotation);
        }
    }
}
