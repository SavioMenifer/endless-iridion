using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelPrefabManager : MonoBehaviour
{
    public int[] tunnelData = new int[36];
    public GameObject spikeTile;
    public GameObject orbPrefab;

    void Start()
    {
        InitTunnel();
    }

    public void InitTunnel()
    {
        for (int i = 0; i < 36; i++)
        {
            Transform t = transform.GetChild(i).gameObject.transform;

            if (tunnelData[i] == 1)
            {
                Destroy(transform.GetChild(i).gameObject);
                GameObject tile = Instantiate(spikeTile, t.position, t.rotation);
                tile.transform.SetParent(transform);
            }
            else if (tunnelData[i] == 2)
            {
                GameObject orb = Instantiate(orbPrefab, t.position, t.rotation);
                orb.transform.SetParent(t);
                orb.transform.localPosition += transform.forward * 15f;
            }
        }
    }
}
