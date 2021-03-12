using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class OS0 : Ouster
{
    

    private RaycastConf configurationRay;

    private float V_Res;
    private float H_Res;

    private Channels VR;
    private System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

    void Start()
    {
        layerMask = 1 << 8;
        layerMask = ~layerMask;

        configurationRay = new RaycastConf();
        configurationRay.Distance = 50.0f;
        configurationRay.VerticalResolution = 32;
        configurationRay.HorizontalResolution = 512;

        configurationRay.VerticalFOV = 1.5708f;
        configurationRay.HorizontalFOV = 3.14f * 2.0f;
        configurationRay.radius = 1.0f;
        configurationRay.StartVerticalAngle = 0.0f;

        numberOfParticles = (int)(configurationRay.VerticalResolution * configurationRay.HorizontalResolution);
        ParticleBuffer = new ComputeBuffer(numberOfParticles, sizeof(float) * 8);

    }
    private void OnRenderObject()
    {
        //Set shader parameter /!\ -> Transorm inutile because lidar point are already in world matrix
        mt.SetPass(0);

        mt.SetBuffer("_ParticleDataBuff", ParticleBuffer);
        mt.SetFloat("_ParticleSize", SizeParticle);

        Graphics.DrawProceduralNow(MeshTopology.Triangles, 3 * 4, numberOfParticles);
    }

    void Update()
    {


        switch (VerticalResolution)
        {
            case Channels.Channel_32:
                configurationRay.VerticalResolution = 32;
                break;
            case Channels.Channel_64:
                configurationRay.VerticalResolution = 40;
                break;
            case Channels.Channel_128:
                configurationRay.VerticalResolution = 128;
                break;
        }

        switch (HorizontalResolution)
        {
            case HR.HR_512:
                configurationRay.HorizontalResolution = 512;
                break;
            case HR.HR_1024:
                configurationRay.HorizontalResolution = 1024;
                break;
            case HR.HR_2048:
                configurationRay.HorizontalResolution = 2048;
                break;
        }

        stopwatch.Reset();
        stopwatch.Start();
        List<ParticleData> particledata = Raycast(configurationRay, transform.position, transform.eulerAngles);
        //-Mathf.Deg2Rad * this.transform.eulerAngles.y
        stopwatch.Stop();
        Debug.Log(" Temps de la fonction = " + (stopwatch.ElapsedTicks / 10000.0) + "ms");


        numberOfParticles = (int)(configurationRay.VerticalResolution * configurationRay.HorizontalResolution);
        ParticleBuffer = new ComputeBuffer(numberOfParticles, sizeof(float) * 8);
        ParticleBuffer.SetData(particledata);

    }
}