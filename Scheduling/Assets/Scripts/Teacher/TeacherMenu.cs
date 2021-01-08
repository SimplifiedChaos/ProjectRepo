using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



//Scene 0 = Home Menu
//Scene 1 = Class Menu
//Scene 2 = Teacher Menu

namespace Oof{
    public class TeacherMenu : MonoBehaviour
    {
        public TeacherViewModel viewModel;


        
        public InputField lastNameField; //Teachers last name input field on UI
        public Slider maxCourseSlider; //Teacher maxcourses slider on UI

        public Toggle progC; //All the various toggles need to be declared so we can access them from the scripts
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
        public Toggle[] toggles;

        public Button saveNewTeacherButton; //Saving new teacher
        public Button editTeacherButton; //Saving the edited teacher
        public Button deleteTeacherButton; //Delete selected teacher
        public Button deSelect; //De select teacher so you can add a new one instead of editing the selected one

        public Toggle teacherSelected; //     T/F if there is a teacher selected
        public int selectedID; //Keep track of selected teacher so we can send this to the db for editing/deleting



        public bool[] disciplines = new bool[13]; //13 bool array so we can keep track of all the discipline toggles in one single array
        public void ChangeToggles(int num) //Change the toggle at that index in the array so we can keep track
        {
            disciplines[num] = !disciplines[num];
        }

        void Start() //Instantiate the array of toggles first thing
        {
            toggles = new Toggle[]{progC, progP, gameDev, DSaA, compOrg, opSys, progLang, cyberSec, mobApps, AI, networks, ToC, PaDS};
        }

        
        

        // TODO: populate the rest of the fields. 
        public void TeacherSelected(Teacher teacher)
        {
            teacherSelected.GetComponent<Toggle>().isOn = true;
            saveNewTeacherButton.GetComponent<Button>().interactable = false; //This section here states that if there is a teacher selected, we are only allowed to delete selected or edit selected
            editTeacherButton.GetComponent<Button>().interactable = true;
            deleteTeacherButton.GetComponent<Button>().interactable = true;
            deSelect.GetComponent<Button>().interactable = true;

            //teacherSelected.GetComponentsInChildren<Text>().text = "Teacher Selected  ID:"+ teacher.getID().ToString();

            ClearFields();
            selectedID = teacher.getID();
            Debug.Log(selectedID);

            lastNameField.text = teacher.name;
            maxCourseSlider.value = teacher.maxClass;
            
            foreach(Dicipline dis in teacher.diciplines)
            {
                toggles[(int)dis].isOn = true;
            }
            
            if(teacher.name.Contains("Dr."))
            {
                deSelect.GetComponent<TurnRainbow>().setTrue();
            }

        }

        // TODO: clear fields.
        public void Deselect() //Undo the previous function, unselect teacher so we can add new one
        {
            teacherSelected.GetComponent<Toggle>().isOn = false;
            saveNewTeacherButton.GetComponent<Button>().interactable = true;
            editTeacherButton.GetComponent<Button>().interactable = false;
            deleteTeacherButton.GetComponent<Button>().interactable = false;
            deSelect.GetComponent<Button>().interactable = false;

            
            //teacherSelected.GetComponentsInChildren<Text>().text = "Teacher Selected  ID:";
            ClearFields();
            

        }
        //TODO
        public void ClearFields()
        {
            deSelect.GetComponent<TurnRainbow>().setFalse();
            lastNameField.text = "";
            maxCourseSlider.value = 4;
            foreach(Toggle ds in toggles)
            {
                ds.isOn = false;
            }
        }


        public void CallSaveNewTeacher()
        {
            StartCoroutine(SaveNewTeacher());
        }

