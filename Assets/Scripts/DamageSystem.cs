using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageSystem : MonoBehaviour
{
    public float health;
    public Slider healthSlider;


    private float currentHealth;

    public GameObject enemeyDeathAnimation;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        healthSlider.maxValue = health;
        healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            GameObject obj = (GameObject)Instantiate(enemeyDeathAnimation, transform.position, Quaternion.identity);
            //Destroy(gameObject.GetComponent("Rigidbody"));
            Destroy(obj, 5f);
            Destroy(gameObject);
        }
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;
    }
}
