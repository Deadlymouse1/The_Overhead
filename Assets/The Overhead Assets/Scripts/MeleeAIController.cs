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
            float distance = Vector2.Distance(transform.position, player.position);
            isActivated = distance <= 25;
            isGoing = distance <= 15;
            isAttack = distance <= 4;
            yield return new WaitForSeconds(0.3f);

        }
    }

    // Update is called once per frame
    void Update()
    {

        if (isActivated)
        {
            moveController.Move(-1, false);
            if (isGoing)
            {
                moveController.Move((player.position.x - transform.position.x) / Mathf.Abs(player.position.x - transform.position.x), false);
            }
            if (isAttack)
            {
                moveController.Attack(isAttack);
                print("I AM ATTACK");
            }


        }
    }
}
