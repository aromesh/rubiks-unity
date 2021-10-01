using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Keeps record of current sides of the cube
public class CubeState : MonoBehaviour
{
    // sides
    public List<GameObject> front = new List<GameObject>();
    public List<GameObject> back = new  List<GameObject>();
    public List<GameObject> up = new  List<GameObject>(); 
    public List<GameObject> down = new  List<GameObject>();
    public List<GameObject> left = new  List<GameObject>();
    public List<GameObject> right = new  List<GameObject>();

    public static bool auto_rotating = false;
    public static bool started = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pickUp(List<GameObject> cube_side)
    {
        foreach(GameObject face in cube_side)
        {
            //attach parent of each face to parent of middle cube (index 4) unless it is index 4
            if (face != cube_side[4])
            {
                face.transform.parent.transform.parent = cube_side[4].transform.parent;
            }
        }
    }

    public void putDown(List<GameObject> little_cubes, Transform pivot)
    {
        foreach (GameObject little_cube in little_cubes)
        {
            if (little_cube != little_cubes[4])
            {
                little_cube.transform.parent.transform.parent = pivot;
            }
        }
    }

    string getSideString(List<GameObject> side)
    {
        string side_string = "";
        foreach(GameObject face in side)
        {
            //first letter
            side_string += face.name[0];
        }
        return side_string;
    }

    public string getStateString()
    {
        string state_string = "";
        state_string += getSideString(up);
        state_string += getSideString(right);
        state_string += getSideString(front);
        state_string += getSideString(down);
        state_string += getSideString(left);
        state_string += getSideString(back);

        return state_string;
    }
}
