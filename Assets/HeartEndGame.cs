using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartEndGame : MonoBehaviour
{

    public GameObject gameController, gameLevel, spellUI, enemies, newLevel, endCam, endPlayer, endNewPlayer, currentMusic;
    public AudioSource rumble, endSong;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider Other)
    {
        if(Other.tag == "Enemy")
        {
            Destroy(Other);
            EndGame();
        }
    }

    void EndGame()
    {
        rumble.Play();
        gameController.GetComponent<GameController>().currentPlayer.SetActive(false);
        gameController.SetActive(false);
        spellUI.SetActive(false);
        enemies.SetActive(false);
        endPlayer.SetActive(true);
        endCam.SetActive(true);
        Invoke("RemoveMap", 2f);


    }

    void RemoveMap()
    {
        gameLevel.SetActive(false);
        currentMusic.SetActive(false);
        Invoke("NewMapSpawn", 2f);

    }

    void NewMapSpawn()
    {
        endSong.Play();
        endPlayer.SetActive(false);
        endNewPlayer.SetActive(true);
        newLevel.SetActive(true);
        Invoke("EndSound", 1f);
    }

    void EndSound()
    {
        rumble.Stop();
        gameObject.GetComponent<AudioHeartController>().ChangeSpeed(0);
    }
}
