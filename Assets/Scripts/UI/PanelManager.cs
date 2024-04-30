using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _skinPanel;

    // Start is called before the first frame update

    public void OpenSkinPanel()
    {
        _skinPanel.SetActive(true);
        _mainPanel.SetActive(false);
    }

    public void BackButton(GameObject panel)
    {
        panel.SetActive(false);
        _mainPanel.SetActive(true);
    }
}
