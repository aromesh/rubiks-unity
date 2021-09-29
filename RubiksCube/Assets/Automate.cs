using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automate : MonoBehaviour
{
    public static List<string> move_list = new List<string>() {};
    private readonly List<string> all_moves = new List<string>()
        {
            "U", "D", "L", "R", "F", "B",
            "U'", "D'", "L'", "R'", "F'", "B'",
            "U2", "D2", "L2", "R2", "F2", "B2"
        };

    private CubeState cube_state;
    private ReadCube read_cube;

    // Start is called before the first frame update
    void Start()
    {
        cube_state = FindObjectOfType<CubeState>();
        read_cube = FindObjectOfType<ReadCube>();

    }

    // Update is called once per frame
    void Update()
    {
        if (move_list.Count > 0 && !CubeState.auto_rotating && CubeState.started)
        {
            //Do the move at first index
            doMove(move_list[0]);
            //remove the move at first index
            move_list.Remove(move_list[0]);

        }
        
    }

    void RotateSide(List<GameObject> side, float angle)
    {
        //auto rotate the side by the specified angle
        //grab pivot rotation script of the center piece and side of rotation
        PivotRotation pr = side[4].transform.parent.GetComponent<PivotRotation>();
        pr.startAutoRotate(side, angle);
    }

    //to generate a random list of shuffled moves
    public void shuffle()
    {
        List<string> moves = new List<string>();
        int shuffle_length = Random.Range(10, 30);
        for (int i = 0; i < shuffle_length; i++)
        {
            //create random move from list of available moves
            int random_move = Random.Range(0, all_moves.Count);
            moves.Add(all_moves[random_move]);
        }
        // TODO
        //might be able to do by reference
        move_list = moves;

        // DEBUGGING
        debugMoveList();
    }

    //debugging to print movelist
    void debugMoveList()
    {
        string temp = "";
        foreach(string str in move_list)
        {
            temp += " " + str;
        }
        print(temp);
    }

    void doMove(string move)
    {
        //get state of cube before each move
        read_cube.ReadState();
        //set global auto-rotate to true in CubeState
        CubeState.auto_rotating = true;

        // TODO
        // These if conditions are a mess (clean this up)

        // U
        if (move == "U")
        {
            RotateSide(cube_state.up, -90);
        }
        if (move == "U'")
        {
            RotateSide(cube_state.up, +90);
        }
        if (move == "U2")
        {
            RotateSide(cube_state.up, -180);
        }

        // D
        if (move == "D")
        {
            RotateSide(cube_state.down, -90);
        }
        if (move == "D'")
        {
            RotateSide(cube_state.down, +90);
        }
        if (move == "D2")
        {
            RotateSide(cube_state.down, -180);
        }

        // R
        if (move == "R")
        {
            RotateSide(cube_state.right, -90);
        }
        if (move == "R'")
        {
            RotateSide(cube_state.right, +90);
        }
        if (move == "R2")
        {
            RotateSide(cube_state.right, -180);
        }

        // L
        if (move == "L")
        {
            RotateSide(cube_state.left, -90);
        }
        if (move == "L'")
        {
            RotateSide(cube_state.left, +90);
        }
        if (move == "L2")
        {
            RotateSide(cube_state.left, -180);
        }

        // F
        if (move == "F")
        {
            RotateSide(cube_state.front, -90);
        }
        if (move == "F'")
        {
            RotateSide(cube_state.front, +90);
        }
        if (move == "F2")
        {
            RotateSide(cube_state.front, -180);
        }

        // B
        if (move == "B")
        {
            RotateSide(cube_state.back, -90);
        }
        if (move == "B'")
        {
            RotateSide(cube_state.back, +90);
        }
        if (move == "B2")
        {
            RotateSide(cube_state.back, -180);
        }
    }
}
