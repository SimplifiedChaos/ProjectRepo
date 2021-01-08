using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Oof
{
    
public class ViewSolution : MonoBehaviour
{
    public InventoryControl inventoryControl;
    public SolutionViewModel viewModel;

    List<Teacher> vTeacherList;
    List<Course> vCourseList;
    Dictionary<int, int> matches;

    int[][] courseGrid;
    string[][] buttonNames;

    void Start()
    {
        // GetCourses();
        // //GetData();
        // ClassToGrid();
        // //GridToStrings();
        //inventoryControl.setTextArray(buttonNames);
    }

    public void GetData(string name)
    {
        int id = viewModel.getID(name);
        vTeacherList = viewModel.GetTeachers(id);
        vCourseList = viewModel.GetCourses(id);
        matches = viewModel.GetMatches(id);
        Debug.LogFormat("teach:{0} course:{1} matches:{2}", vTeacherList.Count, vCourseList.Count, matches.Count);
        PrintDict(matches);
    }

    void PrintDict(Dictionary<int, int> matches)
    {
        foreach(KeyValuePair<int,int> kvp in matches)
        {
            Debug.LogFormat("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
        }
    }

    void ClassToGrid()     // assume no duplicate
    {
        List<List<Course>> bigList = new List<List<Course>>();

        var group1 =    from course in vCourseList
                        //orderby (int)course.getTimeslot() ascending
                        group course by course.courseTitle into newGroup
                        select newGroup;
        
        int imax=0;
        int jmax = 0;

        foreach(var nGroup in group1)
        {
            List<Course> tempList = new List<Course>();
            int j=0;
            foreach(var course in nGroup)
            {
                tempList.Add(course);
                j++;
            }
            bigList.Add(tempList);
            jmax = Math.Max(jmax, tempList.Count);
            imax++;
        }

        imax = bigList.Count;

        courseGrid = new int[imax][];

        for(int i=0;i<imax;i++)
        {
            courseGrid[i] = new int[8];
            List<Course> tempList = bigList[i];
            for(int j=0;j<8;j++)
            {
                Course tempCourse = tempList.Find(item => (int)item.timeslot == j+1);
                if(tempCourse==null) courseGrid[i][j] = 0;
                else courseGrid[i][j] = tempCourse.getID();
            }
        }


        int rows = imax;
        int cols = 8;

        for (int i = 0; i < rows; i++)
        {
            string s= "";
            for (int j = 0; j < cols; j++)
            {
               s+= " " + courseGrid[i][j];
            }
            Debug.Log(s);
        }
    }

    void GridToStrings()
    {
        buttonNames = new string[courseGrid.Length+1][];
        string s = "start";

        for(int i=0;i<buttonNames.Length;i++)
        {
            buttonNames[i] = new string[9];
            string courseName = "no class?";
            for(int j=0;j<9;j++)
            {
                if(i==0 && j==0) s="Schedule";
                else if(i==0) s = TimeSlotToString((Timeslot)j);
                else if(j==0) s = "name of class"; //name of class
                else
                {
                    int courseID = courseGrid[i-1][j-1];
                    if(courseID==0) s = "";
                    else
                    {
                        courseName = vCourseList.Find(item=>item.getID() == courseID).courseTitle;
                        Debug.LogFormat("courseID:{0}", courseID);
                        int teacherID = matches[courseID];//read from dict to get solutionID
                        Debug.LogFormat("teacherID:{0}", teacherID);
                        if(teacherID == 0)
                        {
                            s = "No Teacher";
                        }
                        else
                        {
                            Teacher tempTeacher = vTeacherList.Find(item => item.getID() == teacherID);
                            if(tempTeacher==null)
                            {
                                Debug.LogFormat("Teacher doesn't exist??? courseID:{0} solutionID:{1}", courseID, teacherID);
                                s = "solution error";
                            }
                            else
                            {
                                s = tempTeacher.name;
                            }
                        }
                    }
                }
                buttonNames[i][j] = s;
            }
            buttonNames[i][0] = courseName;
        }
        buttonNames[0][0] = "";
    }

    public string TimeSlotToString(Timeslot time) //why can't I call this from Enum.cs?
    {
        switch(time)
        {
            case Timeslot.NoClass:
                return "No class";
            case Timeslot.MW1:
                return "MW 9:00 am ~ 10:15 am";
            case Timeslot.MW2:
                return "MW 10:30 am ~ 11:45 am";
            case Timeslot.MW3:
                return "MW 12:00 pm ~ 1:15 pm";
            case Timeslot.MW4:
                return "MW 1:30 pm ~ 2:45 pm";
            case Timeslot.TR1:
                return "TR 9:00 am ~ 10:15 am";
            case Timeslot.TR2:
                return "TR 10:30 am ~ 11:45 am";
            case Timeslot.TR3:
                return "TR 12:00 pm ~ 1:15 pm";
            case Timeslot.TR4:
                return "TR 1:30 pm ~ 2:45 pm";
        }
        return "null";
    }

    void GetCourses()
    {
        SampleTest();
    }

     public void SampleTest()
    {
        Teacher[] sampleTeachers = new Teacher[]{ // public Teacher(string tName, int tMaxClass, Dicipline[] tDiciplines, int tId)
            new Teacher("John Doe", 3, new Dicipline[]{(Dicipline)1,(Dicipline)2}, 0),
            new Teacher("Sally Sue", 2, new Dicipline[]{(Dicipline)2,(Dicipline)3}, 1),
            new Teacher("Jack Black", 1, new Dicipline[]{(Dicipline)1}, 2)
        };
        Course[] sampleClasses = new Course[]{ //public Course(string cn, string ct, Dicipline[] cDiciplines, Timeslot cTimeslot, int cid)
            new Course("Class 1", "AI", new Dicipline[]{(Dicipline)1,(Dicipline)3}, (Timeslot)1, 1),
            new Course("Class 2", "Java", new Dicipline[]{(Dicipline)2}, (Timeslot)2, 2),
            new Course("Class 3", "Data Structures", new Dicipline[]{(Dicipline)3}, (Timeslot)1, 3),
            new Course("Class 4", "Web Development", new Dicipline[]{(Dicipline)2,(Dicipline)4}, (Timeslot)2, 4),
            new Course("Class 5", "C++", new Dicipline[]{(Dicipline)1}, (Timeslot)3, 5),
            //new Course("Class 5", "C++", new Dicipline[]{(Dicipline)1}, (Timeslot)2, 7),
            new Course("Class 6", "Intro to Programming", new Dicipline[]{(Dicipline)1,(Dicipline)2,(Dicipline)3}, (Timeslot)1, 6)
        };

        vTeacherList = sampleTeachers.ToList();
        vCourseList = sampleClasses.ToList();

    }

    public void ButtonClicked(string name)
    {
        GetData(name);
        ClassToGrid();
        GridToStrings();
        inventoryControl.setTextArray(buttonNames);
        inventoryControl.makeInventory();
    }




}

}

// //int maxDup = from nGroup from 
// var group12 =   from course in nGroup
//                 group course by (int)course.timeslot into newGroup
//                 select newGroup;

// foreach(var tGroup in group12)
// {
//     BigList[i] = 

// }

