using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TagShot
{
    playerShot, enemyShot
}

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] powerUps;

    [Header("Config enemy")]

    public float timeToMove;
    public int shotvelocity;
    public int[] timeToShot;
    public int idShotEnemy;
    public GameObject[] enemyPrefab;

    [Header("Explosion Prefab")]
    public GameObject explosion;

    [Header("Shoot prefab")]
    public GameObject[] shotPrefab;

    [Header("Player config")]
    public Transform limSup;
    public Transform limInf;
    public Transform limiEsq;
    public Transform limiDir;
    public Transform spawnPlayer;

    public int extraLife;
    public int idShotPlayer;
    [SerializeField]
    public PlayerController _PlayerC;
    public bool isAlivePlayer;
    public bool isInvunerable;
    public float timeInvulnerable;

    public GameObject playerPrefab;
    public Transform playerTransform;

    [Header("Cam config")]
    public Transform limiCamEsq;
    public Transform limiCamDir;

    public float velCam;

    void Start()
    {

        //if (isAlivePlayer)
        //_PlayerC = FindObjectOfType(typeof(PlayerController)) as PlayerController;



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


            if (percent > 85)
            {
                index = 2;

                // bomb
            }
            else if (percent > 65)
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

    public string AplicarTag(TagShot tag)
    {

        string retorno = null;

        switch (tag)
        {
            case TagShot.playerShot:
                retorno = "playerShot";
                break;
            case TagShot.enemyShot:
                retorno = "enemyShot";
                break;


        }
        return retorno;
    }


    public IEnumerator TimetoRespawn()
    {
        isAlivePlayer = false;


        yield return new WaitForSeconds(1);
        PlayerDeath();
    }



    public void PlayerDeath()
    {
        extraLife -= 1;

        if (extraLife >= 0)
        {
            GameObject temp = Instantiate(playerPrefab, spawnPlayer.position, spawnPlayer.localRotation);
            playerTransform = temp.transform;
            temp.GetComponent<Collider2D>().enabled = false;

            StartCoroutine(Invunerable(temp));

        }
        else
        {


            print("Game Over");
        }

    }

    public IEnumerator Invunerable(GameObject tempPlayer)
    {

        float a = Mathf.PingPong(0.5f, 1);
        Debug.Log("color: " + a);
        Color b = new Color(200, 200, 200, a);
        tempPlayer.GetComponent<SpriteRenderer>().color = b;

        isInvunerable = true;
        StartCoroutine(Piscado(tempPlayer));


        yield return new WaitForSeconds(timeInvulnerable);

        isInvunerable = false;
        b = new Color(255, 255, 255, 1);
        tempPlayer.GetComponent<SpriteRenderer>().color = b;
        tempPlayer.GetComponent<Collider2D>().enabled = true;


    }

    public IEnumerator Piscado(GameObject tempPlayer2)
    {
        if (isInvunerable)
        {
            Debug.Log("executando");
            yield return new WaitForSeconds(0.25f);
            if (tempPlayer2 != null)
            {
                tempPlayer2.GetComponent<SpriteRenderer>().enabled = !tempPlayer2.GetComponent<SpriteRenderer>().enabled;

                StartCoroutine(Piscado(tempPlayer2));
            }
        }
        else
        {
            yield return new WaitForEndOfFrame();
            StopCoroutine(Piscado(tempPlayer2));
            tempPlayer2.GetComponent<SpriteRenderer>().enabled = true;
            Debug.Log("parou??");
        }


    }
}
