using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Config")]
    private Rigidbody2D PlayerRb;
    public float speed;


    [Header("Shot Config")]
    public float shotSpeed;
    public float timeToshot;
  

    public GameObject spawnShot;
    public GameObject shotPrefa;
    private bool isShot;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRb = GetComponent<Rigidbody2D>();
        

    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (Input.GetButton("Fire1") && !isShot)
        {
            PlayerShot();
            isShot = true;
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
        GameObject temp = Instantiate(shotPrefa, spawnShot.gameObject.transform);
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, shotSpeed);
        StartCoroutine(ShotTime(timeToshot));
    }
}
