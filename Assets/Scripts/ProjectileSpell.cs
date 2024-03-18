using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpell : MonoBehaviour
{
    public float speed;
    public int damage;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.position += transform.forward * (speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision col){
        GameObject colObject = col.gameObject;
        if(colObject.CompareTag("Enemy")){
            colObject.GetComponent<CommonScript>().Damage(damage);
            Destroy(gameObject);
        }
    }
}
