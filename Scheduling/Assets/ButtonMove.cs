using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMove : MonoBehaviour
{
    private void Update()
    {
        if(Vector3.Distance(Input.mousePosition, transform.position) < 135)
        {
            transform.position = Vector2.MoveTowards(transform.position, Input.mousePosition, -1 * 1000 * Time.deltaTime);
        }

    }
}
