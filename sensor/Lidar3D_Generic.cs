using System.Collections;
using System.Collections.Generic;
using UnityEngine;






public class Lidar3D_Generic : MonoBehaviour
{

    struct DataLidar
    {
        public string sensor;
        public string type;
        public List<float> data;
    }

    struct ParticleData
    {
        public Vector4 Position;
        public Vector4 Color;
    }

    public float Channel;
    public float Range;
    public float HorizontalResolution;
    public float Horizontal_FOV;
    public float Vertical_FOV;
    public bool ActiveDebugRay;
    public Material mt; //material for shader visualisation --> Create new material, assign Unlite/Point & select material in mt 



    private Vector3 initPosition;
    private Vector3 initDirection;
    private int layerMask;
    private ComputeBuffer ParticleBuffer;
    private int numberOfParticles;



    void Start()
    {
        layerMask = 1 << 8;
        layerMask = ~layerMask;

        initPosition = transform.position;
        initDirection = Vector3.forward;
        ActiveDebugRay = false;

        /*----- Init SPECIFICATION -> Please refer to the manufacturer's specifications -----*/
        Channel = 32;
        Range = 200;
        HorizontalResolution = 100;
        Horizontal_FOV = 2 * 3.14f;
        Vertical_FOV = 1.22173f;

        numberOfParticles = (int)(Channel * HorizontalResolution);
        ParticleBuffer = new ComputeBuffer(numberOfParticles, sizeof(float) * 8);
    }
    
    private void OnRenderObject()
    {
        //Set shader parameter /!\ -> Transorm inutile because lidar point are already in world matrix
        mt.SetPass(0);

        mt.SetBuffer("_ParticleDataBuff", ParticleBuffer);
        mt.SetFloat("_ParticleSize", 0.1f);

        Graphics.DrawProceduralNow(MeshTopology.Triangles, 3 * 4, numberOfParticles);
    }


    void FixedUpdate()
    {
        Raycast();
    }


    void Raycast()
    {
        RaycastHit hit;
        List<ParticleData> particle_list = new List<ParticleData>(); ;
   
        //config basis
        float radius = 1.0f;
        float angleHorizontal = 0;
        float step = Horizontal_FOV / HorizontalResolution;
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
                    ParticleData p = new ParticleData();
                    Vector4 position = new Vector4(hit.point.x, hit.point.y, hit.point.z, 10.0f);
                    Vector4 color = new Color(0.8f, 0.0f, 0.219f);
                    p.Position = position;
                    p.Color = color;
                    particle_list.Add(p);
                }
                else
                {
                    if (ActiveDebugRay)
                    {
                        Debug.DrawRay(transform.position, vDirection * Range, Color.yellow);
                    }
                    ParticleData p = new ParticleData();
                    Vector4 position = new Vector4(0.0f, 0.0f, 0.0f, 0.0f); //Create particle in 0,0,0 for respect data list lenght
                    Vector4 color = new Color(0.8f, 0.0f, 0.219f);
                    p.Position = position;
                    p.Color = color;
                    particle_list.Add(p);
                }

                //add angle step to horizontal ray
                angleVertical += stepVertical;
                index += 1;

            };
            angleHorizontal += step;
        };
        ParticleBuffer.SetData(particle_list);

    }

    

    private void OnDestroy()
    {
        ParticleBuffer.Release();
    }



    ////yaw = vertical, pitch = horizontal
    //private void getXYZ(float pitch, float yaw, float distance)
    //{
    //    float x = distance * Mathf.Sin(yaw) * Mathf.Cos(pitch);
    //    float z = distance * Mathf.Sin(pitch);
    //    float y = distance * Mathf.Cos(yaw) * Mathf.Sin(pitch);

    //    Vector3 position = new Vector3(x, y, z);

    //    Debug.Log("x" + x + " y" + y + " z" + z);

    //}

}



