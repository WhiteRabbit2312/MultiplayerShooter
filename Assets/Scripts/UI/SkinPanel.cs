using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinPanel : MonoBehaviour
{
    [SerializeField] private GameObject[] skin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseSkin()
    {

    }

    public void SelectSkin(int skinIdx)
    {
        foreach(var item in skin)
        {
            item.GetComponent<Image>().color = new Color(1, 1, 1);
        }
        skin[skinIdx].GetComponent<Image>().color = Color.green;
        PlayerPrefs.SetInt("Skin", skinIdx);

        Debug.Log("Selected skin: " + skin[skinIdx]);
    }


}
