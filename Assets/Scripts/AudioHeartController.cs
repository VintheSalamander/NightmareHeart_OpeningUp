using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHeartController : MonoBehaviour
{
    public AudioSource audioSource;
    public Material normalMat;
    public Material lightMat;
    private Renderer[] childRenderers;
    public float scaleMultiplier = 1f;
    public GameObject enemies;
    public bool enemiesOnOff;
    public GameObject areaSpeedOne;
    public GameObject areaSpeedTwo;
    private float[] speedOptions = { 0.5f, 1.0f, 2.0f };
    private bool speedTwoActive;
    private bool speedOneActive;
    private bool endGame;

    void Awake(){
        endGame = false;
        speedTwoActive = false;
        childRenderers = GetComponentsInChildren<Renderer>();
        ChangeSpeed(0);
        audioSource.volume *= 2f;
    }

    void Update()
    {
        float[] spectrumData = new float[256];
        audioSource.GetOutputData(spectrumData, 0);

        float averageVolume = 0f;
        for (int i = 0; i < spectrumData.Length; i++){
            averageVolume += spectrumData[i];
        }
        averageVolume /= spectrumData.Length;

        transform.localScale = Vector3.one * (1 + averageVolume * scaleMultiplier);
        if(transform.localScale == Vector3.one){
            ChangeMaterial(normalMat);
            if(enemiesOnOff){
                enemies.SetActive(false);
            }
        }else{
            ChangeMaterial(lightMat);
            if(enemiesOnOff){
                enemies.SetActive(true);
            }
        }
    }

    void ChangeMaterial(Material newMat){
        foreach (Renderer rend in childRenderers){
            if (rend != null){
                rend.material = newMat;
            }
        }
    }

    public void ChangeSpeed(int speedIndex){
        if(!endGame){
            if(speedIndex == 2){
                audioSource.pitch = speedOptions[speedIndex];
                speedTwoActive = true;
            }else if(speedIndex == 1 && !speedTwoActive){
                if(!speedOneActive){
                    audioSource.pitch = speedOptions[speedIndex - 1];
                }else{
                    audioSource.pitch = speedOptions[speedIndex];
                }
            }else if(speedIndex == 0 && !speedTwoActive){
                audioSource.pitch = speedOptions[speedIndex];
            }
        }else{
            audioSource.pitch = speedOptions[0];
        }
    }

    public void SetEndGameTrue(){
        endGame = true;
    }
    public void SetSpeedTwoActive(bool set){
        speedTwoActive = set;
    }

    public void SetSpeedOneActive(bool set){
        speedOneActive = set;
    }
}
