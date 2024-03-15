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
    private float[] speedOptions = { 0.5f, 1.0f, 2.0f };
    private int currentSpeedIndex = 1;

    void Awake(){
        childRenderers = GetComponentsInChildren<Renderer>();
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
        }else{
            ChangeMaterial(lightMat);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)){
            if(currentSpeedIndex != 2){
                currentSpeedIndex += 1;
                ChangeSpeed(currentSpeedIndex);
            }
        }else if (Input.GetKeyDown(KeyCode.Alpha2)){
            if(currentSpeedIndex != 0){
                currentSpeedIndex -= 1;
                ChangeSpeed(currentSpeedIndex);
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

    void ChangeSpeed(int speedIndex){
        currentSpeedIndex = speedIndex;
        audioSource.pitch = speedOptions[speedIndex];
    }
}
