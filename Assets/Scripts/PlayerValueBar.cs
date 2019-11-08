using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * This script tracks the value of a target gameobject with the DamageSystem script attached to it
 * when value drops, values will lerp to new value with a secondary colored sprite (fillImageLerp)
 */

public class PlayerValueBar : MonoBehaviour {
    [SerializeField]
    //game object whose damage is being tracked
    public GameObject targetObject; 
    public DamageSystem targetDamageSystem;
    [SerializeField]
    public float lerpSpeed;
    [SerializeField]   
    public Image fillImage;
    [SerializeField]
    public Image fillImageLerp;
    [SerializeField]
    //maximum value value of valuebar
    public float maxValue;
    //fill percantage of valuebar
    private float currentFillPercentage;
    //property for the value bar's currentValue;
    private float _currentValue;

    //currentValue getter and setter
    public float CurrentValue
    {
        get
        {
            return _currentValue;
        }

        set
        {
            //validate values are within range
            if (value > maxValue)
            {
                _currentValue = maxValue;
            } else if (value < 0) {
                _currentValue = 0;
            } else {
                _currentValue = value;
            }

            //get current fill percentage as a number from 0 to 1
            currentFillPercentage = _currentValue / maxValue;
        }
    }
    


    void Start()
    {
        //init 
        targetDamageSystem = targetObject.GetComponent<DamageSystem>();
        maxValue = targetDamageSystem.health;
        //start value at full
        CurrentValue = maxValue;
        currentFillPercentage = 1;
    }

    void Update()
    {
        //instant value indicator 
        //instant value indicator 
        if (currentFillPercentage != fillImage.fillAmount)
        {
            fillImage.fillAmount = currentFillPercentage;
        }

        //lerp value indicator
        if (currentFillPercentage != fillImageLerp.fillAmount) {
            fillImageLerp.fillAmount = Mathf.Lerp(fillImageLerp.fillAmount, currentFillPercentage, Time.deltaTime * lerpSpeed);
        }

        if(targetDamageSystem.currentHealth != CurrentValue)
        {
            CurrentValue = targetDamageSystem.currentHealth;
        }

        //manual controls for testing
        /*
        if(Input.GetKeyDown("j"))
        {
            CurrentValue -= 10;
        }
        if (Input.GetKeyDown("k"))
        {
            CurrentValue += 10;
        }

        if (Input.GetKeyDown("u"))
        {
            CurrentValue -= 25;
        }
        if (Input.GetKeyDown("i"))
        {
            CurrentValue += 25;
        }
        */
    }
}