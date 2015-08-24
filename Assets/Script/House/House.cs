using UnityEngine;
using System.Collections;

public class House : MonoBehaviour {

	[SerializeField]
	public float m_light_size = 10.0f;

	private GameObject m_light;
	private GameObject m_light_anchor;

	private float timer_awake = 0.0f;
	[SerializeField]
	public float m_angle_rot;
	private float m_angle_rot_v = 0.1f;
	private float m_angle_rot_min = 15.0f;
	private float m_angle_rot_max = 30.0f;

	private Vector2 m_target = Vector2.zero;

	private Vector3 m_light_pos;

	// Use this for initialization
	void Start () {
		m_light = GameObject.Find ("Light");
		m_light_anchor = GameObject.Find("HouseLightAnchor");
		//m_light_anchor = new GameObject("HouseLightAnchor");
		//m_light_anchor.AddComponent<Transform> ();
		//m_light_anchor.transform.position = transform.position;
		Vector3 vt = m_light.transform.localScale;
		vt.y = m_light_size;
		m_light.transform.localScale = vt;
		vt = m_light.transform.localPosition;
		vt.x -= m_light_size/2.0f;
		m_light.transform.localPosition = vt;
		m_light.SetActive (false);
		// rotatio
		//rotate (-90.0f);
		m_angle_rot = m_angle_rot_min;
		rude_awake();
		m_light_pos = m_light_anchor.transform.position;
	}

	void rotate(float angle) {
		//Vector3 vt = m_light.transform.position;
		//m_light.transform.Translate (-transform.position);
		//Vector3 angles = Vector3.zero;
		//angles.z = angle;
		Bounds b = m_light.GetComponent<BoxCollider2D> ().bounds;
		Vector3 c = Vector3.zero;
		c.x = transform.position.x;//+b.min.x;
		c.y = transform.position.y;//+(b.max.y+b.min.x)/2.0f;
		//m_light_anchor.transform.position = c;
		m_light.transform.RotateAround (m_light_anchor.transform.position, Vector3.forward, angle);
		//m_light.transform.Translate (transform.position);
		/*m_light.transform.parent = m_light_anchor.transform;
		Vector3 vt = Vector3.zero;
		vt.z = m_angle_rot;
		m_light.transform.Rotate (vt);
		m_light.transform.parent = transform;*/
		/*m_target = new Vector2 (Mathf.Cos (m_angle_rot) * m_light_size,
		                       Mathf.Sin (m_angle_rot) * m_light_size);
		
		Vector3 vt = m_light.transform.localScale;
		vt.y = m_light_size;
		m_light.transform.localScale = vt;
		vt = m_light.transform.localPosition;
		vt.x = (transform.position.x + m_target.x) / 2.0f;
		vt.y = (transform.position.y + m_target.y) / 2.0f;
		m_light.transform.position = vt;
		m_light.transform.Rotate (new Vector3 (0, 0, angle));*/
	}

	void rotateTo(float from, float to, float step) {
		Quaternion q = Quaternion.AngleAxis(Mathf.LerpAngle(from, to, step), Vector3.forward);
		m_light.transform.position = q * m_light_pos;
		m_light.transform.rotation = q;
	}

	void rude_awake() {
		m_light.SetActive (true);
		//GameObject ufo = GameObject.FindWithTag ("PlayerManager");

		/*Vector3 angles = m_light.transform.eulerAngles;
		angles.z = m_angle_rot;
		Vector3 vt = transform.position;
		transform.position = Vector3.zero;
		m_light.transform.RotateAround(transform.position,Vector3.forward, m_angle_rot);
		transform.position = vt;*/
		//rotate (m_angle_rot);
	}

	void LightRotateAround(Vector3 center, Vector3 axis, float angle){
		Vector3 pos  = m_light.transform.position;
		Quaternion rot  = Quaternion.AngleAxis(angle, axis); // get the desired rotation
		Vector3 dir  = pos - center; // find current direction relative to center
		dir = rot * dir; // rotate the direction
		m_light.transform.position = center + dir; // define new position
		// rotate object to keep looking at the center:
		Quaternion myRot  = m_light.transform.rotation;
		m_light.transform.rotation *= Quaternion.Inverse(myRot) * rot * myRot;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject ufo = GameObject.Find("PlayerManager").GetComponentInChildren<BoxCollider2D>().gameObject;
		/*GameObject ufo = GameObject.Find("PlayerManager").GetComponentInChildren<BoxCollider2D>().gameObject;
		Vector3 to = new Vector2 (ufo.transform.position.x, ufo.transform.position.y);
		Vector3 from = new Vector2 (m_light_anchor.transform.position.x, m_light_anchor.transform.position.y);
		Vector3 dir = (to - from);
		Ray2D ray = new Ray2D(m_light_anchor.transform.position, dir);
		RaycastHit2D hit = new RaycastHit2D ();
		Debug.DrawLine(ray.origin, ray.origin+ray.direction, Color.green, 2, false);
		if (Physics2D.Raycast(from, dir, m_light_size)) {

		} else {

		}*/
		Vector3 to = ufo.transform.position;
		Vector3 from = m_light_anchor.transform.position;
		//Vector3 to = new Vector3 (ufo.transform.position.x, ufo.transform.position.y, 0);
		//Vector3 from = new Vector3 (m_light_anchor.transform.position.x, m_light_anchor.transform.position.y, 0);
		Vector3 dir = (to - from);
		//Quaternion lookAt = Quaternion.LookRotation(to-from);
		Vector3 diff = to-from;
		diff.Normalize();
		
		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		//Quaternion lookAt = Quaternion.LookRotation(to - from, m_light_anchor.transform.TransformDirection(Vector3.up));
		//Debug.Log (m_light.transform.eulerAngles.z+" "+rot_z);
		//transform.rotation = Quaternion.Lerp(transform.rotation, lookAt, Time.smoothDeltaTime * 0.1f);
		//m_light.transform.rotation = Quaternion.Slerp (m_light.transform.rotation, Quaternion.Euler (0, 0, rot_z+90.0f), 1.0f * Time.deltaTime);
		//rotate (Mathf.LerpAngle(m_light.transform.eulerAngles.z, rot_z, Time.smoothDeltaTime * 0.1f));
		m_light.transform.rotation = Quaternion.identity;
		m_light.transform.position = Vector3.one;
		//m_light.transform.position = Vector3.zero;
		rotate (Mathf.LerpAngle(m_light.transform.eulerAngles.z, rot_z, Time.smoothDeltaTime * 0.1f));
		//m_light.transform.position = Vector3.zero;
		//LightRotateAround (from, Vector3.forward, rot_z + 90.0f);
		//m_light.transform.position = lp + lra;
		timer_awake += Time.deltaTime;
		/*if(timer_awake<6.0f) {
			rude_awake();
		}else if(timer_awake>6.0f) {
			timer_awake = 0.0f;
		}
		timer_awake += Time.deltaTime;*/
	}
}
