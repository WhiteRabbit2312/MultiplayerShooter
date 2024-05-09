using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class SkinPanel : MonoBehaviour
{
    [SerializeField] private GameObject[] _skin;

    private int skinIdx { get; set; }

    // Start is called before the first frame update
    public void Awake()
    {
        if (!PlayerPrefs.HasKey("Skin"))
        {
            PlayerPrefs.SetInt("Skin", 0);
        }

        skinIdx = PlayerPrefs.GetInt("Skin");
        _skin[skinIdx].GetComponent<Animator>().Play("Go");

    }

    public void SelectSkin(int skinIdx)
    {
        foreach (var item in _skin)
        {
            //item.GetComponent<Image>().color = new Color(1, 1, 1);
            item.GetComponent<Animator>().Play("Idle");
        }
        //TODO: винести string значення в константу або readonly
        _skin[skinIdx].GetComponent<Animator>().Play("Go");

        PlayerPrefs.SetInt("Skin", skinIdx);
    }


}
