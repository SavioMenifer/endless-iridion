using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelGenerator : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject tunnelModulePrefab;
    public GameObject gameManager;
    public Vector3 spawnPosition;
    public int concurrentModules = 5;

    public int lastModuleIndex = 0;

    List<GameObject> spawnedModules = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < concurrentModules; i++)
        {
            spawnPosition += new Vector3(0f, 0f, 3.6f);

            GameObject spawnedModule = Instantiate(
                tunnelModulePrefab,
                spawnPosition,
                Quaternion.identity
            );

            spawnedModule.transform.SetParent(transform);
            spawnedModules.Add(spawnedModule);
        }
    }

    // bring tunnel module that player has passed to the front to create endless tunnel
    public void MoveLastModuleForward()
    {
        spawnedModules[lastModuleIndex].transform.position +=
            transform.forward * concurrentModules * 3.6f;
        lastModuleIndex++;
        //Debug.Log(lastModuleIndex);
        if (lastModuleIndex >= concurrentModules)
            lastModuleIndex = 0;
    }

    // generate int[36] array representation of hexagonal tunnel made where:
    // 0 - normal tile
    // 1 - spikes
    // 2 - orbs
    private int[] GenerateTunnelData(int difficulty)
    {
        int[] tunnelData = new int[36]; // initialise array with all zeros

        int placedSpikes = 0;
        int placedOrbs = 0;

        while (placedSpikes < difficulty)
        {
            int potentialSpikePosition = Random.Range(0, 35);
            if (
                tunnelData[(potentialSpikePosition - 1) % 36] != 1
                && tunnelData[(potentialSpikePosition + 1) % 36] != 1
            )
            {
                tunnelData[potentialSpikePosition] = 1;
                placedSpikes++;
            }
        }

        while (placedOrbs < 3)
        {
            int potentialOrbPosition = Random.Range(0, 35);
            if (
                tunnelData[(potentialOrbPosition - 1) % 36] != 2
                && tunnelData[(potentialOrbPosition + 1) % 36] != 2
                && tunnelData[potentialOrbPosition] != 1
            )
            {
                tunnelData[potentialOrbPosition] = 2;
                placedOrbs++;
            }
        }

        return tunnelData;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(lastModuleIndex);
        if (
            mainCamera.WorldToViewportPoint(spawnedModules[lastModuleIndex].transform.position).z
            < -3.6f
        )
        {
            spawnedModules[lastModuleIndex].GetComponent<TunnelPrefabManager>().tunnelData =
                GenerateTunnelData(gameManager.GetComponent<GameManager>().difficulty);
            spawnedModules[lastModuleIndex].GetComponent<TunnelPrefabManager>().InitTunnel();
            MoveLastModuleForward();
        }
    }
}
