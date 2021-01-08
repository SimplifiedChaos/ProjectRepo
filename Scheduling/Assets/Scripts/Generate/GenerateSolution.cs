using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Oof{

public class GenerateSolution : MonoBehaviour
{
    public Controller controller;
    //public TeacherViewModel teacherViewModel;
    //public CourseViewModel courseViewModel;

    List<Teacher> teacherList;
    List<Course> courseList;
    //Teacher[] teachers;
    //Course[] classes;

    List<Teacher> sortedTeacherList1;
    List<Teacher> sortedTeacherList2;
    List<Course> sortedCourseList;

    Dictionary<int, int> matches1; //= new Dictionary<int, int>();
    Dictionary<int, int> matches2;

    string nameToSend;
    Dictionary<int, int> matchesToSend;
    string pulledTeacher;
    string pulledCourse;

    bool c2;
    
    //Number of classes at the same timeslot?
    
    // Start is called before the first frame update

    public void Start()
    {
        teacherList = new List<Teacher>();
        courseList = new List<Course>();
    }

    public void StartG()
    {
        SortClasses();
        Debug.Log("Data Sorted");
        Debug.Log("teacher list1");
        for(int i=0;i<sortedTeacherList1.Count;i++)
        {
            Debug.LogFormat("{0}", sortedTeacherList1[i].name);
        }
        Debug.Log("teacher list2");
        for(int i=0;i<sortedTeacherList2.Count;i++)
        {
            Debug.LogFormat("{0}", sortedTeacherList2[i].name);
        }
        Debug.Log("class list");
        for(int i=0;i<sortedCourseList.Count;i++)
        {
            Debug.LogFormat("{0}", sortedCourseList[i].courseNumber);
        }

        matches1 = Generate(sortedTeacherList1,sortedCourseList);
        controller.AddPrint("Generate 1 done");
        nameToSend = "Solution 1";
        matchesToSend = matches1;
        CallPushSolutions();

        matches2 = Generate(sortedTeacherList2, sortedCourseList);
        controller.AddPrint("Generate 2 done");
        nameToSend = "Solution 2";
        matchesToSend = matches2;
        CallPushSolutions();
    }

    public void getData()
    {
        CallPullAllTeachers();
        CallPullAllCourses();
        //SampleTest();
    }

    IEnumerator PullAllTeachers()
        {
            Debug.Log("Start");
            WWWForm form = new WWWForm();

            //WWW www = new WWW("http://localhost/sqlconnect/pullteachers.php", form);
            WWW www = new WWW("https://ualrhomeworkwebserver.000webhostapp.com/unity_access/pullteachers.php", form);
            yield return www;
            Debug.Log("Yielded www form");
            //Debug.Log(www.text.ToString());

            string strToSplit = www.text;
            pulledTeacher = strToSplit;
            char delimiters = '\t';
            string[] arrSplit = strToSplit.Split(delimiters);
            pulledTeacher = String.Join(":",arrSplit);
            teacherList.Clear(); //This will clear the list so we can prepare for the new list to be inserted into the controller 

            for (int i = 0; i < arrSplit.Length - 1; i += 4)
            {
                //Debug.Log(arrSplit[i]); //ID, name, maxclass, dicipline
                Teacher tempTeacher = new Teacher(arrSplit[i], arrSplit[i + 1], arrSplit[i + 2], arrSplit[i + 3]);
                teacherList.Add(tempTeacher);
            }
            //Debug.Log(teacherList.Count);
            Debug.Log("Finished splitting array");
            
        }

        public void CallPullAllTeachers()
        {
            StartCoroutine(PullAllTeachers());
        }

        IEnumerator PullAllCourses()
        {
            WWWForm form = new WWWForm();
            //WWW www = new WWW("http://localhost/sqlconnect/pullcourses.php", form);
            WWW www = new WWW("https://ualrhomeworkwebserver.000webhostapp.com/unity_access/pullcourses.php", form);
            yield return www;
            string strToSplit = www.text;
            //pulledCourse = strToSplit;
            char delimiters = '\t';
            string[] arrSplit = strToSplit.Split(delimiters);
            pulledCourse = String.Join(":",arrSplit);
            courseList.Clear(); //This will clear the list so we can prepare for the new list to be inserted into the controller 
            for (int i = 0; i < arrSplit.Length - 1; i += 5)
            {
                Debug.Log(arrSplit[i]); //ID, name, maxclass, dicipline
                Course tempCourse = new Course(arrSplit[i], arrSplit[i + 1], arrSplit[i + 2], arrSplit[i + 3], arrSplit[i + 4]);
                courseList.Add(tempCourse);
            }
        }

        public void CallPullAllCourses()
        {
            StartCoroutine(PullAllCourses());
        }

    public void SampleTest()
    {
        Teacher[] sampleTeachers = new Teacher[]{ // public Teacher(string tName, int tMaxClass, Dicipline[] tDiciplines, int tId)
            new Teacher("John Doe", 3, new Dicipline[]{(Dicipline)1,(Dicipline)2}, 0),
            new Teacher("Sally Sue", 2, new Dicipline[]{(Dicipline)2,(Dicipline)3}, 1),
            new Teacher("Jack Black", 1, new Dicipline[]{(Dicipline)1}, 2)
        };
        Course[] sampleClasses = new Course[]{ //public Course(string cn, string ct, Dicipline[] cDiciplines, Timeslot cTimeslot, int cid)
            new Course("Class 1", "AI", new Dicipline[]{(Dicipline)1,(Dicipline)3}, (Timeslot)1, 0),
            new Course("Class 2", "Java", new Dicipline[]{(Dicipline)2}, (Timeslot)2, 1),
            new Course("Class 3", "Data Structures", new Dicipline[]{(Dicipline)3}, (Timeslot)1, 2),
            new Course("Class 4", "Web Development", new Dicipline[]{(Dicipline)2,(Dicipline)4}, (Timeslot)2, 3),
            new Course("Class 5", "C++", new Dicipline[]{(Dicipline)1}, (Timeslot)3, 4),
            new Course("Class 6", "Intro to Programming", new Dicipline[]{(Dicipline)1,(Dicipline)2,(Dicipline)3}, (Timeslot)1, 5)
        };

        teacherList = sampleTeachers.ToList();
        courseList = sampleClasses.ToList();

    }
    /*
    The number of classes is greater than the sum of classes teachers can be assigned
    The number of classes at the same time slot is less than the number of teachers
    For each discipline attribute, the number of classes with a specific discipline is greater than the sum of classes teachers can be assigned
    */
    public string checkEligibility()//List<Teacher> teacherList, List<Course> courseList)
    {
        int tSum = 0;
        bool c1,c2,c3;
        //c1
        foreach(Teacher teacher in teacherList)
        {
            tSum += teacher.maxClass;
        }
        c1 = true;
        if(courseList.Count < tSum) c1 = false; //too many classes
        //"There are too many classes compared to the number of classes teachers can teach
        //c2
        var sortedClass =   from course in courseList
                            orderby course.getDiciplines().Length
                            group course by (int)course.getTimeslot() into newGroup
                            orderby newGroup.Count()
                            select newGroup;
        c2 = true;
        foreach(var group in sortedClass)
        {
            if(group.Count()>teacherList.Count) c2 = false; // too many classes in one period
        }
        //"There are too many classes in period i"
        //c3
        c3 = true;
        int dcSum,dtSum;
        for(int i=0;i<(Dicipline.GetNames(typeof(Dicipline)).Length);i++)
        {
            dcSum=0;    dtSum=0;
            foreach(Course course in courseList)
            {
                if(Array.Exists(course.getDiciplines(), element => element == (Dicipline)i)) dcSum++;
            }
            foreach(Teacher teach in teacherList)
            {
                if(Array.Exists(teach.diciplines, element => element == (Dicipline)i)) dtSum++;
            }
            if(dcSum>dtSum) c3 = false; //too many classes in i dicipline. 
        }
        //"There are not enought teachers to cover i"
        
        return " ";
    }

    void SortClasses()  // shouldn't be needed if the data is read in an ordered way. 
    {
        var sortedClass =   from course in courseList
                            orderby course.diciplines.Length
                            group course by (int)course.getTimeslot() into newGroup
                            orderby newGroup.Count() descending
                            select newGroup;
        
        var sortedClass2 = sortedClass.SelectMany(group => group); 
        sortedCourseList = sortedClass2.ToList();                 

        var sortedTeacher1 =    from teacher in teacherList
                                orderby teacher.diciplines.Length, teacher.maxClass descending
                                select teacher;

        sortedTeacherList1 = sortedTeacher1.ToList();

        var sortedTeacher2 =    from teacher in teacherList
                                orderby teacher.maxClass descending, teacher.diciplines.Length
                                select teacher;

        sortedTeacherList2 = sortedTeacher2.ToList();
    }

    Dictionary<int, int> Generate(List<Teacher> gTeachers, List<Course> gClasses)
    {
        Dictionary<int, int> matches = new Dictionary<int, int>();
        //int[] taughtBy = new int[gClasses.Count]; //the index of the array = class id. value at such index is the teacher id.
        List<Teacher> tList = new List<Teacher>(gTeachers); //new List<Teacher>(gTeachers);

        tList.ForEach(item => item.ClearTimeslot());

        for(int i=0;i<gClasses.Count;i++)
        {
            Course selectC = gClasses[i];
            bool found=false;
            for(int j=0;j<tList.Count;j++)
            {
                Teacher selectT = tList[j];
                bool diciplineMatches = CompareDicipline(selectC, selectT);
                Debug.LogFormat("Course:{0} Teacher:{1} diciplinematches:{2}", selectC.getID(), selectT.getID(), diciplineMatches);
                if(diciplineMatches)
                {
                    bool timeMatches = teacherFree(selectC, selectT);
                    Debug.LogFormat("Course:{0} Teacher:{1} timematches:{2}", selectC.getID(), selectT.getID(), timeMatches);
                    if(timeMatches)
                    {
                        matches.Add(selectC.getID(),selectT.getID());
                        //Debug.LogFormat("Course:{0}, {1} Teacher:{2}, {3} matches:{4}", selectC.getID(),selectC.courseTitle, selectT.getID(), selectT.name, diciplineMatches);
                        if(IsFull(selectT))
                        {
                            tList.Remove(selectT);
                            Debug.LogFormat("{0} removed!!!", selectT.getID());
                        }
                        found = true;
                        break; //should only break out of the tlist loop
                    }
                }
                Debug.LogFormat("Matche Found: {0}", found);
            }
            if(!found)
            {
                matches.Add(selectC.getID(),0); // not taught by any teacher
                //Debug.LogFormat("No teacher teaches {0}", selectC.getID());
            }
        }
        return matches;
    }

    public bool CompareDicipline(Course course, Teacher teacher) // checks if the teacher can teach such class dicipline
    {
        Debug.LogFormat("Starting Compare with {0} and {1}", course.courseTitle, teacher.name);
        //Debug.LogFormat("Course {0} = ({1}", course.courseTitle)
        for(int i=0;i<course.diciplines.Length;i++)
        {
            Dicipline cDicipline = course.diciplines[i];
            for(int j=0;j<teacher.diciplines.Length;j++)
            {
                Debug.LogFormat("i:{0}.{1}  j:{2}.{3}", i, (int)course.diciplines[i], j, (int)teacher.diciplines[j]);
                if(course.diciplines[i] == teacher.diciplines[j]) return true;
                //Debug.LogFormat("i:{0}  j:{1}", i, j);
            }
        }
        return false;
    }

    public bool teacherFree(Course course, Teacher teacher) //checks if the teacher is available at such class time
    {
        Timeslot[] teaching = teacher.teachingPeriod;
        for(int i=0;i<teaching.Length;i++)
        {
            if(teaching[i] == course.getTimeslot()) return false;

            if(teaching[i] == Timeslot.NoClass)
            {
                //int index = Array.FindIndex(teaching, element => element == Timeslot.NoClass);
                //if(index!=i) Debug.LogFormat("Somethings wrong: i={1}, index={2}, className={3}, teacherName={4}", i, index, class.getCourseTitle, teacher.getName);
                teaching[i] = course.getTimeslot();
                teacher.teachingPeriod = teaching;
                return true;
            }
        }
        return false;
    }

    void PrintDict(Dictionary<int, int> matches)
    {
        foreach(KeyValuePair<int,int> kvp in matches)
        {
            Debug.LogFormat("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
        }
    }

    public bool IsFull(Teacher teacher) // checks if teacher already has a full schedule
    {
        int index = Array.FindIndex(teacher.teachingPeriod, element => element == Timeslot.NoClass);
        if(index == -1) return true;
        else return false;
    }

    public void CallPushSolutions()
    {
        StartCoroutine(SaveNewSolution());
    }

    IEnumerator SaveNewSolution()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameToSend);
        form.AddField("matchString", matchesToString(matchesToSend));
        Debug.Log(pulledTeacher);
        form.AddField("teachers", pulledTeacher);
        form.AddField("courses", pulledCourse);
        WWW www = new WWW("https://ualrhomeworkwebserver.000webhostapp.com/unity_access/pushsolutions.php", form);
            //WWW www = new WWW("http://localhost/sqlconnect/pushsolutions.php", form); //Josh: If you can get this to access teacher.php, not as a URL but as a file path.. teacher.php will be in the same directory as the index.html that calls this program.. Access that and we got it
            yield return www;
        
        if (www.text == "0")
        {
            Debug.Log("Teacher created successfully!");
        }
        else
        {
            Debug.Log("User failed. Error #" + www.text);
        }
    }

    public string matchesToString(Dictionary<int, int> matches)
    {
        string matchString = "";
        foreach (KeyValuePair<int, int> kvp in matches)
        {
            matchString += kvp.Key + "-" + kvp.Value + "/";
        }
        //matchString = matchString.Remove(matchString.Length,1);
        matchString = matchString.Remove(matchString.Length - 1);
        Debug.Log(matchString);
        return matchString;
    }
}

}