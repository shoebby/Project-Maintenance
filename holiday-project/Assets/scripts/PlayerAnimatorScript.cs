using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorScript : MonoBehaviour
{
    private Animator anim;
    private bool isWalking;
    private bool isRunning;
    private bool isWalkingBack;
    private bool isQuickTurn;
    private bool isStrafingLeft;
    private bool isStrafingRight;
    private bool isTurning;

    private bool forwardPressed;
    private bool runPressed;
    private bool backwardPressed;
    private bool quickturnPressed;
    private bool strafeLeftPressed;
    private bool strafeRightPressed;
    private float turnFloat;

    private int isWalkingHash;
    private int isRunningHash;
    private int isWalkingBackHash;
    private int isQuickTurnHash;
    private int isStrafingLeftHash;
    private int isStrafingRightHash;
    private int isTurningHash;

    void Start()
    {
        anim = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isWalkingBackHash = Animator.StringToHash("isWalkingBack");
        isQuickTurnHash = Animator.StringToHash("isQuickTurn");
        isStrafingLeftHash = Animator.StringToHash("isStrafingLeft");
        isStrafingRightHash = Animator.StringToHash("isStrafingRight");
        isTurningHash = Animator.StringToHash("isTurning");
    }

    void Update()
    {
        forwardPressed = Input.GetKey(KeyCode.W);
        runPressed = Input.GetKey(KeyCode.LeftShift);
        backwardPressed = Input.GetKey(KeyCode.S);
        quickturnPressed = Input.GetKey(KeyCode.Q);
        strafeLeftPressed = Input.GetKey(KeyCode.Z);
        strafeRightPressed = Input.GetKey(KeyCode.C);
        turnFloat = Input.GetAxis("Horizontal");

        isWalking = anim.GetBool("isWalking");
        isRunning = anim.GetBool("isRunning");
        isWalkingBack = anim.GetBool("isWalkingBack");
        isQuickTurn = anim.GetBool("isQuickTurn");
        isStrafingLeft = anim.GetBool("isStrafingLeft");
        isStrafingRight = anim.GetBool("isStrafingRight");
        isTurning = anim.GetBool("isTurning");

        //walking
        if (!isWalking && forwardPressed)
            anim.SetBool(isWalkingHash, true);

        if (isWalking && !forwardPressed)
            anim.SetBool(isWalkingHash, false);

        //walking backwards
        if (!isWalkingBack && backwardPressed)
            anim.SetBool(isWalkingBackHash, true);

        if (isWalkingBack && !backwardPressed)
            anim.SetBool(isWalkingBackHash, false);

        //running
        if (!isRunning && (forwardPressed && runPressed))
            anim.SetBool(isRunningHash, true);

        if (isRunning && (!forwardPressed || !runPressed))
            anim.SetBool(isRunningHash, false);

        //turning
        if (!forwardPressed && (turnFloat < 0 || turnFloat > 0))
            anim.SetBool(isTurningHash, true);
        else if (forwardPressed && (turnFloat < 0 || turnFloat > 0))
            anim.SetBool(isTurningHash, false);

        if (isTurning && turnFloat == 0)
            anim.SetBool(isTurningHash, false);

        //quickturn
        if (!isQuickTurn && quickturnPressed)
            anim.SetBool(isQuickTurnHash, true);

        if (isQuickTurn && !quickturnPressed)
            anim.SetBool(isQuickTurnHash, false);

        //strafe
        if (!isStrafingLeft && strafeLeftPressed)
            anim.SetBool(isStrafingLeftHash, true);

        if (isStrafingLeft && !strafeLeftPressed)
            anim.SetBool(isStrafingLeftHash, false);

        if (!isStrafingRight && strafeRightPressed)
            anim.SetBool(isStrafingRightHash, true);

        if (isStrafingRight && !strafeRightPressed)
            anim.SetBool(isStrafingRightHash, false);
    }
}
