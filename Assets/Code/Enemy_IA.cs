using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_IA : MonoBehaviour
{
    public GameObject enemysAirGO; // Object
    private GameController _GC;
    

    public string placeId;       // place to start

    public float pointsToTurn;   // float positions to turn the object
    public float enemyVel;       // velocity
    public float degreeToTurn;   // degrees


    public GameObject   arma;

    private bool isCurve;


    public TagShot tagBullet;



    // Start is called before the first frame update
    void Start()
    {
        _GC = FindObjectOfType(typeof(GameController)) as GameController;



    }

    // Update is called once per frame
    void Update()
    {
        IaMechanics();
    }

    private void OnBecameVisible()
    {
        StartCoroutine(WhenToShot());
        print("Shoting");
    }
    IEnumerator WhenToShot()
    {
        yield return new WaitForSeconds(_GC.timeToShot[0]);
        Shooting();
        StartCoroutine(WhenToShot());
    }
    void Shooting()
    {

        GameObject temp = Instantiate(_GC.shotPrefab[_GC.idShotEnemy]);
        temp.transform.gameObject.tag = _GC.AplicarTag(tagBullet);
        temp.transform.position = arma.transform.position;
        temp.GetComponent<Rigidbody2D>().velocity = transform.up * -1 * _GC.shotvelocity;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "playerShot":
                GameObject temp = Instantiate(_GC.explosion, transform.position, transform.localRotation);
                Destroy(this.gameObject);
                Destroy(collision.gameObject);
                _GC.SpawnLoot(this.transform);
                break;
       
        }
    }

    void IaMechanics()
    {
        float enemyPosX = enemysAirGO.transform.position.x;
        float enemyPosY = enemysAirGO.transform.position.y;





        switch (placeId)
        {
            case ("Top"):



                float direction = enemyVel * -Time.deltaTime; // enemy goes to down

                enemysAirGO.transform.Translate(new Vector3(0, direction, 0));

                if (enemyPosY <= pointsToTurn && !isCurve)
                {
                    isCurve = true;
                    enemysAirGO.transform.rotation = Quaternion.Euler(0, 0, 0);

                }

                if (isCurve) enemysAirGO.transform.rotation = Quaternion.Euler(0, 0, degreeToTurn);

                break;
            case ("Mid"):

                float directionMid = enemyVel * Time.deltaTime;

                if (!isCurve && enemyPosX < pointsToTurn)
                {

                    enemysAirGO.transform.Translate(new Vector3(directionMid, 0, 0), 0);


                    enemysAirGO.transform.rotation = Quaternion.Euler(0, 0, 90);
                }
                else
                {
                    isCurve = true;
                    enemysAirGO.transform.Translate(new Vector3(0, directionMid, 0), 0);
                    enemysAirGO.transform.rotation = Quaternion.Euler(0, 0, degreeToTurn);
                }


                break;
            case ("Bottom"):

                float directionBottom = enemyVel * Time.deltaTime;

                if (!isCurve && enemyPosY < pointsToTurn)
                {

                    enemysAirGO.transform.rotation = Quaternion.Euler(0, 0, 230);
                    enemysAirGO.transform.Translate(new Vector3(-directionBottom, directionBottom, 0), 0);

                }
                else
                {
                    directionBottom *= -1;
                    isCurve = true;
                    enemysAirGO.transform.rotation = Quaternion.Euler(0, 0, -30);
                    enemysAirGO.transform.Translate(new Vector3(directionBottom, directionBottom, 0), 0);
                }




                break;



        }
    }
}
