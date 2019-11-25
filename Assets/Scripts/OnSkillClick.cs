using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnSkillClick : MonoBehaviour
{

    [SerializeField]
    private float skillCooldownE;
    [SerializeField]
    private float skillCooldownR;
    [SerializeField]
    private float skillCooldownPAtk;
    [SerializeField]
    private float skillCooldownBasic;

    private Image skillUsedOverlayE;
    private Image skillUsedOverlayR;
    private Image skillUsedOverlayPAtk;
    private Image skillUsedOverlayBasic;
    private Button btn;

    // Start is called before the first frame update
    void Start()
    {
        skillUsedOverlayE = GameObject.Find("SkillUsedOverlayE").GetComponent<Image>() as Image;
        skillUsedOverlayR = GameObject.Find("SkillUsedOverlayR").GetComponent<Image>() as Image;
        skillUsedOverlayPAtk = GameObject.Find("SkillUsedOverlayPAtk").GetComponent<Image>() as Image;
        skillUsedOverlayBasic = GameObject.Find("SkillUsedOverlayBasic").GetComponent<Image>() as Image;

        skillUsedOverlayE.fillAmount = 0;
        skillUsedOverlayR.fillAmount = 0;
        skillUsedOverlayPAtk.fillAmount = 0;
        skillUsedOverlayBasic.fillAmount = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            object[] parms = new object[2] { skillUsedOverlayE, skillCooldownE };
            StartCoroutine("LerpOverlay", parms);
        }
        if (Input.GetKeyDown("r"))
        {
            object[] parms = new object[2] { skillUsedOverlayR, skillCooldownR };
            StartCoroutine("LerpOverlay", parms);
        }
        if (Input.GetMouseButtonDown(0))
        {
            object[] parms = new object[2] { skillUsedOverlayPAtk, skillCooldownPAtk };
            StartCoroutine("LerpOverlay", parms);
        }
        if (Input.GetMouseButtonDown(1))
        {
            object[] parms = new object[2] { skillUsedOverlayBasic, skillCooldownBasic };
            StartCoroutine("LerpOverlay", parms);
        }

    }

    public IEnumerator LerpOverlay(object[] parms)
    {
        Image img = (Image)parms[0];
        float skillCooldown = (float)parms[1];
        float currentTime;
        float decrementAmount = 0.01f;
        for (currentTime = skillCooldown; currentTime > 0; currentTime -= decrementAmount)
        {
            img.fillAmount = currentTime / skillCooldown;
            //Debug.Log("Time: " + currentTime);
            yield return new WaitForSeconds(.01f);
        }
        img.fillAmount = 0;
        yield return null;
    }
}