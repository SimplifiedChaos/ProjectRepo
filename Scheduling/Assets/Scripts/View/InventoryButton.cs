using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    public Text myText;
    public bool isRainbow;

    private Button theButton;
    private ColorBlock og;
    private ColorBlock theColor;
    private int frame;

    public void SetText(string mytext)
    {
        myText.text = mytext;
    }

    void ButtonTransitionColors()
    {
        theColor.highlightedColor = Color.blue;
        theColor.normalColor = Color.cyan;
        theColor.pressedColor = Color.green;
 
        theButton.colors = theColor;
        Debug.Log("Color Changed");
    }

    public void setBlue()
    {
        Debug.Log("Blue");
        theColor.normalColor = Color.HSVToRGB(261f/360f, 0.3f, 1);
        theButton.colors = theColor;
    }

    public void setGreen()
    {
        Debug.Log("Green");
        theColor.normalColor = Color.HSVToRGB(150f/360f, 0.3f, 1);
        theButton.colors = theColor;
    }

    public void setNormal()
    {
        theButton.colors = og;
    }

    void Awake()
    {
        theButton = GetComponent<Button>();
        theColor = GetComponent<Button>().colors;
        og = theColor;
        frame = 0;
        isRainbow = false;
    }

    public void setTrue()
    {
        isRainbow = true;
    }

    void Update()
    {
        frame++;
        if(frame>360) frame = 0;
        if(isRainbow) ButtonTransitionColorsRainbow(frame);
    }

    void ButtonTransitionColorsRainbow(int num)
    {
        float fnum = (float)num;
        theColor.normalColor = Color.HSVToRGB(fnum/360, 1, 1);
        theButton.colors = theColor;
    }
}
