using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Video;

public class PlayerController : MonoBehaviour
{

    private CharacterController characterController;
    public float speed, mouseSensitivity;
    public Transform playerBody;
    public Vector3 lastPos;
    private Animator anim;
    public GameObject myCam;
    public bool attacking;
    public int mana;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        lastPos = playerBody.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!attacking)
        {
            Movement();
            KeyCheck();
        }

        

        
    }

    void KeyCheck()
    {
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            attacking = true;
            anim.SetBool("Attack1", true);
            Invoke("AutomatePlayer", 4f);
        }

        if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            attacking = true;
            anim.SetBool("Attack2", true);
            Invoke("AutomatePlayer", 4f);
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            attacking = true;
            anim.SetBool("Attack3", true);
            Invoke("AutomatePlayer", 4f);
        }
    }

    void AutomatePlayer()
    {
        myCam.SetActive(false);
        GameController.current.SpawnNewPlayer();
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);
        if (lastPos != playerBody.position)
        {
            anim.SetBool("Moving", true);
            lastPos = playerBody.position;
        }
        else
        {
            anim.SetBool("Moving", false);
        }
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        playerBody.Rotate(Vector3.up * mouseX * 10);
    }
}
