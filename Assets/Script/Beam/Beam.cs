using UnityEngine;
using System.Collections;

public class Beam : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject beam = transform.gameObject;
		MeshFilter mesh_filter = beam.GetComponent<MeshFilter>();
		MeshRenderer mesh_renderer = beam.GetComponent<MeshRenderer>();
		Rigidbody2D mesh_rigidBody2D = beam.GetComponent<Rigidbody2D> ();
		BoxCollider2D mesh_boxCollider2D = beam.GetComponent<BoxCollider2D> ();
		mesh_rigidBody2D.isKinematic = true;
		mesh_boxCollider2D.isTrigger = true;
		//mesh_boxCollider2D.
		BuildBeamMesh (mesh_filter.mesh);
		//mesh_collider.sharedMesh = new Mesh ();
		//mesh_collider.isTrigger = true;
		//BuildBeamMesh (mesh_collider.sharedMesh);

		//mesh_collider = (MeshCollider)CreateBeamMesh ();
		//mesh_renderer = (MeshRenderer)CreateBeamMesh ();
	}
	
	Mesh BuildBeamMesh(Mesh mesh) {
		Vector3[] vertices = new Vector3[]
		{
			new Vector3( 1, 0, 0),
			new Vector3(0.8f, 5, 0),
			new Vector3(-0.8f, 5, 0),
			new Vector3(-1, 0, 0),
		};
		Vector2[] uv = new Vector2[]
		{
			new Vector2(1, 1),
			new Vector2(1, 0),
			new Vector2(0, 1),
			new Vector2(0, 0),
		};
		int[] triangles = new int[]
		{
			//0, 1, 2,
			//2, 1, 3,
			1, 0, 2,
			2, 0, 3,
		};
		mesh.vertices = vertices;
		mesh.uv = uv;
		mesh.triangles = triangles;
		return mesh;
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("pute");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
