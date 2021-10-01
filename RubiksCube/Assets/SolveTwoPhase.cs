using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kociemba;

public class SolveTwoPhase : MonoBehaviour
{
    public ReadCube readCube;
    public CubeState cubeState;

    private bool do_once = true;
    // Start is called before the first frame update
    void Start()
    {
        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CubeState.started && do_once)
        {
            do_once = false;
            Solver();
        }
    }

    public void Solver()
    {
        readCube.ReadState();

        // get cube state as a string
        string move_string = cubeState.getStateString();
        print(move_string);
        // solve cube using package

        string info = "";

        // First time the package is called, build tables
        //string solution = Kociemba.SearchRunTime.solution(move_string, out info, buildTables: true);
        
        //Every other time
        string solution = Search.solution(move_string, out info);

        // convert solved moves from string to list of moves
        List<string> solution_list = stringToList(solution);

        // Automate List
        Automate.move_list = solution_list;
        print(info);
    }

    List<string> stringToList(string solution)
    {
        List<string> solution_list = new List<string>(solution.Split(new string[] {" "}, System.StringSplitOptions.RemoveEmptyEntries));

        return solution_list;
    }
}
