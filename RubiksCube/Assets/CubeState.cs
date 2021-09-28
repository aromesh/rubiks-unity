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
        //start the side rotation logic
        cube_side[4].transform.parent.GetComponent<PivotRotation>().Rotate(cube_side);
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
}
