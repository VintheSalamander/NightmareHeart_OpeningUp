using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col){
        GameObject colObject = col.gameObject;
        if(colObject.CompareTag("Enemy")){
            colObject.GetComponent<CommonScript>().Damage(damage);
        }
    }
}
