using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private PauseMenuController menuController;
    public CharacterController controller;

    public bool canMove = true;
    public bool isRunning = false;
    public float walkSpeed = 1f;
    public float runSpeed = 3f;
    public float turnSpeed = 90f;

    [Header("Resources")]
    public int self;
    public int maxHealth;
    public int currentHealth;
    public int vials;

    public enum Useables //ToDo: find out how the fuck im going to implement this LMAOOO
    {
        vialPouch, camera, pipeWrench, screwdriver, thumbDrive, unmarkedCD, masterKey, crowbar, pliers, none
    }

    [Header("Useables")]
    public Useables canUse;
    public Transform usePos;
    public bool canUseSelf;
    public bool canUseAid;
    public bool isUsing;

    public enum Equipped
    {
        rubberGloves, gasMask, ballerinaFlats, wellingtonBoots, none
    }

    [Header("Equipment")]
    public Equipped headSlot;
    public Equipped handsSlot;
    public Equipped feetSlot;
    [Space(5)]

    [Header("Equipment Utils")]
    public bool gasImmune;
    public bool causticImmune;
    public bool electricityImmune;
    public bool quiet;

    private void Start()
    {
        gasImmune = false;
        causticImmune = false;
        electricityImmune = false;
        quiet = false;

        headSlot = Equipped.none;
        handsSlot = Equipped.none;
        feetSlot = Equipped.none;

        menuController = FindObjectOfType<PauseMenuController>();
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        #region Movement

        if (canMove && !menuController.inMenu && !isUsing)
        {
            Vector3 movDir;
            float moveSpeed;

            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
            {
                moveSpeed = runSpeed;
                isRunning = true;
            }
            else
            {
                moveSpeed = walkSpeed;
                isRunning = false;
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                canMove = false;
                StartCoroutine(Quickturn());
            }

            //check if strafing before applying movement
            if (Input.GetKey(KeyCode.Z))
            {
                movDir = transform.right * -moveSpeed;
            }
            else if (Input.GetKey(KeyCode.C))
            {
                movDir = transform.right * moveSpeed;
            }
            else
            {
                //rotation and forward movement
                transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);
                movDir = transform.forward * Input.GetAxis("Vertical") * moveSpeed;
            }
            // move the character
            controller.Move(movDir * Time.deltaTime - Vector3.up * 0.1f);
        }
        #endregion

        #region Equipment Switches

        switch (headSlot)
        {
            case Equipped.gasMask:
                gasImmune = true;
                break;
            case Equipped.none:
                gasImmune = false;
                break;
        }

        switch (handsSlot)
        {
            case Equipped.rubberGloves:
                causticImmune = true;
                break;
            case Equipped.none:
                causticImmune = false;
                break;
        }

        switch (feetSlot)
        {
            case Equipped.ballerinaFlats:
                quiet = true;
                electricityImmune = false;
                break;
            case Equipped.wellingtonBoots:
                quiet = false;
                electricityImmune = true;
                break;
            case Equipped.none:
                quiet = false;
                electricityImmune = false;
                break;
        }
        #endregion

        #region Odd Useables Management

        //control if First Aid Kit is useable
        if (currentHealth < maxHealth)
            canUseAid = true;
        else if (currentHealth >= maxHealth)
            canUseAid = false;

        //control if Self is useable
        if (self > 0)
            canUseSelf = true;
        else if (self <= 0)
            canUseSelf = false;

        #endregion
    }

    public void DeductHealth(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            //ToDo: death sequence
        }
    }

    public IEnumerator Quickturn()
    {
        yield return new WaitForSeconds(1f); //wait for quickturn anim duration
        transform.Rotate(0, 180, 0);
        canMove = true;
    }

    public IEnumerator Strafe(int dir)
    {
        yield return new WaitForSeconds(1f);
        controller.Move((transform.right) * .5f * dir);
        canMove = true;
    }
}
