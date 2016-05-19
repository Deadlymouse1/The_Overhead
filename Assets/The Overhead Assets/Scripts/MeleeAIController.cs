using UnityEngine;
using System.Collections;

public class MeleeAIController : MonoBehaviour
{
    private Transform player;
    private LayerMask ground;

    private bool rHavePlatform;
    private bool lHavePlatform;

    private Transform rPlatformCheck;
    private Transform lPlatformCheck;

    private bool isActivated;
    private bool isGoing;
    private CharacterMoveController moveController;
    private bool isAttack = false;


    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        moveController = GetComponent<CharacterMoveController>();
        lPlatformCheck = transform.FindChild("LPlatformCheck");
        rPlatformCheck = transform.FindChild("RPlatformCheck");
        StartCoroutine("checkRange");


    }
    IEnumerator checkRange()
    {
        print("123");
        for (;;)
        {
            Vector3 distance = player.position - transform.position;

            float sqrLen = distance.sqrMagnitude;
            isActivated = sqrLen <= 100;
            isGoing = sqrLen <= 50;
            isAttack = sqrLen <= 20;
            yield return new WaitForSeconds(0.3f);

        }
    }
    void Patrol()
    {
        if (!rHavePlatform)
        {
            moveController.Move(-1, false);
            print("rPlatform");
        }
        if (!lHavePlatform)
        {
            moveController.Move(1, false);
            print("lPlatform");
        }
        
    }
    void GoingTo()
    {
        moveController.Move((player.position.x - transform.position.x) / Mathf.Abs(player.position.x - transform.position.x), false);
    }

    // Update is called once per frame
    void Update()
    {

        /*if (isActivated && !isGoing && !isAttack)
        {
                Patrol();
                moveController.Move(Random.Range(-1, 1), false);
        }*/
        if (isGoing && isActivated && !isAttack)
        {
            GoingTo();
        }
        if (isAttack)
        {
            moveController.Attack(isAttack);
        }


        
    }
    void FixedUpdate()
    {
        float checkRadius = 0.2f;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(lPlatformCheck.position, checkRadius, ground);
        rHavePlatform = colliders.Length > 0;
        colliders = Physics2D.OverlapCircleAll(rPlatformCheck.position, checkRadius, ground);
        lHavePlatform = colliders.Length > 0;
        
    }
}

