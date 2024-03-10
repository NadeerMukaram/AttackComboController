using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private StarterAssetsInputs StarterAssetsInputs;
    private Animator animator;
    private ThirdPersonController thirdPersonController;

    private bool bodyblow = false;

    private bool isAttacking = false; // Flag to indicate whether an attack is in progress
    private int attackCount = 0; // Counter for tracking attack phases

    // Delay before resetting the attack count after the player stops pressing the attack button
    public float resetDelay = 0.5f;
    private Coroutine resetCoroutine;

    void Start()
    {
        StarterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
        thirdPersonController = GetComponent<ThirdPersonController>();
    }

    // Update is called once per frame
    void Update()
    {

        if(StarterAssetsInputs.bodyblow && !bodyblow)
        {
            bodyblow = true;
            Debug.Log("BODY BROW!");
            StartCoroutine(BodyBlow());
        }



        // Check if the attack button is pressed
        if (StarterAssetsInputs.attack)
        {
            // Start attack sequence if not already attacking
            if (!isAttacking)
            {
                // Set the flag to indicate that an attack is in progress
                isAttacking = true;

                // Execute the attack
                StartCoroutine(PerformAttack());
            }

            // Cancel the reset coroutine if the player presses the attack button again
            if (resetCoroutine != null)
            {
                StopCoroutine(resetCoroutine);
                resetCoroutine = null;
            }
        }
        else
        {
            // Start the reset coroutine if the player stops pressing the attack button
            if (resetCoroutine == null)
            {
                resetCoroutine = StartCoroutine(ResetAttackCountAfterDelay());
            }
        }

    }

    private IEnumerator PerformAttack()
    {
        // Increment attack count
        attackCount++;
        Debug.Log("Attack " + attackCount);

        // Reset attack count to 1 if it reaches 3
        if (attackCount > 3)
        {
            attackCount = 1;
        }

        // Play the appropriate attack animation based on the attack count
        switch (attackCount)
        {
            case 1:
                animator.SetTrigger("Attack1");
                StartCoroutine(RootMotion());
                break;
            case 2:
                animator.SetTrigger("Attack2");
                StartCoroutine(RootMotion());
                break;
            case 3:
                animator.SetTrigger("Attack3");
                StartCoroutine(RootMotion());
                break;
            // Add more cases for additional attack phases if needed
            default:
                Debug.LogWarning("No animation defined for attack count: " + attackCount);
                break;
        }

        // Wait for the attack animation to finish
        yield return new WaitForSeconds(1f); // Replace 'animationDuration' with the actual duration of the attack animation

        // Reset the flag after the attack is finished
        isAttacking = false;
    }

    private IEnumerator ResetAttackCountAfterDelay()
    {
        // Wait for the specified delay before resetting the attack count
        yield return new WaitForSeconds(resetDelay);

        // Reset the attack count
        attackCount = 0;
        resetCoroutine = null;
    }

    private IEnumerator RootMotion()
    {
        animator.applyRootMotion = true;
        thirdPersonController.canMove = false;

        yield return new WaitForSeconds(1f);

        animator.applyRootMotion = false;
        thirdPersonController.canMove = true;
    }

    private IEnumerator BodyBlow()
    {
        animator.SetTrigger("BodyBlow");
        animator.applyRootMotion = true;
        thirdPersonController.canMove = false;
        yield return new WaitForSeconds (3f);
        animator.applyRootMotion = false;
        thirdPersonController.canMove = true;
        bodyblow = false;

    }
}
