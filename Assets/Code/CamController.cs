using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameController _GC;

    [SerializeField]
    public Transform playerPos;


    void Start()
    {
        _GC = FindObjectOfType(typeof(GameController)) as GameController;

    }

    // Update is called once per frame
    void Update()
    {
        CamOffset();
    }

    private void CamOffset()
    {

        float posPlayerX = playerPos.position.x;
        float posPlayerY = playerPos.position.y;


        if (playerPos.position.x >= _GC.limiDir.position.x)
        {
            playerPos.position = new Vector2(_GC.limiDir.position.x, playerPos.position.y);
        }
        else if (playerPos.position.x <= _GC.limiEsq.position.x)
        {
            playerPos.position = new Vector2(_GC.limiEsq.position.x, playerPos.position.y);

        }

        if (playerPos.position.y >= _GC.limSup.position.y)
        {
            playerPos.position = new Vector2(playerPos.position.x, _GC.limSup.position.y);
        }
        else if (playerPos.position.y <= _GC.limInf.position.y)
        {
            playerPos.position = new Vector2(playerPos.position.x, _GC.limInf.position.y);

        }
    }

    private void LateUpdate()
    {
        if (transform.position.x > _GC.limiCamEsq.position.x && transform.position.x < _GC.limiCamDir.position.x)
        {
            CamLimited();
        }
        else if (transform.position.x <= _GC.limiCamEsq.position.x && playerPos.position.x > _GC.limiCamEsq.position.x)
        {
            CamLimited();
        }
        else if (transform.position.x >= _GC.limiCamDir.position.x && playerPos.position.x < _GC.limiCamDir.position.x)
        {
            CamLimited();
        }
    }
    void CamLimited()
    {
        Vector3 pos = new Vector3(playerPos.position.x, transform.position.y, -10);
        transform.position = Vector3.Lerp(transform.position, pos, _GC.velCam * Time.deltaTime);

    }
}
