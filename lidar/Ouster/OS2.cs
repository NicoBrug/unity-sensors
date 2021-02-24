using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class OS2 : Ouster
{


    private RaycastConf configurationRay;

    private float V_Res;
    private float H_Res;

    private Channels VR;

    void Start()
    {
        layerMask = 1 << 8;
        layerMask = ~layerMask;

        configurationRay = new RaycastConf();
        configurationRay.Distance = 240;
        configurationRay.VerticalResolution = 32;
        configurationRay.HorizontalResolution = 512;

        configurationRay.VerticalFOV = 0.3839724f;
        configurationRay.HorizontalFOV = Mathf.PI * 2.0f;
        configurationRay.radius = 1.0f;
        configurationRay.StartVerticalAngle = 0.0f;

        numberOfParticles = (int)(configurationRay.VerticalResolution * configurationRay.HorizontalResolution);
        ParticleBuffer = new ComputeBuffer(numberOfParticles, sizeof(float) * 8);

      
    }

    void Update()
    {

        switch (VerticalResolution)
        {
            case Channels.Channel_32:
                configurationRay.VerticalResolution = 32;
                break;
            case Channels.Channel_64:
                configurationRay.VerticalResolution = 64;
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

        numberOfParticles = (int)(configurationRay.VerticalResolution * configurationRay.HorizontalResolution);
        ParticleBuffer = new ComputeBuffer(numberOfParticles, sizeof(float) * 8);

        List<ParticleData> particledata = Raycast(configurationRay, transform.position);
        ParticleBuffer.SetData(particledata);

    }
}