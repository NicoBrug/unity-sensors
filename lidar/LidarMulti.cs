using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LidarMulti : MonoBehaviour
{

    private System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

    void Start()
    {
        stopwatch.Reset();
        stopwatch.Start();
        RaycasExample();
        stopwatch.Stop();
        Debug.Log(" Temps de la fonction = " + (stopwatch.ElapsedTicks / 10000.0) + "ms");
    }


    private void RaycasExample()
    {
        // Perform a single raycast using RaycastCommand and wait for it to complete
        // Setup the command and result buffers


        var results = new NativeArray<RaycastHit>(1000000, Allocator.TempJob);

        var commands = new NativeArray<RaycastCommand>(1000000, Allocator.TempJob);

        // Set the data of the first command
        Vector3 origin = Vector3.forward * -10;

        Vector3 direction = Vector3.forward;

        for (int i=0; i < 1000000; i++)
        {
            commands[i] = new RaycastCommand(origin, direction);
        }
      

        // Schedule the batch of raycasts
        JobHandle handle = RaycastCommand.ScheduleBatch(commands, results, 1, default(JobHandle));

        // Wait for the batch processing job to complete
        handle.Complete();

        // Copy the result. If batchedHit.collider is null there was no hit
        RaycastHit[] batchedHit = results.ToArray();

        // Dispose the buffers
        results.Dispose();
        commands.Dispose();
    }
}