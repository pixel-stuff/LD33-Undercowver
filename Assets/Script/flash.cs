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
		//m_flash.SetActive (true);
		//Debug.Log ("START");
	}

	// Update is called once per frame
	void Update () {
		if (m_flash_start) {
			//Debug.Log (m_timer);
			if(m_timer>0.9f) {
				m_flash.SetActive (true);
				AudioManager.Play("camera_shutter");
			}
			if(m_timer>1.5f) {
				m_flash.SetActive (false);
			}
			m_timer+=Time.deltaTime;
		}
	}
}
