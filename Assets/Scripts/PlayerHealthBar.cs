using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Image fillImage;
    [SerializeField]
    public float maximumHealth;
    public float currentHealth;
    public float currentFill;

    // Start is called before the first frame update
    void Start()
    {
        //fillImage = GetComponent<Image>();
        fillImage.fillAmount = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
