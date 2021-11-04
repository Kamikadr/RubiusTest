using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class UIController : MonoBehaviour
{
    const string URL = "https://picsum.photos/200";

    public CardPanelController panelController;
    public Dropdown typeOfViewing;
    public Button canelButton;
    
    public void OnLoadButtonClickListener() 
    {
        switch (typeOfViewing.value) 
        {
            case 0:
                canelButton.interactable = true;
                panelController.FlipAllCards();
                canelButton.interactable = false;
                break;
            case 1:

                break;
            case 2:

                break;
        }
    }

    public void OnCanelButtonClickListener() 
    {
        panelController.StopAllCoroutines();
    }

   
}
