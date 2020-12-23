using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject blowPrefa;
    private GameController _GC;

    private PlayerController _Pcontrol;

    public Transform Weapon;
    public GameObject ShotPrefab;

    void Start()
    {
        _GC = FindObjectOfType(typeof(GameController)) as GameController;
        _Pcontrol = FindObjectOfType(typeof(PlayerController)) as PlayerController;

        StartCoroutine("ShotTime");
    }

    // Update is called once per frame
    void Update()
    {

        Weapon.right = _Pcontrol.transform.position - transform.localPosition; 
        // diferença de vetores para achar a posição do player

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

    private void Shot()
    {
        GameObject temp = Instantiate(ShotPrefab, Weapon.position, Weapon.localRotation);
        temp.GetComponent<Rigidbody2D>().velocity = Weapon.right * _GC.shotvelocity;

    }

    IEnumerator ShotTime()
    {
        yield return new WaitForSeconds(Random.Range(_GC.timeToShot[0], _GC.timeToShot[1]));
        Shot();
        StartCoroutine("ShotTime");
    }
}
