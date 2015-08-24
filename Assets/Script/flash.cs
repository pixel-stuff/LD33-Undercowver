using UnityEngine;
using System.Collections;

public class flash : MonoBehaviour {

	private float m_timer = 0.0f;
	private bool m_flash_start = false;
	[Header("Flash Element")]
	[SerializeField]
	private GameObject m_flash = null;

	// Use this for initialization
	void Start () {
		m_flash_start = false;
		m_flash.SetActive (false);
	}

	public void startFlash() {
		m_timer = 0.0f;
		m_flash_start = true;
	}

	// Update is called once per frame
	void Update () {
		if (m_flash_start) {
			if(m_timer>1.0f) {
				m_flash.SetActive (true);
			}
			if(m_timer>1.9f) {
				m_flash.SetActive (false);
			}
			m_timer+=Time.deltaTime;
		}
	}
}
