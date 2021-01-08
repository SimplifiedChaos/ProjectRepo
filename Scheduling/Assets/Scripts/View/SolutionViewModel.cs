using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oof
{

public class SolutionViewModel : MonoBehaviour
{
    public DropDownScript dropDownScript;

    public List<Solution> solutionList;
    
    int idSelected;

    void Start()
    {
        solutionList = new List<Solution>();
        UpdateList();
    }

    public int getID(string name)
    {
        Solution sol = solutionList.Find(e => e.name == name);
        return sol.getID();
    }

    public List<Solution> GetSolutionList()
    {
        return new List<Solution>(solutionList);
    }

    public Dictionary<int,int> GetMatches(int id)
    {
        Solution temp = solutionList.Find(item => item.getID() == id);
        Debug.LogFormat("id:{0} name:{1}", id, temp.name);
        Dictionary<int,int> tempDict =  temp.getDictionary();
        Debug.LogFormat("tempLength:{0}", tempDict.Count);
        return tempDict;
    }

    public List<Teacher> GetTeachers(int id)
    {
        Solution temp = solutionList.Find(item => item.getID() == id);
        Debug.LogFormat("id:{0} name:{1}", id, temp.name);
        List<Teacher> tempList = temp.getTeacherList();
        Debug.LogFormat("tempLength:{0}", tempList.Count);
        return tempList;
    }

    public List<Course> GetCourses(int id)
    {
        Solution temp = solutionList.Find(item => item.getID() == id);
        Debug.LogFormat("id:{0} name:{1}", id, temp.name);
        List<Course> tempList = temp.getCourseList();
        Debug.LogFormat("tempLength:{0}", tempList.Count);
        return tempList;
    }

    public void UpdateList()
    {
        CallPullAllSolutions();
    }

    IEnumerator PullAllSolutions()
    {
        Debug.Log("Start");
        WWWForm form = new WWWForm();
            //WWW www = new WWW("http://localhost/sqlconnect/pullsolutions.php", form);
            WWW www = new WWW("https://ualrhomeworkwebserver.000webhostapp.com/unity_access/pullsolutions.php", form);
            yield return www;
        Debug.Log("Yielded www form");
        Debug.Log(www.text.ToString());

        string strToSplit = www.text;
        char delimiterTab = '\t';
        string[] arrSplit = strToSplit.Split(delimiterTab);
        solutionList.Clear();
        Debug.Log("arrSplit:" + arrSplit.Length);
        for (int i = 0; i < arrSplit.Length - 1; i += 5)
        {
            Debug.Log(arrSplit[i]); //ID, name, Dictionary, Teachers, courses
            Solution tempSolution = new Solution(arrSplit[i], arrSplit[i + 1], arrSplit[i + 2], arrSplit[i + 3], arrSplit[i + 4]);
            solutionList.Add(tempSolution); 
            Debug.Log("00000000000000000000000000000000000000000000000000000000000000000000000000000000000");          
        }
        dropDownScript.UpdateList(solutionList);
        Debug.Log("buttonListUpdated?");
    }
    public void CallPullAllSolutions()
    {
        StartCoroutine(PullAllSolutions());
    }
}

}
