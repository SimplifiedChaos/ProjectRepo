using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oof{

public class Course
{
    public string courseNumber;
    public string courseTitle;
    public Dicipline[] diciplines;
    public Timeslot timeslot;
    int ID;
    
	public int getID() {
		return this.ID;
	}

	public void setID(int id) {
		this.ID = id;
	}

	public string getCourseNumber() {
		return this.courseNumber;
	}

	public void setCourseNumber(string courseNumber) {
		this.courseNumber = courseNumber;
	}

	public string getCourseTitle() {
		return this.courseTitle;
	}

	public void setCourseTitle(string courseTitle) {
		this.courseTitle = courseTitle;
	}

	public Dicipline[] getDiciplines() {
		return this.diciplines;
	}

	public void setDiciplines(Dicipline[] diciplines) {
		this.diciplines = diciplines;
	}

	public Timeslot getTimeslot() {
		return this.timeslot;
	}

	public void setTimeslot(Timeslot timeslot) {
		this.timeslot = timeslot;
	}

    
    public Course()
    {
        courseNumber = "0000";
        courseTitle = "NoClass";
        diciplines = new Dicipline[]{0};
        timeslot = 0;
        ID = 0;
    }

    public Course(string cn, string ct, Dicipline[] cDiciplines, Timeslot cTimeslot, int cid)
    {
        courseNumber = cn;
        courseTitle = ct;
        diciplines = cDiciplines;
        timeslot = cTimeslot;
        ID = cid;
    }

	public Course(string id, string number, string title, string ctimselot, string dicipline)
	{
		ID = int.Parse(id);
		courseNumber = number;
		courseTitle = title;
		timeslot = (Timeslot)int.Parse(ctimselot);
		List<Dicipline> temp = new List<Dicipline>();
        for(int i=0;i<dicipline.Length;i++)
        {
            if(dicipline[i]=='y') temp.Add((Dicipline)i);
        }
        diciplines = temp.ToArray();
	}


 
}

}