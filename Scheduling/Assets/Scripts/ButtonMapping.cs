using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonMapping : MonoBehaviour
{
    //Scene 0 = Home Menu
    //Scene 1 = Class Menu
    //Scene 2 = Teacher Menu


    public void GoToScene(int num)
    {
        SceneManager.LoadScene(num);
    }
}
