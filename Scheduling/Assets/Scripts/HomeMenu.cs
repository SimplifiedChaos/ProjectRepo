using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Oof
{
    
public class HomeMenu : MonoBehaviour
{

    //Scene 0 = Home Menu
    //Scene 1 = Class Menu
    //Scene 2 = Teacher Menu

    public void GoToClassScene()
    {
        SceneManager.LoadScene(1);//This is called on the home menu when you click the Add Class button, you go to the class scene
    }


    public void GoToTeacherScene()
    {
        SceneManager.LoadScene(2);//Same as other, except goes to the add teacher scene
    }
    

    public void GoToEditTeacherScene()
    {
        SceneManager.LoadScene(3);
    }

}
    //CHANGE SOME OF THIS STUFF SO WE CAN GO TO THE CORRECT SCENEEEESSSSS
}