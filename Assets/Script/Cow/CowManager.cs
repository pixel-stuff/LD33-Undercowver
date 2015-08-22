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
		//positionner cow entre [-4.75;-4.60] en Y
		//positionner cow entre [-9;3.6] en X
		for (int i=0; i < 4; i++) {
			obj = (GameObject)Instantiate(m_cowPrefab,new Vector3(Random.Range(-9f,3.6f), Random.Range(-4.75f,-4.60f),0f ),Quaternion.identity);
			obj.transform.parent = this.transform;

			cow = obj.GetComponent<Cow>();
			cow.Init(i,"Cow_" + i);
			m_listCow.Add(cow);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
