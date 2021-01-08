using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Oof
{
    

public class InventoryControl : MonoBehaviour
{

    public GameObject buttonTemplate;
    public GridLayoutGroup gridGroup;
    public Color myColor;

    int[] list;
    string[] stringList;
    string[][] matchList;
    List<GameObject> buttons;

    void Start()
    {
        buttons = new List<GameObject>();
        stringList = new string[]{"1","2","3","4","5","6","7","8","9","10","11","","13","14","","","17","18"};
        //makeInventory();
    }



    public void makeInventory()
    {

        foreach (GameObject item in buttons)
        {
            Destroy(item);
        }

        int row = matchList.Length;
        int col = 9;
        for(int i=0;i<row;i++)
        {
            for(int j=0;j<col;j++)
            {
                string s = matchList[i][j];
                //string s = stringList[j+col*i];
            
                GameObject newButton = Instantiate(buttonTemplate) as GameObject;
                newButton.GetComponent<InventoryButton>().SetText(s);
                newButton.transform.SetParent(buttonTemplate.transform.parent, false);
                newButton.SetActive(true);
                buttons.Add(newButton);

                if(i==0 & j==0)
                {
                    newButton.GetComponent<Image>().color = myColor; //which clears it?
                }
                else if(i==0)
                {
                    newButton.GetComponent<InventoryButton>().setBlue();
                }
                else if(j==0)
                {
                    newButton.GetComponent<InventoryButton>().setGreen();
                }
                else
                {
                    newButton.GetComponent<InventoryButton>().setNormal();
                }

                if(s=="") newButton.GetComponent<Image>().color = myColor;
                if(s.Contains("Dr.")) newButton.GetComponent<InventoryButton>().isRainbow = true;

                
                Debug.LogFormat("Num{0}", s);

            }
        }
    }

    public void setTextArray(string[][] toSet)
    {
        matchList = toSet;
    }

    void setIntArray(int[] toSet)
    {
        list = toSet;
    }
}

}
