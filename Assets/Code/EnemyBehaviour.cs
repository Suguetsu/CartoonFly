using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    private GameController _GC;

    public Transform[] wayPoints;
    public int idWayPoints;
    public float enemyVel;
    GameObject temp;

    private bool isCanMove;




    // Start is called before the first frame update
    void Start()
    {

        _GC = FindObjectOfType(typeof(GameController)) as GameController;

        instanciaEnemy();

    }

    // Update is called once per frame
    void Update()
    {
        BehaviourEnemy();
    }

    void BehaviourEnemy()
    {
        if (isCanMove && temp.gameObject != null)
        {

            temp.transform.position = Vector3.MoveTowards(temp.transform.position, wayPoints[idWayPoints].position, enemyVel * Time.deltaTime);
            Debug.Log("MOVENDO");

            if (temp.transform.position == wayPoints[idWayPoints].position)
            {
                isCanMove = false;
                StartCoroutine(EnemysBehaviour());
            }
        }
    }

    void instanciaEnemy()
    {
        int localId = Random.Range(0, wayPoints.Length);

        Debug.Log(localId);
        temp = Instantiate(_GC.enemyPrefab[0]);
        temp.transform.position = wayPoints[localId].transform.position;
        StartCoroutine(EnemysBehaviour());

    }

    IEnumerator EnemysBehaviour()
    {
        idWayPoints++;
        if (idWayPoints >= wayPoints.Length)
        {
            idWayPoints = 0;
        }
        yield return new WaitForSeconds(_GC.timeToMove);
        isCanMove = true;




    }
}
