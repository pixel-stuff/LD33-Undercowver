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
		vt.y -= m_light_size/2.0f;
		m_light.transform.localPosition = vt;
		m_light.SetActive (false);
		// rotatio
		rotate (-100.0f);
		m_angle_rot = m_angle_rot_min;
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
		m_light.transform.RotateAround (m_light_anchor.transform.position, Vector3.forward, angle*Mathf.PI/180.0f);
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

	void rude_awake() {
		m_light.SetActive (true);
		/*Vector3 angles = m_light.transform.eulerAngles;
		angles.z = m_angle_rot;
		Vector3 vt = transform.position;
		transform.position = Vector3.zero;
		m_light.transform.RotateAround(transform.position,Vector3.forward, m_angle_rot);
		transform.position = vt;*/
		rotate (m_angle_rot);
		if (m_angle_rot_v>0.0f && m_angle_rot >= m_angle_rot_max) {
			m_angle_rot_v = -m_angle_rot_v;
		} else if(m_angle_rot_v<0.0f && m_angle_rot<=m_angle_rot_min) {
			m_angle_rot_v = -m_angle_rot_v;
		}
		m_angle_rot += m_angle_rot_v;
	}
	
	// Update is called once per frame
	void Update () {
		if(timer_awake<6.0f) {
			rude_awake();
		}else if(timer_awake>6.0f) {
			timer_awake = 0.0f;
		}
		timer_awake += Time.deltaTime;
	}
}
