using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameController _GC;

    [Header("Player Config")]
    private Rigidbody2D PlayerRb;
    public float speed;

    [Header("Shot Config")]
    public float shotSpeed;
    public float timeToshot;



    public GameObject spawnShot;

    private bool isShot;

    public TagShot tagBullest;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRb = GetComponent<Rigidbody2D>();

        _GC = FindObjectOfType(typeof(GameController)) as GameController;
        _GC._PlayerC = this; // recebe o script do player para q ele posso ser reiniciado

        _GC.isAlivePlayer = true;



    }

    // Update is called once per frame
    void Update()
    {

        if (_GC.isAlivePlayer)
        {
            Move();

            if (Input.GetButton("Fire1") && !isShot)
            {
                PlayerShot();
                isShot = true;
            }
        }
    }


    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        PlayerRb.velocity = new Vector2(horizontal * speed, vertical * speed);
    }


    IEnumerator ShotTime(float time)
    {
        yield return new WaitForSeconds(time);

        isShot = false;
    }
    void PlayerShot()
    {
        GameObject temp = Instantiate(_GC.shotPrefab[_GC.idShotPlayer], spawnShot.gameObject.transform);

        temp.transform.gameObject.tag = _GC.AplicarTag(tagBullest);

        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, shotSpeed);
        StartCoroutine(ShotTime(timeToshot));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "enemyShot":
                GameObject temp = Instantiate(_GC.explosion, transform.position, transform.localRotation);

                Destroy(temp.gameObject, 0.5f);
                Destroy(this.gameObject);
                Destroy(collision.gameObject);
                _GC.StartCoroutine(_GC.TimetoRespawn());
                break;

        }
    }



}
