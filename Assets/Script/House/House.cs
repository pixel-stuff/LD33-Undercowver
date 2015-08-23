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
		rotate (-90.0f);
	}

	void rotate(float angle) {
		//Vector3 vt = m_light.transform.position;
		//m_light.transform.Translate (-transform.position);
		//Vector3 angles = Vector3.zero;
		//angles.z = angle;=
		Vector3 c = Vector3.zero;//GetComponent<BoxCollider2D> ().bounds.center;
		m_light.transform.RotateAround (m_light_anchor.transform.position, Vector3.forward, angle);
		//m_light.transform.Translate (transform.position);
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
		m_angle_rot += 0.01f;
		if (m_angle_rot > 90.0f)
			m_angle_rot = -90.0f;
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
