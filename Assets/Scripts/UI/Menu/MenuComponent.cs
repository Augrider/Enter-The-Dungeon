using System.Collections;
using System.Collections.Generic;
using Game.State;
using TMPro;
using UnityEngine;

public class MenuComponent : MonoBehaviour
{
    [SerializeField] private Canvas _menuCanvas;
    [SerializeField] private TextMeshProUGUI _menuTitleText;


    private void Awake()
    {
        _menuCanvas.enabled = false;
    }

    public void OnPlayerDead()
    {
        _menuTitleText.SetText("You lost!");
        _menuCanvas.enabled = true;
    }

    public void OnPlayerWon()
    {
        _menuTitleText.SetText("You won!");
        _menuCanvas.enabled = true;
    }


    public void Restart()
    {
        GameStateLocator.Service.ErasePlayerSave();
        GameStateLocator.Service.GoToLevel(0);
    }
}
