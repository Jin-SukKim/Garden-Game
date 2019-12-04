using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnSkillClick : MonoBehaviour
{

    [SerializeField]
    private float skillCooldownQ;
    [SerializeField]
    private float skillCooldownE;
    [SerializeField]
    private float skillCooldownPAtk;
    [SerializeField]
    private float skillCooldownBasic;

    private Image skillUsedOverlayQ;
    private Image skillUsedOverlayE;
    private Image skillUsedOverlayPAtk;
    private Image skillUsedOverlayBasic;
    private Button btn;

    // Start is called before the first frame update
    void Start()
    {
        skillUsedOverlayQ = GameObject.Find("SkillUsedOverlayQ").GetComponent<Image>() as Image;
        skillUsedOverlayE = GameObject.Find("SkillUsedOverlayE").GetComponent<Image>() as Image;
        skillUsedOverlayPAtk = GameObject.Find("SkillUsedOverlayPAtk").GetComponent<Image>() as Image;
        skillUsedOverlayBasic = GameObject.Find("SkillUsedOverlayBasic").GetComponent<Image>() as Image;

        skillUsedOverlayQ.fillAmount = 0;
        skillUsedOverlayE.fillAmount = 0;
        skillUsedOverlayPAtk.fillAmount = 0;
        skillUsedOverlayBasic.fillAmount = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            object[] parms = new object[2] { skillUsedOverlayQ, skillCooldownQ };
            StartCoroutine("LerpOverlay", parms);
        }
        if (Input.GetKeyDown("e"))
        {
            object[] parms = new object[2] { skillUsedOverlayE, skillCooldownE };
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