using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour {

    public WorldTerrain Prefab;
    Mesh worldMesh;

	// Use this for initialization
	void Start () {
        var map = new Simulation.HexMap(5);
        var worldTerrain = Instantiate(Prefab);
        worldMesh = map.GetBasicMesh();
        worldTerrain.GetComponent<MeshFilter>().mesh = worldMesh;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
