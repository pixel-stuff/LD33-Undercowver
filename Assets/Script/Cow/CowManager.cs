using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CowManager : MonoBehaviour {

	[SerializeField]
	private GameObject m_cowPrefab;

	private List<Cow> m_listCow;

	// Use this for initialization
	void Start () {
		m_listCow = new List<Cow> ();

		GameObject obj;
		Cow cow;
		for (int i=0; i < 4; i++) {
			obj = (GameObject)Instantiate(m_cowPrefab,Vector3.zero,Quaternion.identity);
			obj.transform.parent = this.transform;

			cow = GetComponent<Cow>();
			m_listCow.Add(obj.GetComponent<Cow>());
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
