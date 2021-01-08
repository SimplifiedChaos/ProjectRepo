using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Oof
{
    
public class CButtonListControl : MonoBehaviour
{
    //[System.Serializable]
    public GameObject buttonTemplate;

    //[System.Serializable]
    public CourseViewModel viewModel;

    public List<Course> courseList;

    public List<GameObject> buttons;

    void GenerateButton(string name, int id)
    {
        

        GameObject button = Instantiate(buttonTemplate) as GameObject;
        button.SetActive(true);

        button.GetComponent<CButtonListButton>().SetTextID(name, id);
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
        foreach (GameObject item in buttons)
        {
            Destroy(item);
        }
        courseList = viewModel.getCourseList();
        // name id
        foreach(Course course in courseList)
        {
            GenerateButton(course.courseTitle, course.getID());
        }

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

    public void ButtonClicked(int id)
    {
        viewModel.ButtonSelected(id);   
        //clearButton();
    }
}

}

