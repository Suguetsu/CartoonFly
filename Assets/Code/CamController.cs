using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameController _GC;



    void Start()
    {
        _GC = FindObjectOfType(typeof(GameController)) as GameController;
    }

    // Update is called once per frame
    void Update()
    {
        if (_GC.isAlivePlayer && _GC.playerTransform.transform.position != null)
            CamOffset();
    }

    private void CamOffset()
    {

        float posPlayerX = _GC.playerTransform.transform.position.x;
        float posPlayerY = _GC.playerTransform.transform.position.y;


        if (_GC.playerTransform.transform.position.x >= _GC.limiDir.position.x)
        {
            _GC.playerTransform.transform.position = new Vector2(_GC.limiDir.position.x, _GC.playerTransform.transform.position.y);
        }
        else if (_GC.playerTransform.transform.position.x <= _GC.limiEsq.position.x)
        {
            _GC.playerTransform.transform.position = new Vector2(_GC.limiEsq.position.x, _GC.playerTransform.transform.position.y);

        }

        if (_GC.playerTransform.transform.position.y >= _GC.limSup.position.y)
        {
            _GC.playerTransform.transform.position = new Vector2(_GC.playerTransform.transform.position.x, _GC.limSup.position.y);
        }
        else if (_GC.playerTransform.transform.position.y <= _GC.limInf.position.y)
        {
            _GC.playerTransform.transform.position = new Vector2(_GC.playerTransform.transform.position.x, _GC.limInf.position.y);

        }
    }

    private void LateUpdate()
    {
        if (_GC.isAlivePlayer && _GC.playerTransform.transform.position != null)
        {
            if (transform.position.x > _GC.limiCamEsq.position.x && transform.position.x < _GC.limiCamDir.position.x)
            {
                CamLimited();
            }
            else if (transform.position.x <= _GC.limiCamEsq.position.x && _GC.playerTransform.transform.position.x > _GC.limiCamEsq.position.x)
            {
                CamLimited();
            }
            else if (transform.position.x >= _GC.limiCamDir.position.x && _GC.playerTransform.transform.position.x < _GC.limiCamDir.position.x)
            {
                CamLimited();
            }

        }
    }
    void CamLimited()
    {
        Vector3 pos = new Vector3(_GC.playerTransform.transform.position.x, transform.position.y, -10);
        transform.position = Vector3.Lerp(transform.position, pos, _GC.velCam * Time.deltaTime);

    }
}
