using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealthBar : MonoBehaviour {
    [SerializeField]
    public float lerpSpeed = 1;
    private Image fillImage;
    private float currentFill;
    [SerializeField]
    public float maxHealth { get; set; }

    //property for the health bar's currentValue;
    public float CurrentValue
    {
        get
        {
            return currentValue;
        }

        set
        {
            //validate values are within range
            if (value > CurrentValue)
            {
                currentValue = maxHealth;
            } else if (value < 0) {
                currentValue = 0;
            } else {
                currentValue = value;
            }

            //get current fill as a number from 0 to 1
            currentFill = currentFill / maxHealth;
        }
    }
    private float currentValue;


    void Start()
    {

    }

    void Update()
    {
        //lerp
        if (currentFill != fillImage.fillAmount) {
            fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
        }

        //manual controls for testing
        if(Input.GetKeyDown("J"))
        {
            CurrentValue -= 10;
        }
        if (Input.GetKeyDown("K"))
        {
            CurrentValue += 10;
        }
    }
}