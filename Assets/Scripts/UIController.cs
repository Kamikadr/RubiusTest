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
    private delegate void DelegateArray();
    DelegateArray[] delegateArray;


    private void Start()
    {
        panelController.onLoadIsDoneEvent += OnLoadIsDone;
        delegateArray = new DelegateArray[3];
        delegateArray[0] += panelController.AllAtOnceFlip;
        delegateArray[1] += panelController.OneByOneFlip;
        delegateArray[2] += panelController.WhenImageReadyFlip;


    }


    public void OnLoadButtonClickListener() 
    {
        loadButton.interactable = false;
        typeOfViewing.interactable = false;
        canelButton.interactable = true;
        delegateArray[typeOfViewing.value]();
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
