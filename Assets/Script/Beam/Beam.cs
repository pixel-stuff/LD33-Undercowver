using UnityEngine;
using System.Collections;

public class Beam : MonoBehaviour {
	[SerializeField]
	public float m_x_base = 1.0f;
	[SerializeField]
	public float m_x_top = 0.8f;
	[SerializeField]
	public float m_y_base = 0.0f;
	[SerializeField]
	public float m_y_top = 10.0f;

	private Cow m_catched;

	private GameObject m_border_left;
	private GameObject m_border_right;
	private GameObject m_border_top;

	// Use this for initialization
	void Start () {
		m_catched = null;

		GameObject beam = transform.gameObject;
		MeshFilter mesh_filter = beam.GetComponent<MeshFilter>();
		Rigidbody2D mesh_rigidBody2D = beam.GetComponent<Rigidbody2D> ();
		BoxCollider2D mesh_boxCollider2D = beam.GetComponent<BoxCollider2D> ();
		mesh_rigidBody2D.isKinematic = true;
		mesh_boxCollider2D.isTrigger = true;
		Vector2 boxColl2D_center = mesh_boxCollider2D.offset;
		boxColl2D_center = new Vector2 ((m_x_base - m_x_top) / 2.0f, (m_y_top-m_y_base)/2.0f);
		mesh_boxCollider2D.offset = boxColl2D_center;
		Vector2 boxColl2D_size = mesh_boxCollider2D.size;
		boxColl2D_size = new Vector2 ((m_x_base>m_x_top?m_x_base:m_x_top)*2.0f, (m_y_top>m_y_base?m_y_top:m_y_base));
		mesh_boxCollider2D.size = boxColl2D_size;
		BuildBeamMesh (mesh_filter.mesh);
		ParticleSystem beam_particles = beam.GetComponentInChildren<ParticleSystem> ();
		beam_particles.transform.localPosition = new Vector3 (transform.localPosition.x, m_y_base, 0.0f);

		m_border_left = GameObject.CreatePrimitive (PrimitiveType.Quad);
		m_border_left.transform.parent = transform;
		m_border_left.transform.localPosition = new Vector3 (0, 0, 0);
		m_border_left.transform.Rotate (new Vector3 (0.0f, 0.0f, -10.0f));
		m_border_left.transform.localScale = new Vector3 (0.1f, m_y_top-m_y_base, 0.0f);
		m_border_left.transform.localPosition = new Vector3 (-m_x_top, (m_y_top-m_y_base)/2.0f, 0.0f);
		m_border_left.GetComponent<MeshRenderer> ().enabled = false;
		MeshCollider meshcol_tmp = m_border_left.GetComponent<MeshCollider> ();
		Component.DestroyImmediate (meshcol_tmp);
		m_border_left.AddComponent<BoxCollider2D> ().isTrigger = true;
		Beam_borders_collider border = m_border_left.AddComponent<Beam_borders_collider> ();
		border.parent = this;

		m_border_right = GameObject.CreatePrimitive (PrimitiveType.Quad);
		m_border_right.transform.parent = transform;
		m_border_right.transform.localPosition = new Vector3 (0, 0, 0);
		m_border_right.transform.Rotate (new Vector3 (0.0f, 0.0f, 10.0f));
		m_border_right.transform.localScale = new Vector3 (0.1f, m_y_top-m_y_base, 0.0f);
		m_border_right.transform.localPosition = new Vector3 (m_x_top, (m_y_top-m_y_base)/2.0f, 0.0f);
		m_border_right.GetComponent<MeshRenderer> ().enabled = false;
		meshcol_tmp = m_border_right.GetComponent<MeshCollider> ();
		Component.DestroyImmediate (meshcol_tmp);
		m_border_right.AddComponent<BoxCollider2D> ().isTrigger = true;
		border = m_border_right.AddComponent<Beam_borders_collider> ();
		border.parent = this;

		m_border_top = GameObject.CreatePrimitive (PrimitiveType.Quad);
		m_border_top.transform.parent = transform;
		m_border_top.transform.localScale = new Vector3 (2.0f, 2.0f, 0.0f);
		m_border_top.transform.localPosition = new Vector3 (0.0f, m_y_top-0.5f, 0.0f);
		m_border_top.GetComponent<MeshRenderer> ().enabled = false;
		meshcol_tmp = m_border_top.GetComponent<MeshCollider> ();
		Component.DestroyImmediate (meshcol_tmp);
		m_border_top.AddComponent<BoxCollider2D> ().isTrigger = true;
		border = m_border_top.AddComponent<Beam_borders_collider> ();
		border.isBorderTop = true;
		border.parent = this;

	}
	
	Mesh BuildBeamMesh(Mesh mesh) {
		Vector3[] vertices = new Vector3[]
		{
			new Vector3(m_x_base, m_y_base, 0),
			new Vector3(m_x_top, m_y_top, 0),
			new Vector3(-m_x_top, m_y_top, 0),
			new Vector3(-m_x_base, m_y_base, 0),
		};
		Vector2[] uv = new Vector2[]
		{
			new Vector2(1, 0),
			new Vector2(1, 1),
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

	public Cow getCatched() {
		return m_catched;
	}
	
	public void docked() {
		m_catched = null;
		Debug.Log ("Docked");
	}

	public void releaseCatched() {
		m_catched = null;
		Debug.Log ("Released");
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (m_catched!=null && other.GetComponent<Cow> ()!=null) {
			m_catched = other.GetComponent<Cow>();
			m_catched.setCowState(CowState.BeingLiftToShip);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
