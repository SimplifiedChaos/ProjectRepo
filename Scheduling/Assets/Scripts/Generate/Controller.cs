using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Oof
{
    


public class Controller : MonoBehaviour
{
    public GenerateSolution generateSolution;
    public Text myTextBox;
    public Button validityButton;
    public Button generateButton;

    private string myString;


    public void AddPrint(string message)
    {
        myString += "\n"+ message;
        myTextBox.text = myString;
    }

    void Start()
    {
        myString = "Pulling Data..."; //Printing to screen
        AddPrint(myString);
        generateSolution.getData(); //Pull teachers and courses from the DB
        AddPrint("Data pulled");
        validityButton.GetComponent<Button>().interactable = true; //Allow validity check to be used
    }

    public void OnValidClick()
    {
        AddPrint("Eligibility Checking");
        generateSolution.checkEligibility();
        AddPrint("Eligibility Checked");
        generateButton.GetComponent<Button>().interactable = true; //Allows generate to be clicked
    }

    public void OnGenerateClick()
    {
        generateSolution.StartG(); //Generate solution, see GenerateSolution.cs for more
        Debug.Log("clicked");
    }

    
}

}
