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
    private Rigidbody rb;
    private BoxCollider boxCollider;
    private bool isTurret;

    public AudioSource basicAttack, enerygyBall, lightConeSound, meteors;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
        isTurret = false;

        anim = GetComponent<Animator>();
        lastPos = playerBody.position;

        currentSpell = vfxAutoAttack;
        canAA = true;

        boxLightCone.SetActive(false);
        boxMeteorShower.SetActive(false);
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
        if(Input.GetKeyUp(KeyCode.Mouse0)){
            if(canAA){
                DoAutoAttack(transform.position, transform.rotation);
                canAA = false;
                StartCoroutine(EnableAutoAttack());
            }
        }

        if(Input.GetKeyUp(KeyCode.Alpha2))
        {   
            isTurret = true;
            attacking = true;
            rb.isKinematic = true;
            boxCollider.enabled = false;

            anim.SetBool("Attack1", true);
            DoProjectileSpell(transform.position, transform.rotation);
            currentSpell = vfxProjectile;
            Invoke("AutomatePlayer", delayProjectile);
            
        }

        if(Input.GetKeyUp(KeyCode.Alpha3))
        {
            isTurret = true;
            attacking = true;
            rb.isKinematic = true;
            boxCollider.enabled = false;

            anim.SetBool("Attack2", true);
            Vector3 spawnPosition = transform.position + transform.forward * 20f;
            DoMeteorShower(spawnPosition, transform.rotation);
            currentSpell = vfxMeteorShower;
            Invoke("AutomatePlayer", delayMeteorShower);
            
        }

        if(Input.GetKeyUp(KeyCode.Alpha1))
        {
            isTurret = true;
            attacking = true;
            rb.isKinematic = true;
            boxCollider.enabled = false;

            anim.SetBool("Attack3", true);
            DoLightCone(transform.position, transform. rotation);
            currentSpell = vfxLightCone;
            Invoke("AutomatePlayer", delayLightCone);
            
        }
    }

    void AutomatePlayer()
    {
        myCam.SetActive(false);
        if(currentSpell == vfxLightCone){
            boxLightCone.SetActive(true);
            boxLightCone.GetComponent<LightConeController>().StartTurret(delayLightCone);
        }else if(currentSpell == vfxMeteorShower){
            boxMeteorShower.SetActive(true);
            boxMeteorShower.GetComponent<MeteorController>().StartTurret(delayMeteorShower);
        }else if(currentSpell == vfxProjectile){
            boxProjectile.SetActive(true);
            boxProjectile.GetComponent<ProjectileController>().StartTurret(delayProjectile);
        }
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

    void DoAutoAttack(Vector3 position, Quaternion rotation){
        GameObject autoAttack = Instantiate(vfxAutoAttack, position, rotation);
        basicAttack.Play();
        StartCoroutine(DestroyVFX(autoAttack, delayAA));
    }

    void DoMeteorShower(Vector3 position, Quaternion rotation){
        GameObject meteorShower = Instantiate(vfxMeteorShower, position, rotation);
        meteors.Play();
        StartCoroutine(DestroyVFX(meteorShower, delayMeteorShower));
    }

    void DoLightCone(Vector3 position, Quaternion rotation){
        GameObject lightCone = Instantiate(vfxLightCone, position, rotation);
        lightConeSound.Play();
        StartCoroutine(DestroyVFX(lightCone, delayLightCone));
    }

    void DoProjectileSpell(Vector3 position, Quaternion rotation){
        GameObject projectile = Instantiate(vfxProjectile, position, rotation);
        enerygyBall.Play();
        StartCoroutine(DestroyVFX(projectile, delayProjectile));
    }

    static IEnumerator DestroyVFX(GameObject vfx, float delay){
        yield return new WaitForSeconds(delay);
        if(vfx){
            Destroy(vfx);
        }
    }

    IEnumerator EnableAutoAttack(){
        yield return new WaitForSeconds(delayAA/2);
        canAA = true;
    }

    void OnCollisionEnter(Collision other)
    {
        if(!isTurret){
            if (other.gameObject.tag == "Enemy"){
                GameController.current.SpawnNewPlayer();
                Destroy(gameObject);
            }
        }
    }
}
