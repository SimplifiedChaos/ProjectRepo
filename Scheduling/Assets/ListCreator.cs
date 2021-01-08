using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListCreator : MonoBehaviour
{

    [SerializeField]
    private Transform SpawnPoint = null;

    [SerializeField]
    private GameObject item = null;

    [SerializeField]
    private RectTransform content = null;


    public int numberOfItems;


    public string[] teacherNames;
    public string[] IDs;

    // Use this for initialization
    void Start()
    {
        numberOfItems = 400; //MAKE SURE THIS VALUE CHANGES
        teacherNames = new string[numberOfItems];
        IDs = new string[numberOfItems];

        for (int i = 0; i < numberOfItems; i++)
        {
            teacherNames[i] = i.ToString();
            IDs[i] = i.ToString();
        }

        //setContent Holder Height;
        content.sizeDelta = new Vector2(0, numberOfItems * 30);
        

        for (int i = 0; i < numberOfItems; i++)
        {
            // 60 width of item
            float spawnY = i * 30;
            //newSpawn Position
            Vector3 pos = new Vector3(SpawnPoint.position.x, -spawnY, SpawnPoint.position.z);
            //instantiate item
            GameObject SpawnedItem = Instantiate(item, pos, SpawnPoint.rotation);
            //setParent
            SpawnedItem.transform.SetParent(SpawnPoint, false);
            //get ItemDetails Component
            ItemDetails itemDetails = SpawnedItem.GetComponent<ItemDetails>();
            //set name
            itemDetails.teacherName.text = teacherNames[i];
            //set image
            itemDetails.ID.text = IDs[i];


        }
    }
}