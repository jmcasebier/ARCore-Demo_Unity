using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject uiGun;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSpawner()
    {
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        uiGun.SetActive(true);
        SpawnController.instance.ActivateSpawner();
    }
}
