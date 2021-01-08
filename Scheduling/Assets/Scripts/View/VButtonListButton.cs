using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Oof
{
    

public class VButtonListButton : MonoBehaviour
{
    public VButtonListControl buttonControl;

    public Text myText;
    public int ID;
    
    private string myTextString;
    
    public void SetTextID(string textString, int id)
    {
        myTextString = textString;
        myText.text = textString;
        ID = id;
    }
    
    public void OnClick()
    {
        buttonControl.ButtonClicked(ID);
    }

    // void Awake()
    // {
    //     GetComponent<Button>().onClick.AddListener(OnClick);
    // }
    
}

}