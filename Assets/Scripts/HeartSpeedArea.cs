using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSpeedArea : MonoBehaviour
{
    public AudioHeartController audioHeartController;
    public int speedIndex;
    public int enemyCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col){
        if(col.CompareTag("Enemy")){
            col.GetComponent<CommonScript>().ChangeArea(this);
            IncreaseEnemyCount();
        }
    }

    void OnTriggerExit(Collider col){
        if(col.CompareTag("Enemy")){
            DecreaseEnemyCount();
        }
    }

    public void IncreaseEnemyCount(){
        enemyCount++;
        if (enemyCount == 1){
            if(speedIndex == 1){
                audioHeartController.SetSpeedOneActive(true);
            }
            audioHeartController.ChangeSpeed(speedIndex);
        }
    }

    public void DecreaseEnemyCount(){
        enemyCount--;
        if (enemyCount == 0){
            if(speedIndex == 2){
                audioHeartController.SetSpeedTwoActive(false);
            }
            if(speedIndex == 1){
                audioHeartController.SetSpeedOneActive(false);
            }
            audioHeartController.ChangeSpeed(speedIndex - 1);
        }
    }

}
