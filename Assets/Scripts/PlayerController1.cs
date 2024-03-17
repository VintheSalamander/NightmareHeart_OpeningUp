using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Video;

public class PlayerController1 : MonoBehaviour
{

    private CharacterController characterController;
    public float speed, mouseSensitivity;
    public Transform playerBody;
    public Vector3 lastPos;
    private Animator anim;
    public GameObject myCam;
    public bool attacking;

    public GameObject vfxMeteorShower;
    public float delayMeteorShower;
    public GameObject vfxLightCone;
    public float delayLightCone;
    public GameObject vfxProjectile;
    public float delayProjectile;
    public GameObject vfxAutoAttack;
    public float delayAA;

    public GameObject boxMeteorShower;
    public GameObject boxLightCone;
    public GameObject boxProjectile;
    
    private bool canAA;

    private GameObject currentSpell;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        lastPos = playerBody.position;
        currentSpell = vfxAutoAttack;
        canAA = true;
        boxMeteorShower.SetActive(false);
        boxLightCone.SetActive(false);
        boxProjectile.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!attacking)
        {
            currentSpell = vfxAutoAttack;
            Movement();
            KeyCheck();
        }
    }

    void KeyCheck()
    {
        if(Input.GetKeyUp(KeyCode.Alpha1))
        {
            attacking = true;
            anim.SetBool("Attack1", true);
            DoProjectileSpell(transform.position, transform.rotation);
            currentSpell = vfxProjectile;
            Invoke("AutomatePlayer", 4f);
            
        }

        if(Input.GetKeyUp(KeyCode.Mouse0)){
            if(canAA){
                DoAutoAttack(transform.position, transform.rotation);
                canAA = false;
                StartCoroutine(EnableAutoAttack());
            }
        }

        if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            attacking = true;
            anim.SetBool("Attack2", true);
            DoMeteorShower(transform.position, transform.rotation);
            currentSpell = vfxMeteorShower;
            Invoke("AutomatePlayer", 4f);
            
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            attacking = true;
            anim.SetBool("Attack3", true);
            DoLightCone(transform.position, transform. rotation);
            currentSpell = vfxLightCone;
            Invoke("AutomatePlayer", 4f);
            
        }
    }

    void AutomatePlayer()
    {
        myCam.SetActive(false);
        GameController1.current.SpawnNewPlayer();
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

    void DoAutoAttack(Vector3 position, Quaternion rotation){
        GameObject autoAttack = Instantiate(vfxAutoAttack, position, rotation);
        StartCoroutine(DestroyVFX(autoAttack, delayAA));
    }

    void DoMeteorShower(Vector3 position, Quaternion rotation){
        GameObject meteorShower = Instantiate(vfxMeteorShower, position, rotation);
        Debug.Log("Check");
        StartCoroutine(DestroyVFX(meteorShower, delayMeteorShower));
    }

    void DoLightCone(Vector3 position, Quaternion rotation){
        GameObject lightCone = Instantiate(vfxLightCone, position, rotation);
        StartCoroutine(DestroyVFX(lightCone, delayLightCone));
    }

    void DoProjectileSpell(Vector3 position, Quaternion rotation){
        GameObject projectile = Instantiate(vfxProjectile, position, rotation);
        StartCoroutine(DestroyVFX(projectile, delayLightCone));
    }

    public static IEnumerator DestroyVFX(GameObject vfx, float delay){
        Debug.Log("Check1");
        yield return new WaitForSeconds(delay);
        Debug.Log("Check2");
        Destroy(vfx);
    }

    IEnumerator EnableAutoAttack(){
        yield return new WaitForSeconds(delayAA/2);
        canAA = true;
    }

}
