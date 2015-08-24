using UnityEngine;
using System.Collections;

public struct KeyPoint {
	public float x;
	public float y;
	public float t;
	public KeyPoint(float _x, float _y, float _t) {
		x = _x;
		y = _y;
		t = _t;
	}
}

public class House : MonoBehaviour {
	
	[Header("Light length")]
	[SerializeField]
	public float m_light_size = 10.0f;
	private float m_light_prev_size = 10.0f;

	private GameObject m_light;
	private GameObject m_light_anchor;

	private float m_timer_awake = 0.0f;
	[Header("Time span while player can hide (s)")]
	[SerializeField]
	private float m_timer_awake_length = 2.0f;
	[Header("Time span light is on (s)")]
	[SerializeField]
	private float m_timer_awake_lamp_length = 2.0f;

	private float m_angle_rot;

	private Vector3 m_light_pos;
	
	[Header("Window lights GameObject")]
	Light m_spotlight = null;
	public GameObject m_house_unlighted = null;
	public GameObject m_house_lighted = null;

	private bool m_house_is_lighted = false;

	private ArrayList m_key_points;
	private int m_key_points_index;

	// Use this for initialization
	void Start () {
		m_light = GameObject.Find ("Light");
		m_light_anchor = GameObject.Find("HouseLightAnchor");
		m_spotlight = GetComponentInChildren<Light> ();
		m_key_points = new ArrayList ();
		m_key_points_index = 0;
		m_light.SetActive (false);
		m_house_is_lighted = false;
		if(m_house_unlighted!=null)
			m_house_unlighted.SetActive(true);
		if(m_house_lighted!=null)
			m_house_lighted.SetActive(false);

		m_key_points.Add (new KeyPoint(40.0f,1.5f, 0.0f));
		m_key_points.Add (new KeyPoint(20.0f,1.5f, 2.0f));
		m_key_points.Add (new KeyPoint(0.0f,1.5f, 4.0f));
		m_key_points.Add (new KeyPoint(-20.0f,1.5f, 6.0f));
		m_key_points.Add (new KeyPoint(-40.0f,1.5f, 8.0f));
	}
	public void rude_awake() {
		if (!isAwake ()) {
			m_timer_awake = 0.0f;
			m_house_is_lighted = true;
			switch_house_light();
		}
	}

	void resizeLight(float size) {
		m_light_size = size;
		Vector3 vt = m_light.transform.localScale;
		vt.y = size;
		m_light.transform.localScale = vt;
		vt = m_light.transform.localPosition;
		vt.x = -size/2.0f;
		m_light.transform.localPosition = vt;
		m_spotlight.range = size*100.0f;
	}

	public bool isAwake() {
		return m_house_is_lighted;
	}
	
	public bool isLampOnHand() {
		return m_light.activeSelf;
	}
	
	public bool isAwakeAndLampOnHand() {
		return isAwake() && isLampOnHand();
	}

	void switch_house_light() {
		if (isAwake ()) {
			if(m_house_unlighted!=null) {
				m_house_unlighted.SetActive(false);
			}
			if(m_house_lighted!=null) {
				m_house_lighted.SetActive(true);
			}
		} else {
			if(m_house_unlighted!=null) {
				m_house_unlighted.SetActive(true);
			}
			if(m_house_lighted!=null) {
				m_house_lighted.SetActive(false);
			}
		}
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
	
	void setAngleLengthToLook(float angle, float length) {
		m_light_prev_size = m_light_size;
		m_light_size = length;
		m_angle_rot = angle;
		resizeLight (m_light_size);
	}
	
	// Update is called once per frame
	void Update () {
		if (isAwake() && !isLampOnHand() && m_timer_awake > m_timer_awake_lamp_length) {
			m_light.SetActive (true);
			m_spotlight.enabled = true;
			m_timer_awake = 0.0f;
		}
		if (isAwakeAndLampOnHand ()) {
			for (int i=0; i<m_key_points.Count; i++) {
				KeyPoint kp = (KeyPoint)m_key_points [i];
				if (m_timer_awake < kp.t) {
					setAngleLengthToLook (kp.x, kp.y);
					break;
				}
			}
			m_light_anchor.transform.eulerAngles = new Vector3 (0, 0, Mathf.LerpAngle (m_light_anchor.transform.eulerAngles.z, m_angle_rot, Time.smoothDeltaTime * 1.0f));
			if(m_timer_awake > m_timer_awake_length) {
				if (m_house_lighted != null)
					m_house_is_lighted = false;
				m_light.SetActive(false);
				m_spotlight.enabled = false;
				switch_house_light();
			}
		}
		// unlighting house
		if(isAwake ())
			m_timer_awake += Time.deltaTime;
	}
}
