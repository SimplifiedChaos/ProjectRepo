using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oof
{

public class Solution : MonoBehaviour
{
    int id;
    public string name;
    Dictionary<int, int> kvpDict;
    List<Teacher> teacherList;
    List<Course> courseList;

	



    public Solution(int id, string name, Dictionary<int, int> kvpDict, List<Teacher> teacherList, List<Course> courseList)
    {
        this.id = id;
        this.name = name;
        this.kvpDict = kvpDict;
        this.teacherList = teacherList;
        this.courseList = courseList;
    }

    public Solution(string idIn, string nameIn, string DictIn, string teachIn, string courseIn)
    {
        this.id = int.Parse(idIn);
        name = nameIn;

        kvpDict = new Dictionary<int,int>();
        Debug.Log(DictIn);
        char delimiterEntries = '/';
        char delimiterPairs = '-';
        string[] arrSplit = DictIn.Split(delimiterEntries); //1D array of "32-14" . "43-12" . "343-45"
        Debug.Log(arrSplit[0]);
        foreach(string str in arrSplit)
        {
            string[] tempArr = str.Split(delimiterPairs);
            Debug.Log(tempArr.Length);
            Debug.LogFormat("{0} - {1}", tempArr[0], tempArr[1]);
            // int key = 0;
            // int value = 0;
            // Debug.Log(int.TryParse(tempArr[0], out key));
            // Debug.Log(int.TryParse(tempArr[1], out value));
            
            kvpDict.Add(int.Parse(tempArr[0]),int.Parse(tempArr[1]));
        }
        Debug.Log("kvpDict"+kvpDict.Count);

        teacherList = new List<Teacher>();
        string strToSplit = teachIn;
        char delimiters = ':';
        arrSplit = strToSplit.Split(delimiters);
        for (int i = 0; i < arrSplit.Length - 1; i += 4)
        {
            Debug.Log(arrSplit[i]); //ID, name, maxclass, dicipline
            Teacher tempTeacher = new Teacher(int.Parse(arrSplit[i]), arrSplit[i + 1], int.Parse(arrSplit[i + 2]), arrSplit[i + 3]);
            teacherList.Add(tempTeacher);
        }

        courseList = new List<Course>();
        strToSplit = courseIn;
        arrSplit = strToSplit.Split(delimiters);
        for (int i = 0; i < arrSplit.Length - 1; i += 5)
        {
            Debug.Log(arrSplit[i]); //ID, courseNumber, courseTitle, timeslot, dicipline
            Course tempCourse = new Course(arrSplit[i], arrSplit[i + 1], arrSplit[i + 2], arrSplit[i + 3], arrSplit[i + 4]);
            courseList.Add(tempCourse);
        }
    }

    public int getID()
    {
        return this.id;
    }
    public void setID(int id)
    {
        this.id = id;
    }

    public Dictionary<int,int> getDictionary()
    {
        Debug.Log("kvpDict.Count in Solution " + kvpDict.Count);
        return new Dictionary<int,int>(kvpDict);
    }

    public void setDictionary(Dictionary<int,int> thisDict)
    {
        this.kvpDict = thisDict;
    }

    public List<Teacher> getTeacherList()
    {
		return this.teacherList;
	}

	public void setTeacherList(List<Teacher> teacherList)
    {
		this.teacherList = teacherList;
	}

	public List<Course> getCourseList()
    {
		return this.courseList;
	}

	public void setCourseList(List<Course> courseList)
    {
		this.courseList = courseList;
	}

    //Methods here
    //I dont think we need a clear or alternative contructors here
}

}
