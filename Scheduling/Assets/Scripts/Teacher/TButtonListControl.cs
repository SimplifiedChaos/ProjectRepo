using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Oof
{
    
public class TButtonListControl : MonoBehaviour
{
    //[System.Serializable]
    public GameObject buttonTemplate;

    //[System.Serializable]
    public TeacherViewModel viewModel;

    public List<Teacher> teacherList;

    public List<GameObject> buttons;

    void GenerateButton(string name, int id) //instantiation of a button, creation
    {

        //Debug.Log("Made it here");
        GameObject button = Instantiate(buttonTemplate) as GameObject;
        button.SetActive(true);

        button.GetComponent<TButtonListButton>().SetTextID(name, id);
        button.transform.SetParent(buttonTemplate.transform.parent, false);
        if(name.Contains("Dr.")) button.GetComponent<TButtonListButton>().setTrue();


        buttons.Add(button);

        //Debug.Log("and to here");
    }

    void Start()
    {
        buttons = new List<GameObject>(); //Trackable list of buttons for management
        
    }

    public void UpdateList() //Destroys entire list of buttons, so we can create new
    {
        foreach (GameObject item in buttons)
        {
            Destroy(item);
        }
        //Debug.Log("Starting list update");
        teacherList = viewModel.getTeacherList();
        // name id
        foreach(Teacher teacher in teacherList)
        {
            GenerateButton(teacher.name, teacher.getID()); //Generates new buttons based on teachers
        }
        //Debug.Log("updated list");

    }

    void clearButton() //Clearing all buttons
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

    public void ButtonClicked(int id) //Save the ID of the selected button
    {
        viewModel.ButtonSelected(id);   
        //clearButton();
    }
}

}

