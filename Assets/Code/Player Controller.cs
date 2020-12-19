using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private RididyBody2D PlayerRb;

    public float Speed;
    

    // Start is called before the first frame update
    void Start()
    {
        PlayerRb = getcomponent<RididyBody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void Move()
    {
        
    }
}
