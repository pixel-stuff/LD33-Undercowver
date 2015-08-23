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

			cow = obj.GetComponent<Cow>();
			cow.Init("Cow_" + i);
			m_listCow.Add(cow);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
