using UnityEngine;
using System.Collections;

public class flash : MonoBehaviour {

	private float m_timer = 0.0f;
	private bool m_flash_start = false;

	// Use this for initialization
	void Start () {
		MeshRenderer mr = GetComponent<MeshRenderer>();
		mr.enabled = false;
		m_flash_start = true;
	}

	public void startFlash() {
		m_timer = 0.0f;
		m_flash_start = true;
	}

	// Update is called once per frame
	void Update () {
		if (m_flash_start) {
			if(m_timer>1.0f) {
				MeshRenderer mr = GetComponent<MeshRenderer>();
				mr.enabled = true;
			}
			if(m_timer>1.9f) {
				MeshRenderer mr = GetComponent<MeshRenderer>();
				mr.enabled = false;
			}
			m_timer+=Time.deltaTime;
		}
	}
}
