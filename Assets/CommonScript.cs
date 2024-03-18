using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CommonScript : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public bool isPlayer;

    [SerializeField] public static int mana = 2;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0 && !isPlayer)
        {

            switch (maxHealth)
            {
                case >5:
                    CommonScript.mana += 4;
                    break;

                default:
                    CommonScript.mana += 1;
                    break;
            }


            GameController.current.allEnemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
