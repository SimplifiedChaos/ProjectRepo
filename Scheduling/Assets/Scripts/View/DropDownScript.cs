using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Oof
{
    

public class DropDownScript : MonoBehaviour
{
   public Dropdown dropdown;
   public ViewSolution viewSolution;
   public List<string> names;

   int c;

   void Start()
   {
       names = new List<string>();
   }

//    void PopulateList()
//    {
//        List<string> names = new List<string>() {"Fred", "Bunny", "Sue"};
//        dropdown.options = new List<Dropdown.OptionData>();
//        dropdown.AddOptions(names);

//    }

   public void Dropdown_IndexChanged(int index)
   {
       string name = names[index];
       viewSolution.ButtonClicked(name);
       
   }

   public void UpdateList(List<Solution> solutions)
   {
       names = new List<string>();
       dropdown.options = new List<Dropdown.OptionData>();
       foreach(Solution sol in solutions)
       {
           names.Add(sol.name);
       }
       dropdown.AddOptions(names);
       Dropdown_IndexChanged(0);
   }
}

}
