using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinPanel : MonoBehaviour
{
    [SerializeField] private GameObject[] _skin;


    // Start is called before the first frame update
    private void Awake()
    {
        
        if (!PlayerPrefs.HasKey("Skin"))
        {
            PlayerPrefs.SetInt("Skin", 0);
            
        }

        int skinIdx = PlayerPrefs.GetInt("Skin");
        _skin[skinIdx].GetComponent<Animator>().Play("Go");

    }

    public Sprite GetSprite
    {
        get; private set;
    }

    public void SelectSkin(int skinIdx)
    {
        foreach(var item in _skin)
        {
            //item.GetComponent<Image>().color = new Color(1, 1, 1);
            item.GetComponent<Animator>().Play("Idle");
        }
        //TODO: винести string значення в константу або readonly
        _skin[skinIdx].GetComponent<Animator>().Play("Go");
        GetSprite = _skin[skinIdx].GetComponent<Image>().sprite;

        

        PlayerPrefs.SetInt("Skin", skinIdx);
    }


}
