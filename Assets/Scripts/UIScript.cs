using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public TextMeshProUGUI manaText;
    public Color canSpellColor;
    public Color cannotSpellColor;
    public Image lightConeImage;
    public Image projectileImage;
    public Image meteorShowerImage;
    // Start is called before the first frame update
    void Start()
    {
        manaText.text = CommonScript.mana.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        int mana = CommonScript.mana;
        manaText.text = mana.ToString();
        if(mana >= 3){
            lightConeImage.color = canSpellColor;
        }else{
            lightConeImage.color = cannotSpellColor;
        }

        if(mana >= 6){
            projectileImage.color = canSpellColor;
        }else{
            projectileImage.color = cannotSpellColor;
        }

        if(mana >= 12){
            meteorShowerImage.color = canSpellColor;
        }else{
            meteorShowerImage.color = cannotSpellColor;
        }
    }
}
