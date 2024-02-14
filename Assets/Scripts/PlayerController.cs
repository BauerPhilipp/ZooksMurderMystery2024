using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(1f, 7f)]
    [SerializeField] float moveSpeed;  //3f is good
    [SerializeField] float rotationSpeed; // 10f is good, 400 by using Ketra Games Version
    [SerializeField] Transform characterMesh;
    [SerializeField] RoomManager roomManager;

    private PlayerControlls playerControlls;
    public PlayerControlls PlayerControlls { get { return playerControlls; } private set { } }
    private Vector2 inputVector;
    private Rigidbody rb;


    private void Awake()
    {
        playerControlls = new PlayerControlls();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        playerControlls.Enable();
    }

    private void OnDisable()
    {
        playerControlls.Disable();
    }

    private void Update()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        inputVector = playerControlls.Player.Move.ReadValue<Vector2>();

        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        moveDirection.Normalize();

        transform.Translate(moveDirection * (Time.deltaTime * moveSpeed));

        //Rotate the Character
        if (moveDirection != Vector3.zero)
        {
            //Ketra Games Version
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

            characterMesh.transform.rotation =
                Quaternion.RotateTowards(characterMesh.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "NextLevelCollider")
        {
            roomManager.LoadNextRoom();
        }
        else if (collision.gameObject.name == "PreviousLevelCollider")
        {
            roomManager.LoadPreviousRoom();
        }
        else
        {
            //Debug.Log("Other collider");
        }
    }

}
