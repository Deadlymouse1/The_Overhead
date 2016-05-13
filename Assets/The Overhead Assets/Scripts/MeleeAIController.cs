using UnityEngine;
using System.Collections;

public class MeleeAIController : MonoBehaviour
{
    private Transform player;
    private bool isActivated;
    private bool isGoing;
    private CharacterMoveController moveController;
    private bool isAttack = false;


    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        moveController = GetComponent<CharacterMoveController>();

        StartCoroutine("checkRange");

    }
    IEnumerator checkRange()
    {
        print("123");
        for (;;)
        {
            Vector3 distance = player.position - transform.position ;

            float sqrLen = distance.sqrMagnitude;
            isActivated = sqrLen <= 200;
            isGoing = sqrLen <= 50;
            isAttack = sqrLen <= 20;
            yield return new WaitForSeconds(0.3f);

        }
    }

    // Update is called once per frame
    void Update()
    {

        if (isActivated)
        {
            moveController.Move(-1, false);
        }
            if (isGoing)
            {
                moveController.Move((player.position.x - transform.position.x) / Mathf.Abs(player.position.x - transform.position.x), false);
            }
            if (isAttack)
            {
                moveController.Attack(isAttack);
            }


        
    }
}
