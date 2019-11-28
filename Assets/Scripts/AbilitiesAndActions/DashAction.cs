using UnityEngine;
using System.Collections;

public class DashAction : IAction
{
    public float distance = 10f;
    public float speed = 5f;
    public void doAction(Entity e, Ability a)
    {
        e.StartCoroutine(Dash(e,a));
    }

    IEnumerator Dash(Entity e, Ability a)
    {
        Vector3 start = e.transform.position;
        Vector3 end = e.transform.position + (10 * e.transform.forward);

        float total = Vector3.Distance(start, end);

        float startTime = Time.time;

        Vector3 lastLoc = end;
        e.IsDisabled = true;
        while (Vector3.Distance(e.transform.position, end) >= 0.1)
        {
            if (Time.time - startTime > 1f && Mathf.Abs(Vector3.Distance(lastLoc, e.transform.position)) <= 0.05)
            {
                break;
            }
            float curDistance = (Time.time - startTime) * speed;

            float fractionOfDistance = curDistance / total;

            lastLoc = e.transform.position;
            e.transform.position = Vector3.Lerp(start, end, fractionOfDistance);

            yield return new WaitForEndOfFrame();
        }
        Debug.Log("Dash ended");
        e.IsDisabled = false;

    }
}
