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
    private delegate void d();
    d[] del;


    private void Start()
    {
        panelController.onLoadIsDoneEvent += OnLoadIsDone;
        del = new d[3];
        del[0] += panelController.AllAtOnceFlip;
        del[1] += panelController.OneByOneFlip;
        del[2] += panelController.WhenImageReadyFlip;


    }


    public void OnLoadButtonClickListener() 
    {
        loadButton.interactable = false;
        typeOfViewing.interactable = false;
        canelButton.interactable = true;
        del[typeOfViewing.value]();
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
