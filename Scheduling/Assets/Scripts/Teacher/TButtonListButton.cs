using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Oof
{
    

public class TButtonListButton : MonoBehaviour
{
    public TButtonListControl buttonControl;

    public Text myText; //The values on the screen, UI elements
    public int ID; //Same as above
    public bool isRainbow;
    
    private Button theButton;
    private ColorBlock theColor;
    private string myTextString;
    private int frame;
    
    
    public void SetTextID(string textString, int id) //Setting the button so you can manipulate it
    {
        myTextString = textString;
        myText.text = textString;
        ID = id;
    }
    
    public void OnClick() //Checks when the button gets clicked so we can store the ID of the button for knowing what is "Selected"
    {
        buttonControl.ButtonClicked(ID);
    }

    public void setTrue()
    {
        isRainbow = true;
    }

    void Awake()
    {
        theButton = GetComponent<Button>();
        theColor = GetComponent<Button>().colors;
        //ButtonTransitionColors();
        frame = 0;
        isRainbow = false;
    }

    void ButtonTransitionColors()
    {
        theColor.highlightedColor = Color.blue;
        theColor.normalColor = Color.cyan;
        theColor.pressedColor = Color.green;
 
        theButton.colors = theColor;
        Debug.Log("Color Changed");
    }

    void ButtonTransitionColorsRainbow(int num)
    {
        float fnum = (float)num;
        theColor.normalColor = Color.HSVToRGB(fnum/360, 1, 1);
        theButton.colors = theColor;
    }

    void Update()
    {
        frame++;
        if(frame>360) frame = 0;
        if(isRainbow) ButtonTransitionColorsRainbow(frame);
    }

    // void Awake()
    // {
    //     GetComponent<Button>().onClick.AddListener(OnClick);
    // }
    
}

}