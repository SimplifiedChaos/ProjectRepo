using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace Oof
{
    
public class VButtonListControl : MonoBehaviour
{
    //[System.Serializable]
    public GameObject buttonTemplate;
    public SolutionViewModel viewModel;
    public ViewSolution viewSolution;
    public Text myText;
    //[System.Serializable]
    public List<Solution> solutionList;

    public List<GameObject> buttons;

    void GenerateButton(string name, int id)
    {
        

        GameObject button = Instantiate(buttonTemplate) as GameObject;
        button.SetActive(true);

        button.GetComponent<VButtonListButton>().SetTextID("ID: " + id + "   " + name, id);
        button.transform.SetParent(buttonTemplate.transform.parent, false);
        buttons.Add(button);
    }

    void Start()
    {
        buttons = new List<GameObject>();
        //UpdateList();
        
    }

    public void UpdateList()
    {
        Debug.Log("updateList start");
        foreach (GameObject item in buttons)
        {
            Destroy(item);
        }
        solutionList = viewModel.GetSolutionList();
        Debug.LogFormat("SolutionList.Count:{0}", solutionList.Count);
        // name id
        foreach(Solution solution in solutionList)
        {
            GenerateButton(solution.name, solution.getID());
        }
        Debug.Log("updateList end");


    }

    void clearButton()
    {
        if (buttons.Count > 0)
        {
            foreach (GameObject button in buttons)
            {
                Destroy(button.gameObject);
            }
            buttons.Clear();
        }
    }

    public void SetMyText(String s)
    {
        myText.text = s;
    }

    public void ButtonClicked(int id)
    {
        //viewSolution.ButtonClicked(id);
        //viewModel.ButtonSelected(id);   
        //clearButton();
    }

    public void OnViewClick()
    {
        //viewSolution.ButtonClicked();
    }
}

}

