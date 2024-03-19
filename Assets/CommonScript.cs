using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CommonScript : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public bool isPlayer;
    public HeartSpeedArea currentArea;

    [SerializeField] public static int mana = 2;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        currentArea = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int damage){
        health -= damage;
        Debug.Log(health + " " + gameObject.name + "oo");
        if(health < 0){
            health = 0;
        }

        if(health == 0 && !isPlayer)
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

            if(currentArea != null){
                currentArea.DecreaseEnemyCount();
            }
            GameController.current.allEnemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    public void ChangeArea(HeartSpeedArea heartSpeedArea){
        currentArea = heartSpeedArea;
    }
}