        IEnumerator SaveNewTeacher()
        {

            string serial = ""; //Create a string to add to so we can send that string to the database
            //This is where i serialize the bool array into a string of chars
            for (int i = 0; i<toggles.Length; i++)
            {
                if (toggles[i].isOn)
                {
                    serial += "y"; //If the toggle is on, add a Y to the disciplines string
                }
                else
                {
                    serial += "n"; //if the toggle is off, add a N to the disciplines string
                }
                Debug.LogFormat("Toggle: {0}", serial.ToString());
            }



            WWWForm form = new WWWForm(); //Prepare a form sending {lastName, maxCourses, disciplines} to the database
            form.AddField("lastName", lastNameField.text);
            form.AddField("maxCourses", maxCourseSlider.value.ToString());
            form.AddField("disciplines", serial);

            WWW www = new WWW("https://ualrhomeworkwebserver.000webhostapp.com/unity_access/teacher.php", form);
            //WWW www = new WWW("http://localhost/sqlconnect/teacher.php", form); //Josh: If you can get this to access teacher.php, not as a URL but as a file path.. teacher.php will be in the same directory as the index.html that calls this program.. Access that and we got it
            yield return www; //wait for the form from PHP to get sent back before continuing on

            if (www.text == "0") //if the php form echos 0 back, we know we did it right
            {
                Debug.Log("Teacher created successfully!");
            }
            else
            {
                Debug.Log("User failed. Error #" + www.text); //otherwise, send back the error message from php
            }
            viewModel.UpdateList(); //update the list on the left side after we do the database manipulation
        }


        public void CallEdit() //Start edit coroutine (basically opens a new thread for that method to run on, so our app doesnt pause while waiting on retrieval from DB
        {
            CallDelete();
            CallSaveNewTeacher();
        }


        IEnumerator Edit() //This is the edit coroutine
        {
            string serial = ""; //Same serialization as above, getting toggles ready to go
            //This is where i serialize the bool array into a string of chars
            for (int i = 0; i<toggles.Length; i++)
            {
                if (toggles[i].isOn)
                {
                    serial += "y";
                }
                else
                {
                    serial += "n";
                }
                Debug.LogFormat("Toggle: {0}", serial.ToString());
            }


            WWWForm form = new WWWForm(); //send a form consisting of {id, lastName, maxCourses, disciplines} to the DB, we use php file editteacher which will find and replace the ID value with new values
            form.AddField("id", selectedID);
            form.AddField("lastName", lastNameField.text);
            form.AddField("maxCourses", maxCourseSlider.value.ToString());
            form.AddField("disciplines", serial);

            //WWW www = new WWW("http://localhost/sqlconnect/editteacher.php", form);
            WWW www = new WWW("https://ualrhomeworkwebserver.000webhostapp.com/unity_access/editteacher.php", form);
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

        
        public void CallDelete()
        {
            StartCoroutine(Delete());
        }

        // TODO
        IEnumerator Delete()
        {
            //selectedID;
            //Debug.Log("Beginning delete");
            //Debug.Log(selectedID);


            WWWForm form = new WWWForm();
            form.AddField("id", selectedID);

            //WWW www = new WWW("http://localhost/sqlconnect/deleteteachers.php", form);
            WWW www = new WWW("https://ualrhomeworkwebserver.000webhostapp.com/unity_access/deleteteachers.php", form);
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
            saveNewTeacherButton.interactable = (lastNameField.text.Length >= 1);
        }

        public void GoToMainMenu()
        {
            SceneManager.LoadScene(0); //Goes to the main menu scene
        }

    }
}



// public void SaveNewTeacher()
        // {
        //     string serial = "";
        //     //This is where i serialize the bool array into a string of chars
        //     for (int i = 0; i < 13; i++)
        //     {
        //         if (disciplines[i] == true)
        //         {
        //             serial += "y";
        //         }
        //         else
        //         {
        //             serial += "n";
        //         }
        //         Debug.Log(serial.ToString());
        //     }

        //     viewModel.addTeacher(lastNameField.text, maxCourseSlider.value.ToString(), serial);
        // }
