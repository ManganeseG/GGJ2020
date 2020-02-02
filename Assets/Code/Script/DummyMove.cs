using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyMove : MonoBehaviour
{
    CharacterController characterController;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;

    [Header("Useful Variables")]
    [Range(0, 3)] public int Player;
    public enum CharaName { Cat, Goat, Axolotl, Owl }
    public CharaName CharacterName;
    public GameObject CatMesh;
    public GameObject OwlMesh;
    public GameObject GoatMesh;
    public GameObject AxolotlMesh;
    private GameObject meshSelected;

    public Transform grabPos;
    public bool isGrab;
    private GameObject grabbedItem;
    private float holdCD;
    public float HoldCD = 1f;

    private float stunCD;
    public float StunCD = 1f;
    private float timeBeforeNextStun;
    public float TimeBeforeNextStun = 5f;
    public GameObject StunZone;
    private float stunnedStateCD;
    public float StunnedStateCD = 3f;
    private bool canStun = false;
    private bool hasBeenStunned = false;

    private Animator animator;
    private Quaternion targetRotation;
    public int ControllerIndex;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        //Needed Var
        isGrab = false;
        holdCD = HoldCD;
        stunCD = StunCD;
        timeBeforeNextStun = TimeBeforeNextStun;
        stunnedStateCD = StunnedStateCD;
        StunZone.SetActive(false);

        switch (CharacterName)
        {
            case CharaName.Cat:
                meshSelected = Instantiate(CatMesh, gameObject.transform.position, gameObject.transform.rotation);
                meshSelected.transform.parent = gameObject.transform;
                break;
            case CharaName.Goat:
                meshSelected = Instantiate(GoatMesh, gameObject.transform.position, gameObject.transform.rotation);
                meshSelected.transform.parent = gameObject.transform;
                break;
            case CharaName.Axolotl:
                meshSelected = Instantiate(AxolotlMesh, gameObject.transform.position, gameObject.transform.rotation);
                meshSelected.transform.parent = gameObject.transform;
                break;
            case CharaName.Owl:
                meshSelected = Instantiate(OwlMesh, gameObject.transform.position, gameObject.transform.rotation);
                meshSelected.transform.parent = gameObject.transform;
                break;
            default:
                break;
        }

        animator = meshSelected.GetComponent<Animator>();
    }

    void Update()
    {
        #region Move
        if (hasBeenStunned == false)
        {
            if (characterController.isGrounded)
            {

                moveDirection = new Vector3(-Input.GetAxis("Horizontal_" + ControllerIndex), 0.0f, -Input.GetAxis("Vertical_" + ControllerIndex));
                animator.SetFloat("Speed", moveDirection.magnitude);
                animator.SetBool("HasObject", isGrab);

                if (moveDirection.magnitude > 0)
                {
                    targetRotation = Quaternion.LookRotation(moveDirection.normalized);
                }
                meshSelected.transform.rotation = targetRotation;
                moveDirection *= speed;
            }
            moveDirection.y -= gravity * Time.deltaTime;

            characterController.Move(moveDirection * Time.deltaTime);
            #endregion

            if (isGrab)
            {
                holdCD -= Time.deltaTime;
                grabbedItem.transform.position = grabPos.transform.position;

                if ((Input.GetButtonDown("Action_" + ControllerIndex) && holdCD <= 0f) || hasBeenStunned == true)
                {
                    grabbedItem = null;
                    isGrab = false;
                    holdCD = HoldCD;
                }
            }
        }
        else
        {
            characterController.Move(Vector3.zero);
            stunnedStateCD -= Time.deltaTime;
            if (stunnedStateCD <= 0)
            {
                hasBeenStunned = false;
                stunnedStateCD = StunnedStateCD;
            }
        }
        if ((Input.GetKeyDown(KeyCode.G) || Input.GetButtonDown("Push_" + ControllerIndex)) && canStun == true && isGrab == false)
        {
            animator.SetTrigger("Push");
            StunZone.SetActive(true);
            canStun = false;
        }
        if (!canStun)
        {
            timeBeforeNextStun -= Time.deltaTime;
            stunCD -= Time.deltaTime;
            if (stunCD <= 0f)
            {
                StunZone.SetActive(false);

            }
            if (timeBeforeNextStun <= 0f)
            {
                canStun = true;
                timeBeforeNextStun = TimeBeforeNextStun;
                stunCD = StunCD;
            }

        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Item") && Input.GetButtonDown("Action_" + ControllerIndex) && isGrab == false)
        {
            isGrab = true;
            grabbedItem = col.gameObject;
        }
        if (col.gameObject.layer == LayerMask.NameToLayer("Totem") && isGrab == true)
        {
            Totem totemSc;
            totemSc = col.gameObject.GetComponent<Totem>();
            if (totemSc.playerTotem == Player)
            {
                totemSc.constructionSlider.value += 0.25f;
                Destroy(grabbedItem);
                grabbedItem = null;
                isGrab = false;
                holdCD = HoldCD;
            }
        }
        if (col.gameObject.layer == LayerMask.NameToLayer("StunZone"))
        {
            DummyMove ennemyMove;
            ennemyMove = col.gameObject.GetComponentInParent<DummyMove>();
            ennemyMove.hasBeenStunned = true;
            ennemyMove.animator.SetTrigger("Fall");
        }
        //if (col.gameObject.layer == LayerMask.NameToLayer("Player") && (Input.GetButtonDown("Push_" + ControllerIndex) || Input.GetKeyDown(KeyCode.G)) && canStun == true && !isGrab)
        //{
        //    Debug.Log("meh");
        //
        //    canStun = false;
        //}
    }


}
