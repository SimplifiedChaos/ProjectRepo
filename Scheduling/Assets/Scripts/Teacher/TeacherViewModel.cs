using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;


namespace Oof
{
    public class TeacherViewModel : MonoBehaviour
    {
        public TButtonListControl buttonListControl;
        public TeacherMenu teacherMenu;
        //public List<Course> classList = new List<Course>();
        public List<Teacher> teacherList;

        public List<Teacher> getTeacherList()
        {
            return new List<Teacher>(teacherList);
        }    


        IEnumerator PullAllTeachers()
        {
            Debug.Log("Start");
            WWWForm form = new WWWForm();

            WWW www = new WWW("https://ualrhomeworkwebserver.000webhostapp.com/unity_access/pullteachers.php", form);
            yield return www;
            Debug.Log("Yielded www form");
            Debug.Log(www.text.ToString());

            string strToSplit = www.text;
            char delimiters = '\t';
            string[] arrSplit = strToSplit.Split(delimiters);

            teacherList.Clear(); //This will clear the list so we can prepare for the new list to be inserted into the controller 

            for (int i = 0; i < arrSplit.Length - 1; i += 4)
            {
                Debug.Log(arrSplit[i]); //ID, name, maxclass, dicipline
                Teacher tempTeacher = new Teacher(int.Parse(arrSplit[i]), arrSplit[i + 1], int.Parse(arrSplit[i + 2]), arrSplit[i + 3]);
                teacherList.Add(tempTeacher);
            }
            Debug.Log(teacherList.Count);
            Debug.Log("Finished splitting array");
            buttonListControl.UpdateList();
        }

        public void CallPullAllTeachers()
        {
            StartCoroutine(PullAllTeachers());
        }

        public void UpdateList()
        {
            CallPullAllTeachers();
        }

        

        void Start()
        {
            teacherList = new List<Teacher>();
            CallPullAllTeachers();
            buttonListControl.UpdateList();
        }

        List<Teacher> SampleTeacher()
        {
            Teacher[] sampleTeachers = new Teacher[]{ // public Teacher(string tName, int tMaxClass, Dicipline[] tDiciplines, int tId)
                new Teacher("John Doe", 3, new Dicipline[]{(Dicipline)1,(Dicipline)2}, 0),
                new Teacher("Sally Sue", 2, new Dicipline[]{(Dicipline)2,(Dicipline)3}, 1),
                new Teacher("Jack Black", 1, new Dicipline[]{(Dicipline)1}, 2)
            };
            return sampleTeachers.ToList();
        }

        public void ButtonSelected(int id)
        {
            teacherMenu.TeacherSelected(teacherList.Find(item => item.getID() == id));
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

        // public struct Teacher
        // {
        //     public int ID;
        //     public string name;
        //     public string disciplines;
        //     public int maxClasses;

        //     public Teacher(int ID, string name, int maxClasses, string disciplines)
        //     {
        //         this.ID = ID;
        //         this.name = name;
        //         this.maxClasses = maxClasses;
        //         this.disciplines = disciplines;
        //     }
        // }
