using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadCube : MonoBehaviour
{
    public Transform t_up;
    public Transform t_down;
    public Transform t_left;
    public Transform t_right;
    public Transform t_front;
    public Transform t_back;

    private List<GameObject> front_rays = new List<GameObject>();
    private List<GameObject> back_rays = new List<GameObject>();
    private List<GameObject> left_rays = new List<GameObject>();
    private List<GameObject> right_rays = new List<GameObject>();
    private List<GameObject> up_rays = new List<GameObject>();
    private List<GameObject> down_rays = new List<GameObject>();


    //As faces have layer, can assign a layer mask
    private int layer_mask = 1 << 8; //for faces of cube only
    CubeState cubeState;
    CubeMap cubeMap;
    public GameObject emptyGO;

    // Start is called before the first frame update
    void Start()
    {
        setRayTransforms();

        cubeState = FindObjectOfType<CubeState>();
        cubeMap = FindObjectOfType<CubeMap>();

        ReadState();
        CubeState.started = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReadState()
    {
        cubeState = FindObjectOfType<CubeState>();
        cubeMap = FindObjectOfType<CubeMap>();

        //set state of position in the list so color is known for each position
        cubeState.up = ReadFace(up_rays, t_up);
        cubeState.down = ReadFace(down_rays, t_down);
        cubeState.front = ReadFace(front_rays, t_front);
        cubeState.back = ReadFace(back_rays, t_back);
        cubeState.right = ReadFace(right_rays, t_right);
        cubeState.left = ReadFace(left_rays, t_left);

        //update map with the new found positions
        cubeMap.Set();

    }

    void setRayTransforms()
    {
        //populate ray lists with raycasts starting from transform angled towards the cube
        up_rays = BuildRays(t_up, new Vector3(90,90,0));
        down_rays = BuildRays(t_down, new Vector3(270,90,0));
        left_rays = BuildRays(t_left, new Vector3(0,180,0));
        right_rays = BuildRays(t_right, new Vector3(0,0,0));
        front_rays = BuildRays(t_front, new Vector3(0,90,0));
        back_rays = BuildRays(t_back, new Vector3(0,-90,0));

    }

    List<GameObject> BuildRays(Transform ray_transform, Vector3 direction)
    {
        //raycount to name rays in order
        int ray_count = 0;
        List<GameObject> rays = new List<GameObject>();

        //create 9 rays for each cubie in a face
        // |0|1|2|
        // |3|4|5|
        // |6|7|8|
        // origin at 4, x = +1 at 5, y = +1 at 1

        for (int y = 1; y > -2; y--)
        {
            for (int x = -1; x < 2; x++)
            {
                //Vector3 start_pos = ray_transform.localPosition + new Vector3(x,y,0); // add shift to localPosition vector

                Vector3 start_pos = new Vector3(ray_transform.localPosition.x + x,
                                    ray_transform.localPosition.y + y,
                                    ray_transform.localPosition.z); // add shift to localPosition vector

                GameObject ray_start = Instantiate(emptyGO, start_pos, Quaternion.identity, ray_transform);
                ray_start.name = ray_count.ToString();
                rays.Add(ray_start);
                ray_count++;
            }
        }
        ray_transform.localRotation = Quaternion.Euler(direction);
        return rays;
    }

    public List<GameObject> ReadFace(List<GameObject> ray_starts, Transform ray_transform)
    {
        List<GameObject> faces_hit = new List<GameObject>();

        foreach (GameObject ray_start in ray_starts)
        {
            Vector3 ray = ray_start.transform.position;
            RaycastHit hit;

            //Does ray intersect any objects in the layer mask
            if (Physics.Raycast(ray, ray_transform.forward, out hit, Mathf.Infinity, layer_mask))
            {
                Debug.DrawRay(ray, ray_transform.forward * hit.distance, Color.yellow);
                faces_hit.Add(hit.collider.gameObject);
                //print(hit.collider.gameObject.name);
            } else 
            {
                Debug.DrawRay(ray, ray_transform.forward * 1000, Color.green);
            }
        }
        return faces_hit;
    }
}
