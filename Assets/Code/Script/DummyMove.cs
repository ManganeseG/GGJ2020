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

    public int ControllerIndex;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        //Needed Var
        isGrab = false;
        holdCD = HoldCD;


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
    }

    void Update()
    {
        #region Move
        if (characterController.isGrounded)
        {

            moveDirection = new Vector3(-Input.GetAxis("Horizontal_" + ControllerIndex), 0.0f, -Input.GetAxis("Vertical_" + ControllerIndex));
            moveDirection *= speed;

            //if (Input.GetButton("Jump"))
            //{
            //    moveDirection.y = jumpSpeed;
            //}
        }

        moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);
        #endregion

        //Needed Part
        if (isGrab)
        {
            holdCD -= Time.deltaTime;
            grabbedItem.transform.position = grabPos.transform.position;

            if (Input.GetKeyDown(KeyCode.G) && holdCD <= 0f)
            {
                grabbedItem = null;
                isGrab = false;
                holdCD = HoldCD;
            }
        }
    }

    //Needed Part
    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Item") && Input.GetKeyDown(KeyCode.G) && isGrab == false)
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
    }
}
