using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MeteorController : MonoBehaviour
{
    public GameObject vfxMeteorShower;
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
            GameObject meteorShower = Instantiate(vfxMeteorShower, new Vector3(col.transform.position.x, 0.23f, col.transform.position.z), Quaternion.identity);
            StartCoroutine(DestroyVFX(meteorShower, delaySpell));
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
