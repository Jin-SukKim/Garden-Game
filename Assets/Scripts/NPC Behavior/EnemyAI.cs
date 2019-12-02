using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    private GameObject player;
    private Animator animator;

    public GameObject GetPlayer() {
        return player;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Updating the distance value in the EnemyAnimator state machine.
//         if(player != null) 
//             animator.SetFloat("distance", Vector3.Distance(transform.position, player.transform.position));
        //animator.SetFloat("distance", Vector3.Distance(transform.position, player.transform.position));
    }
}
