using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightConeController : MonoBehaviour
{
    public GameObject vfxLightCone;
    private bool isTurret;
    private bool canAttack;
    private float delaySpell;

    // Start is called before the first frame update
    void Awake()
    {
        isTurret = false;
        canAttack = true;
    }

    // Update is called once per frame
    void OnTriggerStay(Collider col){
        if(isTurret && canAttack && col.CompareTag("Enemy")){
            canAttack = false;
            GameObject lightCone = Instantiate(vfxLightCone, transform.parent.position, transform.parent.rotation);
            StartCoroutine(DestroyVFX(lightCone, delaySpell));
        }
    }

    public void StartTurret(float delay){
        isTurret = true;
        delaySpell = delay;
    }
    IEnumerator DestroyVFX(GameObject vfx, float delay){
        yield return new WaitForSeconds(delay);
        if(vfx){
            Destroy(vfx);
        }
        yield return new WaitForSeconds(delay/2);
        canAttack = true;
    }
}
