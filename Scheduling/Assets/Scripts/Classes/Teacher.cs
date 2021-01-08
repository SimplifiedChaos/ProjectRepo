using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Oof{

public class Teacher
{
    public string name;
    public int maxClass;
    public Dicipline[] diciplines;
    public string diciplineStrings;
    public Timeslot[] teachingPeriod;
    int ID;

	public int getID() {
		return this.ID;
	}

	public void setID(int id) {
		this.ID = id;
	}

    public void ClearTimeslot()
    {
        teachingPeriod = new Timeslot[maxClass];
    }

	

    public Teacher()
    {
        name = "none";
        maxClass = 1;
        diciplines = new Dicipline[]{Dicipline.ProC,Dicipline.ProP};
        ID = 0;
    }

    public Teacher(string tName, int tMaxClass, Dicipline[] tDiciplines, int tId)
    {
        name = tName;
        maxClass = tMaxClass;
        diciplines = tDiciplines;
        ID = tId;
        teachingPeriod = new Timeslot[tMaxClass];
    }

    //for reading from sql
    // form.AddField("lastName", lastNameField.text);
    // form.AddField("maxCourses", maxCourseSlider.value.ToString());
    // form.AddField("disciplines", serial);





    // public Teacher(string tName, string tMaxClass, string tDiciplines, int tId)
    // {
    //     name = tName;
    //     maxClass = Convert.ToInt32(tMaxClass);
    //     int yLength = 0;
    //     foreach(char c in tDiciplines)
    //         if(c =='y') yLength++;
        
    //     diciplines = new Dicipline[yLength];
    //     int nextStart = 0;
    //     for(int i=0;i<diciplines.Length;i++)
    //     {
    //         int pos = tDiciplines.IndexOf("y",nextStart);
    //         diciplines[i] = (Dicipline)pos;
    //         nextStart = pos;
    //     }
    //     ID = tId;
    //     diciplineStrings = tDiciplines;
    // }

    public Teacher(int tId, string tName, int tMaxclass, string tDiciplines)
    {
        ID = tId;
        name = tName;
        maxClass = tMaxclass;
        int yLength = 0;
        foreach(char c in tDiciplines)
            if(c =='y') yLength++;
        
        diciplines = new Dicipline[yLength];
        int nextStart = 0;
        for(int i=0;i<diciplines.Length;i++)
        {
            int pos = tDiciplines.IndexOf("y",nextStart);
            diciplines[i] = (Dicipline)pos;
            nextStart = pos;
        }
        diciplineStrings = tDiciplines;
        teachingPeriod = new Timeslot[maxClass];
    }

    public Teacher(string tId, string tName, string tMaxclass, string tDiciplines)
    {
        ID = int.Parse(tId);
        name = tName;
        maxClass = int.Parse(tMaxclass);
        List<Dicipline> temp = new List<Dicipline>();
        for(int i=0;i<tDiciplines.Length;i++)
        {
            if(tDiciplines[i]=='y') temp.Add((Dicipline)i);
        }
        diciplines = temp.ToArray();
        diciplineStrings = tDiciplines;
        teachingPeriod = new Timeslot[maxClass];
    }
}
}