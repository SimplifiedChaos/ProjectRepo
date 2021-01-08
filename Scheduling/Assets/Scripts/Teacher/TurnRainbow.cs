using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnRainbow : MonoBehaviour
{
    private bool isRainbow;
    private Button theButton;
    private ColorBlock og;
    private ColorBlock theColor;
    private int frame;
    // Start is called before the first frame update
    void Start()
    {
        theButton = GetComponent<Button>();
        theColor = GetComponent<Button>().colors;
        og = theColor;
        //ButtonTransitionColors();
        frame = 0;
        isRainbow = false;
    }

    public void setTrue()
    {
        isRainbow = true;
    }

    public void setFalse()
    {
        isRainbow = false;
        theButton.colors = og;
    }

    // Update is called once per frame
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
