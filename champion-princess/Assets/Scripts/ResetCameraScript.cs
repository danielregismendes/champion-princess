using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCameraScript : MonoBehaviour
{
    public float cameraX;

    [System.Obsolete]
    public void Activate()
    {
        cameraX = FindObjectOfType<EnemySpawn>().GetMaxX();
        GetComponent<Animator>().SetTrigger("Go");
    }

    [System.Obsolete]
    void ResetCamera()
    {
        //cameraX = FindObjectOfType<EnemySpawn>().GetMaxX();
        FindObjectOfType<CameraFollow>().maxXAndY.x = cameraX;
    }

}
