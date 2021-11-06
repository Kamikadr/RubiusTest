using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public CardPanelController panelController;
    public Dropdown typeOfViewing;
    public Button canelButton;
    public Button loadButton;


    private void Start()
    {
        panelController.onLoadIsDoneEvent += OnLoadIsDone;
    }


    public void OnLoadButtonClickListener() 
    {
        loadButton.interactable = false;
        typeOfViewing.interactable = false;
        switch (typeOfViewing.value) 
        {
            case 0:
                canelButton.interactable = true;
                panelController.AllAtOnceFlip();
                break;
            case 1:
                canelButton.interactable = true;
                panelController.OneByOneFlip();
                break;
            case 2:
                canelButton.interactable = true;
                panelController.WhenImageReadyFlip();
                break;
        }
    }

    public void OnCanelButtonClickListener() 
    {
        panelController.Stop();
    }

    private void OnLoadIsDone() 
    {
        canelButton.interactable = false;
        loadButton.interactable = true;
        typeOfViewing.interactable = true;
    }



}
