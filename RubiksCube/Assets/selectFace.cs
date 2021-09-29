using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectFace : MonoBehaviour
{
    CubeState cubeState;
    ReadCube readCube;
    int layer_mask = 1 << 8;
    // Start is called before the first frame update
    void Start()
    {
        //intialise
        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();
    }

    // Update is called once per frame
    void Update()
    {
        //only accept manual input if cube not auto-rotating
        if (Input.GetMouseButtonDown(0) && !CubeState.auto_rotating)
        {
            //read cube state
            readCube.ReadState();

            //raycast from mouse to cube
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f, layer_mask))
            {
                GameObject face = hit.collider.gameObject;

                //list of all sides (where sides are lists of gameobjects)
                List<List<GameObject>> cube_sides = new List<List<GameObject>>()
                {
                    cubeState.up,
                    cubeState.down,
                    cubeState.left,
                    cubeState.right,
                    cubeState.front,
                    cubeState.back
                };

                //if the face (gameObject) hit exists whithin a side
                foreach (List<GameObject> cube_side in cube_sides)
                {
                    if (cube_side.Contains(face))
                    {
                        //make side piece the children of central piece
                        cubeState.pickUp(cube_side);
                        //start the side rotation logic
                        cube_side[4].transform.parent.GetComponent<PivotRotation>().Rotate(cube_side);
                    }
                }

            }
        }
    }
}
