using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotRotation : MonoBehaviour
{
    private List<GameObject> active_side;
    private Vector3 local_forward;
    private Vector3 mouse_reference;
    private bool mouse_drag = false;
    private bool auto_rotating = false;
    private float sensitivity = 0.4f;    //rotation sensitivity
    private Vector3 rotation;    
    private float rotate_speed = 300f;
    private Quaternion target_quaternion;

    private ReadCube read_cube;
    private CubeState cube_state;
    // Start is called before the first frame update
    void Start()
    {
        read_cube = FindObjectOfType<ReadCube>();
        cube_state = FindObjectOfType<CubeState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mouse_drag)
        {
            spinSide(active_side);

            if (Input.GetMouseButtonUp(0))
            {
                mouse_drag = false;
                rotateToRightAngle();
            }
        }
        if (auto_rotating)
        {
            autoRotate();
        }
    }


    private void spinSide(List<GameObject> side)
    {
        //reset rotation
        rotation = Vector3.zero;

        //current mouse pos - last mouse pos
        Vector3 mouse_offset = Input.mousePosition - mouse_reference;

        //rotation logic
        if (side == cube_state.up)
        {
            rotation.y = (mouse_offset.x + mouse_offset.y) * sensitivity * 1;
        }
        if (side == cube_state.down)
        {
            rotation.y = (mouse_offset.x + mouse_offset.y) * sensitivity * -1;
        }

        if (side == cube_state.left)
        {
            rotation.z = (mouse_offset.x + mouse_offset.y) * sensitivity * 1;
        }
        if (side == cube_state.right)
        {
            rotation.z = (mouse_offset.x + mouse_offset.y) * sensitivity * -1;
        }

        if (side == cube_state.front)
        {
            rotation.x = (mouse_offset.x + mouse_offset.y) * sensitivity * -1;
        }
        if (side == cube_state.back)
        {
            rotation.x = (mouse_offset.x + mouse_offset.y) * sensitivity * 1;
        }

        //rotate
        transform.Rotate(rotation, Space.Self);

        //store mouse reference
        mouse_reference = Input.mousePosition;
    }
    
    //side passed into this functioin becomes active side
    public void Rotate(List<GameObject> side)
    {
        active_side = side;
        mouse_reference = Input.mousePosition;
        mouse_drag = true;

        //rotation axis vector (points towards center)
        local_forward = -side[4].transform.parent.transform.localPosition;
    }

    public void rotateToRightAngle()
    {
        Vector3 vec = transform.localEulerAngles;

        //round to nearest 90 degrees
        vec.x = Mathf.Round(vec.x/90)*90;
        vec.y = Mathf.Round(vec.y/90)*90;
        vec.z = Mathf.Round(vec.z/90)*90;

        target_quaternion.eulerAngles = vec;
        auto_rotating = true;
    }

    private void autoRotate()
    {
        mouse_drag = false;
        var step = rotate_speed * Time.deltaTime;
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, target_quaternion, step);

        //if anngle difference < 1 degree, set target angle to finish rotation
        if (Quaternion.Angle(transform.localRotation, target_quaternion) <=1 )
        {
            transform.localRotation = target_quaternion;

            //unparent cubes
            cube_state.putDown(active_side, transform.parent);
            read_cube.ReadState();

            auto_rotating = false;
            mouse_drag = false;
        }
    }
}
