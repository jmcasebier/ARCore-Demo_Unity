using System.Collections.Generic;
using GoogleARCore;
using GoogleARCore.Examples.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnController : MonoBehaviour
{
    public static SpawnController instance;

    public GameObject enemyObject;
    public Camera viewCam;

    private const float k_ModelRotation = 180.0f;
    private float groundHeight;
    private Pose groundPose;
    private bool shouldSpawn = false;
    private int spawnCounter = 180;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldSpawn)
        {
            spawnCounter--;
            if (spawnCounter <= 0)
            {
                // Place enemy
                Instantiate(enemyObject, new Vector3(0, groundHeight, 10), groundPose.rotation);

                // Compensate for the hitPose rotation facing away from the raycast (i.e.
                // camera).
                enemyObject.transform.Rotate(0, k_ModelRotation, 0, Space.Self);
                spawnCounter = 180;
            }
        }
    }

    public void SetGroundPose(Pose p)
    {
        groundPose = p;
        groundHeight = viewCam.transform.position.y - groundPose.position.y;
    }

    public void ActivateSpawner()
    {
        shouldSpawn = true;
    }
}
