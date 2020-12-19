using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject blowPrefa;
    private GameController _GC;

    void Start()
    {
        _GC = FindObjectOfType(typeof(GameController)) as GameController;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {

            case ("Bullet"):
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
                GameObject temp = Instantiate(blowPrefa, transform.localPosition, transform.localRotation);
                Destroy(temp, 0.5f);

                _GC.SpawnLoot(this.transform);
              
                break;
        
        }
    }
}
