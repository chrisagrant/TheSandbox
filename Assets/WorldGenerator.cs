using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour {

    public WorldTerrain Prefab;
    Mesh worldMesh;

	// Use this for initialization
	void Start () {
        var worldTerrain = Instantiate(Prefab);
        worldMesh = new Mesh();
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
