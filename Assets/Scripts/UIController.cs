using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class UIController : MonoBehaviour
{

    public CardPanelController panelController;
    public Dropdown typeOfViewing;
    public Button canelButton;
    
    public void OnLoadButtonClickListener() 
    {
        switch (typeOfViewing.value) 
        {
            case 0:
                canelButton.interactable = true;
                panelController.AllAtOnceFlip();
                canelButton.interactable = false;
                break;
            case 1:
                panelController.OneByOneFlip();
                break;
            case 2:
                panelController.WhenImageReadyFlip();
                break;
        }
    }

    public void OnCanelButtonClickListener() 
    {
        panelController.StopAllCoroutines();
    }

   
}
