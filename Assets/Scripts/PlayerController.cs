using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    
    public Camera viewCam;
    public GameObject bulletImpact;
    public GameObject deadScreen;
    public GameObject deadComponents;
    public GameObject healthEighty;
    public GameObject healthSixty;
    public GameObject healthFourty;
    public GameObject healthTwenty;
    public Animator gunAnim;
    public int currentAmmo;
    public int maxHealth = 100;
    
    private GameObject[] healthLevels;
    public int currentHealth;
    private bool isAlive;

    private void Awake()
    {
        instance = this;
        isAlive = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthLevels = new GameObject[] { healthEighty, healthSixty, healthFourty, healthTwenty };
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            // Player damage UI controls
            for (int i = 0; i < healthLevels.Length; i++)
            {
                healthLevels[i].SetActive(false);
            }
            if (currentHealth <= 80 && currentHealth > 60)
            {
                healthLevels[0].SetActive(true);
            }
            else if (currentHealth <= 60 && currentHealth > 40)
            {
                healthLevels[1].SetActive(true);
            }
            else if (currentHealth <= 40 && currentHealth > 20)
            {
                healthLevels[2].SetActive(true);
            }
            else if (currentHealth <= 20)
            {
                healthLevels[3].SetActive(true);
            }

            // Shooting controls
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (currentAmmo > 0)
                {
                    Ray ray = viewCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        Instantiate(bulletImpact, hit.point, transform.rotation);
                        if (hit.transform.tag == "Enemy")
                        {
                            hit.transform.parent.GetComponent<EnemyController>().TakeDamage();
                        }
                    }
                    currentAmmo--;
                    gunAnim.SetTrigger("Shoot");
                }
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            deadScreen.SetActive(true);
            deadComponents.SetActive(true);
            isAlive = false;
        }
    }

    public void AddHealth(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
