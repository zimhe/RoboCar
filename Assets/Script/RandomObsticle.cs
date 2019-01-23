using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObsticle : MonoBehaviour {

    [SerializeField] float radius = 50f;
    [SerializeField] Transform ObstaclePrefab;
    [SerializeField] float maxSize = 5f;
    [SerializeField] float minSize = 1f;
    [SerializeField] int ObstacleNumber = 50;

    List<Vector3> positions;
    List<Vector3> scales;

    List<Transform> obstacles;

	// Use this for initialization
	void Start ()
    {
        CreateObstacle();
	}

    void CreateObstacle()
    {
        positions = new List<Vector3>();
        scales = new List<Vector3>();
        obstacles = new List<Transform>();

        for (int i = 0; i < ObstacleNumber; i++)
        {
            var px = Random.Range(-radius, radius);
            var pz = Random.Range(-radius, radius);

            var p = new Vector3(px, 0f, pz);

            var sx = Random.Range(minSize, maxSize);
            var sy = Random.Range(minSize, maxSize);
            var sz = Random.Range(minSize, maxSize);

            var s = new Vector3(sx, sy, sz);

            p.y = 0.5f * sy;

            positions.Add(p);
            scales.Add(s);
        }

        foreach (var p in positions)
        {
            int i = positions.IndexOf(p);

            var o = Instantiate(ObstaclePrefab, transform);
            o.localPosition = p;
            o.localScale = scales[i];
        }


    }

    // Update is called once per frame
    void Update () {
		
	}
}
