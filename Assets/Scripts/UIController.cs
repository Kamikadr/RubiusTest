using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] private CardPanelController panelController;
    [SerializeField] private Dropdown typeOfViewing;
    [SerializeField] private Button canelButton;
    [SerializeField] private Button loadButton;
    private delegate void DelegateArray();
    private DelegateArray[] typeOfLoadingArray;


    private void Start()
    {
        panelController.onLoadIsDoneEvent += OnLoadIsDone;
        typeOfLoadingArray = new DelegateArray[3];
        typeOfLoadingArray[0] += panelController.AllAtOnceFlip;
        typeOfLoadingArray[1] += panelController.OneByOneFlip;
        typeOfLoadingArray[2] += panelController.WhenImageReadyFlip;


    }


    public void OnLoadButtonClickListener() 
    {
        loadButton.interactable = false;
        typeOfViewing.interactable = false;
        canelButton.interactable = true;
        typeOfLoadingArray[typeOfViewing.value]();
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
