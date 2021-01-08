using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


namespace Oof
{
    public class CourseViewModel : MonoBehaviour
    {
        public CButtonListControl buttonListControl;
        public CourseMenu courseMenu;
        //public List<Course> classList = new List<Course>();
        public List<Course> courseList;

        public List<Course> getCourseList()
        {
            return new List<Course>(courseList);
        }

        public Course getCourseOf(int id)
        {
            int index = courseList.FindIndex(element => element.getID() == id);
            return courseList[index];
        }

        public bool addCourse(Course course) //returns true if successful
        {
            if(courseList.Contains(course)) return false;
            else
            {
                courseList.Add(course);
                return true;
            }
        }

        // public void addCourse(string lastName, string maxCourse, string diciplines)
        // {
        //     Course newCourse = new Course(courseList.Count, lastName, maxCourse, diciplines);
        //     courseList.Add(newCourse);
            
        // }

        public bool editCourse(Course course) //returns true if successful
        {
            int id = course.getID();
            int index = courseList.FindIndex(element => element.getID() == id);
            if(index==-1) return false;
            else
            {
                courseList[index] = course;
                return true;
            }
        }

        public bool deleteCourse(Course course)
        {
            int index = courseList.IndexOf(course);
            if(index==-1) return false;
            else
            {
                courseList.RemoveAt(index);
                return true;
            }
        }

        public bool deleteCourse(int id)
        {
            int index = courseList.FindIndex(element => element.getID() ==id);
            if(index==-1) return false;
            else
            {
                courseList.RemoveAt(index);
                return true;
            }
        }


        IEnumerator PullAllCourses()
        {
            WWWForm form = new WWWForm();

            //WWW www = new WWW("http://localhost/sqlconnect/pullcourses.php", form);
            WWW www = new WWW("https://ualrhomeworkwebserver.000webhostapp.com/unity_access/pullcourses.php", form);
            yield return www;

            string strToSplit = www.text;
            char delimiters = '\t';
            string[] arrSplit = strToSplit.Split(delimiters);

            courseList.Clear(); //This will clear the list so we can prepare for the new list to be inserted into the controller 

            for (int i = 0; i < arrSplit.Length - 1; i += 5)
            {
                Debug.Log(arrSplit[i]); //ID, courseNumber, courseTitle, timeslot, dicipline
                Course tempCourse = new Course(arrSplit[i], arrSplit[i + 1], arrSplit[i + 2], arrSplit[i + 3], arrSplit[i + 4]);
                courseList.Add(tempCourse);
            }

            Debug.Log(courseList.Count);
            Debug.Log("Finished splitting array");
            buttonListControl.UpdateList();

        }

        public void CallPullAllCourses()
        {
            StartCoroutine(PullAllCourses());
        }

        public void UpdateList()
        {
            CallPullAllCourses();
            //buttonListControl.UpdateList();
        }

        

        void Start()
        {
            courseList = new List<Course>();
            CallPullAllCourses();
            //courseList = SampleCourse();
            buttonListControl.UpdateList();
        }

        List<Course> SampleCourse()
        {
            Course[] sampleCourses = new Course[]{ //public Course(string cn, string ct, Dicipline[] cDiciplines, Timeslot cTimeslot, int cid)
            new Course("Class 1", "AI", new Dicipline[]{(Dicipline)1,(Dicipline)3}, (Timeslot)1, 0),
            new Course("Class 2", "Java", new Dicipline[]{(Dicipline)2}, (Timeslot)2, 1),
            new Course("Class 3", "Data Structures", new Dicipline[]{(Dicipline)3}, (Timeslot)1, 2),
            new Course("Class 4", "Web Development", new Dicipline[]{(Dicipline)2,(Dicipline)4}, (Timeslot)2, 3),
            new Course("Class 5", "C++", new Dicipline[]{(Dicipline)1}, (Timeslot)3, 4),
            new Course("Class 6", "Intro to Programming", new Dicipline[]{(Dicipline)1,(Dicipline)2,(Dicipline)3}, (Timeslot)1, 5)
        };
            return sampleCourses.ToList();
        }

        public void ButtonSelected(int id)
        {
            Course selectedCourse = courseList.Find(item => item.getID() == id);
            var sameName =  from course in courseList
                            where course.courseNumber == selectedCourse.courseNumber
                            select course;
            int[] toSend = new int[sameName.Count()];
            int i=0;
            foreach(Course same in sameName)
            {
                toSend[i] = (int)same.timeslot;
                i++;
            }
            courseMenu.CourseSelected(selectedCourse, toSend);
        }


    }
}

        // public struct Class
        // {
        //     public int ID;
        //     public string name;
        //     public int timeslot;
        //     public string disciplines;
        //     public Class(int ID, string name, int timeslot, string disciplines)
        //     {
        //         this.ID = ID;
        //         this.name = name;
        //         this.timeslot = timeslot;
        //         this.disciplines = disciplines;

        //     }
        // }

        // public struct Course
        // {
        //     public int ID;
        //     public string name;
        //     public string disciplines;
        //     public int maxClasses;

        //     public Course(int ID, string name, int maxClasses, string disciplines)
        //     {
        //         this.ID = ID;
        //         this.name = name;
        //         this.maxClasses = maxClasses;
        //         this.disciplines = disciplines;
        //     }
        // }
