using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Oof
{
    //Scene 0 = Home Menu
    //Scene 1 = Class Menu
    //Scene 2 = Teacher Menu
    public class CourseMenu : MonoBehaviour
    {
        public CourseViewModel viewModel;
        //All inputs, including the toggles being formed into a JSON file
        public InputField courseTitleField;
        public InputField courseNumberField; //String preferably so we can just parse back

        public Toggle ts1; //Time slot 1, etc
        public Toggle ts2;
        public Toggle ts3;
        public Toggle ts4;
        public Toggle ts5;
        public Toggle ts6;
        public Toggle ts7;
        public Toggle ts8;
        public Toggle[] tstoggles; 

        public Toggle progC; //The various disciplines
        public Toggle progP;
        public Toggle gameDev;
        public Toggle DSaA;
        public Toggle compOrg;
        public Toggle opSys;
        public Toggle progLang;
        public Toggle cyberSec;
        public Toggle mobApps;
        public Toggle AI;
        public Toggle networks;
        public Toggle ToC;
        public Toggle PaDS;
        public Toggle[] dtoggles;

        public Button saveNewCourseButton;
        public Button editCourseButton;
        public Button deleteCourseButton;
        public Button deSelect;

        public Toggle courseSelected;
        public int selectedID;


        public bool[] disciplines;
        public bool[] timeslots;

        public void Start()
        {
            disciplines = new bool[13];
            timeslots = new bool[8];
            tstoggles= new Toggle[]{ts1,ts2,ts3,ts4,ts5,ts6,ts7,ts8};
            dtoggles = new Toggle[]{progC, progP, gameDev, DSaA, compOrg, opSys,
             progLang, cyberSec, mobApps, AI, networks, ToC, PaDS};
        }

        int[] getTSToggles()
        {
            int[] toSend = new int[tstoggles.Length];
            for(int i=0;i<tstoggles.Length;i++)
            {
                if(tstoggles[i].isOn) toSend[i] = i+1;
                else toSend[i] = 0;
            }
            return toSend;
        }

        public void ChangeDiscToggles(int num)
        {
            disciplines[num] = !disciplines[num];
        }

        public void ChangeTimeToggles(int num)
        {
            timeslots[num] = !timeslots[num];
        }

        //TODO
        public void CourseSelected(Course course, int[] alreadyToggles)
        {
            courseSelected.GetComponent<Toggle>().isOn = true;
            saveNewCourseButton.GetComponent<Button>().interactable = false;
            editCourseButton.GetComponent<Button>().interactable = true;
            deleteCourseButton.GetComponent<Button>().interactable = true;

            //courseSelected.GetComponentsInChildren<Text>().text = "Course Selected  ID:"+ course.getID().ToString();
            ClearFields();

            selectedID = course.getID();
            courseTitleField.text = course.courseTitle;
            courseNumberField.text = course.courseNumber;

            foreach(Toggle ts in tstoggles)
            {
                ts.interactable = true;
                ts.isOn = false;
            }
            int selectedToggle = (int)course.timeslot;
            foreach(int time in alreadyToggles)
            {
                tstoggles[time-1].interactable = false;
            }
            tstoggles[selectedToggle-1].interactable = true;
            tstoggles[selectedToggle-1].isOn = true;

            foreach(Dicipline dis in course.diciplines)
            {
                dtoggles[(int)dis].isOn = true;
            }
            
        }
        //TODO
        public void Deselect()
        {
            courseSelected.GetComponent<Toggle>().isOn = false;
            saveNewCourseButton.GetComponent<Button>().interactable = true;
            editCourseButton.GetComponent<Button>().interactable = false;
            deleteCourseButton.GetComponent<Button>().interactable = false;

            //courseSelected.GetComponentsInChildren<Text>().text = "Course Selected  ID:";

            ClearFields();



        }
        //TODO
        public void ClearFields()
        {
            courseNumberField.text = "";
            courseTitleField.text = ""; 

            foreach(Toggle ts in tstoggles)
            {
                ts.interactable = true;
                ts.isOn = false;
            }

            foreach(Toggle ds in dtoggles)
            {
                ds.interactable = true;
                ds.isOn = false;
            }
        }


        
        public void CallSaveNewClass()
        {
            StartCoroutine(SaveNewClass());
        }

        public void CallEdit()
        {
            CallDelete();
            CallSaveNewClass();
        }

        public void CallDelete()
        {
            StartCoroutine(Delete());
        }

        
        IEnumerator SaveNewClass()
        {
            Debug.Log("Starting Save new class");
            string serialDisc = "";
            //This is where i serialize the bool array into a string of chars for the DISCIPLINES
            for (int i = 0; i < 13; i++)
            {
                if (disciplines[i] == true)
                {
                    serialDisc += "y";
                }
                else
                {
                    serialDisc += "n";
                }
            }
            int[] serialTime = getTSToggles();



            //WWW www = new WWW("https://ualrhomeworkwebserver.000webhostapp.com/course.php", form)
            Debug.Log(serialTime.Length);
            for(int i = 0; i < serialTime.Length; i++)
            {
                if (serialTime[i] != 0)
                {
                    WWWForm form = new WWWForm();
                    form.AddField("courseName", courseTitleField.text);
                    form.AddField("courseNumber", courseNumberField.text);
                    form.AddField("disciplines", serialDisc);
                    form.AddField("timeslots", serialTime[i]);
                    WWW www = new WWW("https://ualrhomeworkwebserver.000webhostapp.com/unity_access/course.php", form);
                    //WWW www = new WWW("http://localhost/sqlconnect/course.php", form); //Josh: If you can get this to access teacher.php, not as a URL but as a file path.. teacher.php will be in the same directory as the index.html that calls this program.. Access that and we got it
                    yield return www;
                    Debug.Log(www.text.ToString());
                }
            }
            viewModel.UpdateList();
            Debug.Log("Class save finished");
        }

        IEnumerator Edit()
        {
            string serialDisc = "";
            //This is where i serialize the bool array into a string of chars for the DISCIPLINES
            for (int i = 0; i < 13; i++)
            {
                if (disciplines[i] == true)
                {
                    serialDisc += "y";
                }
                else
                {
                    serialDisc += "n";
                }
            }
            int[] serialTime = new int[8];
            //This is where I serialize the bool array into a string of chars for the TIMESLOTS
            for (int i = 0; i < 8; i++)
            {
                if (timeslots[i] == true)
                {
                    serialTime[i] = i + 1;
                }
                else
                {
                    serialTime[i] = 0;
                }
            }

            WWWForm form = new WWWForm();
            form.AddField("id", selectedID);
            form.AddField("courseName", courseTitleField.text);
            form.AddField("courseNumber", courseNumberField.text);
            form.AddField("disciplines", serialDisc);
            form.AddField("timeslots", serialTime[0]); //needs work

            WWW www = new WWW("https://ualrhomeworkwebserver.000webhostapp.com/unity_access/editcourse.php", form);
            //WWW www = new WWW("http://localhost/sqlconnect/editcourse.php", form); //Josh: If you can get this to access teacher.php, not as a URL but as a file path.. teacher.php will be in the same directory as the index.html that calls this program.. Access that and we got it
            yield return www;

            if (www.text == "0")
            {
                Debug.Log("Class created successfully!");
                //SceneManager.LoadScene(0);
            }
            else
            {
                Debug.Log("User failed. Error #" + www.text);
            }
            viewModel.UpdateList();
        }

        IEnumerator Delete()
        {
            //selectedID;

            WWWForm form = new WWWForm();
            form.AddField("id", selectedID);

            //WWW www = new WWW("http://localhost/sqlconnect/deletecourse.php", form);
            WWW www = new WWW("https://ualrhomeworkwebserver.000webhostapp.com/unity_access/deletecourse.php", form);
            yield return www;

            if (www.text == "0")
            {
                //I will make sure the data has changed here
                Debug.Log("Good shit");
            }
            else
            {
                Debug.Log(www.text.ToString());
            }
            viewModel.UpdateList();
        }

        public void VerifyInputs()
        {
            saveNewCourseButton.interactable = ((courseTitleField.text.Length >= 1) && (courseNumberField.text.Length >= 1));
        }

        public void GoToMainMenu()
        {
            SceneManager.LoadScene(0); //Goes to the main menu scene
        }
    }
}

