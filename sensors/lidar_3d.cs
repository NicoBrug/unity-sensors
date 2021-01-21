using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;


class DataLidar
{
    public string sensor;
    public string type;
    public List<float> data;
}


public class lidar_3d : MonoBehaviour
{
    public float Channel;

    public float Range;
    public float HorizontalResolution;

    public float Horizontal_FOV;
    public float Vertical_FOV;
    public bool ActiveDebugRay;

    private Vector3 initPosition;
    private Vector3 initDirection;
    private int layerMask;
    private UDP_client sockets;

   

    void Start()
    {
        layerMask = 1 << 8;
        layerMask = ~layerMask;
        /*----- Init vector & socket -----*/
        initPosition = transform.position;
        initDirection = Vector3.forward;
        ActiveDebugRay = false;

        sockets = new UDP_client();
        sockets.Setup();


        /*----- Init SPECIFICATION -> Please refer to the manufacturer's specifications -----*/
        Channel = 4;
        //Measurement Range
        Range = 200;
        //HorizontalRays
        HorizontalResolution = 5;
        //Horizontal Field of View
        Horizontal_FOV = 2 * 3.14f;
        //Vertical Field of View
        Vertical_FOV = 1.22173f;
    }


    void FixedUpdate()
    {
        List<float> data = Raycast();

        //get racast vector 
        DataLidar dataLidar = new DataLidar();
        dataLidar.sensor = "lidar3D";
        dataLidar.type = "point_of_cloud";
        dataLidar.data = data;

        //send to server
        sockets.sendData(JsonUtility.ToJson(dataLidar));
    }


    List<float> Raycast()
    {
        RaycastHit hit;

        //config basis
        float radius = 1.0f;
        float angleHorizontal = 0;
        float step = Horizontal_FOV / HorizontalResolution;
        //init vector data
        List<float> dataLidar = new List<float>();

        //init index for vector
        int index = 0;

        for (int i = 0; i < HorizontalResolution; ++i)
        {
            //get horizontal direction vector (x & z because in unity Z is not up vector)
            float direction_x = radius * Mathf.Cos(angleHorizontal);
            float direction_z = radius * Mathf.Sin(angleHorizontal);

            float angleVertical = -(Vertical_FOV / 2);
            float stepVertical = Vertical_FOV / Channel;

            for (int j = 0; j < Channel; ++j)
            {
                //get vertical direction 
                float direction_y = radius * Mathf.Sin(angleVertical);
                Vector3 vDirection = new Vector3(direction_x, direction_y, direction_z);

                //Did HIT ? 
                if (Physics.Raycast(transform.position, vDirection, out hit, Range, layerMask))
                {
                    if (ActiveDebugRay)
                    {
                        Debug.DrawRay(transform.position, vDirection * hit.distance, Color.red);
                    }
                    dataLidar.Add(hit.point.x);
                    dataLidar.Add(hit.point.y);
                    dataLidar.Add(hit.point.z);
                }
                else
                {
                    if (ActiveDebugRay)
                    {
                        Debug.DrawRay(transform.position, vDirection * Range, Color.yellow);
                    }
                    dataLidar.Add(0);
                    dataLidar.Add(0);
                    dataLidar.Add(0);
                }

                //add angle step to horizontal ray
                angleVertical += stepVertical;
                index += 1;
                
            };
            angleHorizontal += step;
        };
        return dataLidar;
    }

    //yaw = vertical, pitch = horizontal
    private void getXYZ(float pitch, float yaw, float distance)
    {
        float x = distance * Mathf.Sin(yaw) * Mathf.Cos(pitch);
        float z = distance * Mathf.Sin(pitch);
        float y = distance * Mathf.Cos(yaw) * Mathf.Sin(pitch);

        Vector3 position = new Vector3(x, y, z);

        Debug.Log("x"+x+" y"+y+" z"+z);
    
    }
}

    

