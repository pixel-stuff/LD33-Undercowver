using UnityEngine;
using System.Collections;

public class House : MonoBehaviour {

	[SerializeField]
	public float m_light_size = 10.0f;
	private float m_light_prev_size = 10.0f;

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

	Light m_spotlight = null;

	private ArrayList m_angles;
	private int m_angles_index;

	// Use this for initialization
	void Start () {
		m_light = GameObject.Find ("Light");
		m_light_anchor = GameObject.Find("HouseLightAnchor");
		m_spotlight = GetComponentInChildren<Light> ();
		m_angles = new ArrayList ();
		m_angles_index = 0;
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
		rude_awake ();
		m_angles.Add (0.02f);
		m_angles.Add (-5.00f);
		m_angles.Add (-10.0f);
		m_angles.Add (-15.0f);
		m_angles.Add (-45.0f);
		m_angles.Add (-90.0f);
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

	void resizeLight(float size) {
		m_light_size = size;
		Vector3 vt = m_light_anchor.transform.localScale;
		vt.x = size;
		m_light_anchor.transform.localScale = vt;
		//vt = m_light.transform.localPosition;
		//vt.x = m_light.transform.position.x-size;
		//m_light.transform.localPosition = vt;
		m_spotlight.range = size;
	}

	void lightToLastPositionEared(Vector2 position) {
		// lets go
		Vector3 to = position;
		Vector3 from = m_light_anchor.transform.position;
		Vector3 dir = (to - from);
		resizeLight (dir.magnitude);
		Vector3 diff = to-from;
		diff.Normalize();
		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		m_light.transform.RotateAround (m_light_anchor.transform.position, Vector3.forward, rot_z+180.0f);
	}
	
	void lightToAngleEared(float angle) {
		float lookAt = Quaternion.Angle (
			m_light.transform.rotation,
			Quaternion.AngleAxis (angle, Vector3.forward)
			);
		/*Vector3 angles = m_light_anchor.transform.rotation.eulerAngles;
		angles.z = angle;
		m_light_anchor.transform.rotation.eulerAngles = angles;*/
		m_light_anchor.transform.RotateAround (m_light_anchor.transform.position, Vector3.forward, lookAt);

	}

	void setPointToLook(Vector2 target) {
		Vector3 to = target;
		Vector3 from = m_light_anchor.transform.position;
		Vector3 dir = (to - from);
		Vector3 diff = to-from;
		diff.Normalize();
		m_light_prev_size = m_light_size;
		m_light_size = dir.magnitude;
		m_angle_rot = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		resizeLight (m_light_size);
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
		/*Vector3 to = ufo.transform.position;
		Vector3 from = m_light_anchor.transform.position;
		//Vector3 to = new Vector3 (ufo.transform.position.x, ufo.transform.position.y, 0);
		//Vector3 from = new Vector3 (m_light_anchor.transform.position.x, m_light_anchor.transform.position.y, 0);
		Vector3 dir = (to - from);
		//Quaternion lookAt = Quaternion.LookRotation(to-from);
		Vector3 diff = to-from;
		diff.Normalize();
		Vector3 actual = Vector3.left - from;
		actual.Normalize();
		
		float rot_z_actual = Mathf.Atan2(actual.y, actual.x) * Mathf.Rad2Deg;
		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		//Quaternion lookAt = Quaternion.LookRotation(to - from, m_light_anchor.transform.TransformDirection(Vector3.up));
		Debug.Log (rot_z_actual-180.0f+" "+rot_z+180.0f);*/
		//transform.rotation = Quaternion.Lerp(transform.rotation, lookAt, Time.smoothDeltaTime * 0.1f);
		//m_light.transform.rotation = Quaternion.Slerp (m_light.transform.rotation, Quaternion.Euler (0, 0, rot_z+90.0f), 1.0f * Time.deltaTime);
		//rotate (Mathf.LerpAngle(m_light.transform.eulerAngles.z, rot_z, Time.smoothDeltaTime * 0.1f));
		//rotate (Mathf.LerpAngle(rot_z_actual-180.0f, rot_z+180.0f, Time.smoothDeltaTime * 0.1f));
		//m_light.transform.position = Vector3.zero;
		//m_light.transform.rotation = Quaternion.identity;
		//m_light.transform.position = Vector3.one;
		//LightRotateAround (from, Vector3.forward, rot_z + 90.0f);
		//m_light.transform.position = lp + lra;
		/*if(timer_awake<6.0f) {
			rude_awake();
			//rotate ((m_angle_rot+=0.1f)+180.0f);
		}else if(timer_awake>6.0f) {
			timer_awake = 0.0f;
			m_angle_rot = 0.0f;
		}*/
		if (timer_awake >4.0f && timer_awake<8.0f) {
			//resizeLight (10.0f);
			//float angle = 0.0f;
			//lightToAngleEared (Mathf.LerpAngle(0.0f, 45.0f, Time.smoothDeltaTime));
			//m_angles_index = (m_angles_index+1)%m_angles.Count;
			//Debug.Log (m_angles_index+"/"+m_angles.Count+" "+(float)m_angles[m_angles_index]);
			//lightToAngleEared ((float)m_angles[m_angles_index]);
			//timer_awake = 0.0f;
			Debug.Log ("LOOK!");
			setPointToLook(new Vector2(0.0f,0.0f));
		}
		/*if (timer_awake >8.0f) {
			//resizeLight (10.0f);
			//float angle = 0.0f;
			//lightToAngleEared (Mathf.LerpAngle(0.0f, 45.0f, Time.smoothDeltaTime));
			//m_angles_index = (m_angles_index+1)%m_angles.Count;
			//Debug.Log (m_angles_index+"/"+m_angles.Count+" "+(float)m_angles[m_angles_index]);
			//lightToAngleEared ((float)m_angles[m_angles_index]);
			//timer_awake = 0.0f;
			Debug.Log ("LOOK!");
			setPointToLook(new Vector2(02.0f,-0.0f));
		}*/
		//resizeLight (Mathf.Lerp (m_light_prev_size, m_light_size, Time.time * 1.0f));
		m_light_anchor.transform.rotation = Quaternion.Lerp(m_light_anchor.transform.rotation, Quaternion.AngleAxis(m_angle_rot, Vector3.forward), Time.time * 1.0f);
		//lightToAngleEared (Mathf.LerpAngle(0.0f, -90.0f, Time.smoothDeltaTime*10.0f));
		//Debug.Log (Mathf.LerpAngle(0.0f, 45.0f, Time.smoothDeltaTime));
		timer_awake += Time.deltaTime;
	}
}
