using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CowManager : MonoBehaviour {

	[SerializeField]
	private GameObject m_cowPrefab;

	private List<Cow> m_listCow;

	#region VarToRead
	[Header("Cows State")]
	[Space(10)]
	[SerializeField]
	private int m_numberOfUFOCowToReach = 0;
	[SerializeField]
	private int m_numberOfCow = 0;
	[SerializeField]
	private int m_numberOfUFOCow = 0;
	[SerializeField]
	private int m_numberOfDeadCow = 0;

	[SerializeField]
	private float m_areaWhereCowBeAffraid = 1.5f;
	#endregion VarToRead


	public Action onNewUFOCow;


	#region Singleton
	public static CowManager m_instance;
	void Awake(){
		if(m_instance == null){
			//If I am the first instance, make me the Singleton
			m_instance = this;
		}else{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != m_instance)
				Destroy(this.gameObject);
		}
	}
	#endregion Singleton


	// Use this for initialization
	void Start () {
		m_listCow = new List<Cow> ();


		//m_numberOfUFOCowToReach = PlayerManager.m_instance.levelCow;

		GameObject obj;
		Cow cow;
		//positionner cow entre [-4.75;-4.60] en Y
		//positionner cow entre [-9;3.6] en X
		for (int i=0; i < 15/*m_numberOfUFOCowToReach*/; i++) {
			obj = (GameObject)Instantiate(m_cowPrefab,new Vector3(UnityEngine.Random.Range(-9f,4f),-3.5f,0f ),Quaternion.identity);
			obj.transform.parent = this.transform;

			cow = obj.GetComponent<Cow>();
			cow.Init(i,"Cow_" + i);
			cow.onBeingLiftToShipEnter+= handleCowBeingLiftToShip;
			cow.onCrashedEnter += handleCowCrashed;
			cow.onDeadEnter += handleDead;
			cow.onFlyingEnter += handleCowFlying;
			cow.onLiftedEnter += handleLifted;
			m_listCow.Add(cow);
		}
		//m_listCow [0].setCowState (CowState.Lifted);
		//m_listCow [0].setCowState (CowState.IdleStatic);

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void createCow(){
		GameObject obj = (GameObject)Instantiate(m_cowPrefab,new Vector3(UnityEngine.Random.Range(-9f,3.6f),-3.5f,0f ),Quaternion.identity);
		obj.transform.parent = this.transform;
		
		Cow cow = obj.GetComponent<Cow>();
		cow.Init(m_listCow.Count+1,"Cow_" + (m_listCow.Count+1));
		
		cow.onBeingLiftToShipEnter+= handleCowBeingLiftToShip;
		cow.onCrashedEnter += handleCowCrashed;
		cow.onDeadEnter += handleDead;
		cow.onFlyingEnter += handleCowFlying;
		m_listCow.Add(cow);
		m_numberOfCow++;
	}

	public void handleCowBeingLiftToShip(int id){


	}

	void handleLifted (int id){
		m_numberOfUFOCow++;

		if (onNewUFOCow != null) {
			onNewUFOCow();
		}
	}

	public void handleCowFlying(int id){
		
	}

	public void handleCowCrashed(int id,float speedCrashed){
		//Debug.Log ("COW CRASH ");


		Cow cow = getCowByID (id);
		int numbAffraid = 0;
		//Debug.Log ("Cow Reference: " + cow.name);
		for (int i = 0; i<m_listCow.Count; i++) {
			//Debug.Log ("m_listCow[" + i + "]  : " + m_listCow[i]);
			if(m_listCow[i].getId() != id ){
				numbAffraid++;
				if(Vector3.Distance(m_listCow[i].transform.localPosition,cow.transform.localPosition) <= m_areaWhereCowBeAffraid){
					m_listCow[i].setCowState(CowState.Affraid);
				}
			}
		}

		if (numbAffraid <= 2) {
			AudioManager.Play("cow/Multiple_Cow Moo_Court");
		} else {
			AudioManager.Play("cow/Multiple_Cow Moo");
		}
	}

	public void handleDead(int id){
		m_numberOfDeadCow++;
		//Debug.Log ("COW DEAD ++");
	}


	//Getter Setter
	public int getNumberOfCow(){
		return m_numberOfCow;
	}

	public int getNumberOfUFOCow(){
		return m_numberOfUFOCow;
	}
	
	public int getNumberOfDeadCow(){
		return m_numberOfDeadCow;
	}

	public Cow getCowByID(int id){
		for (int i=0; i < m_listCow.Count; i++) {
			//Debug.Log("Check the if : " +  m_listCow[i].getId());
			if(m_listCow[i].getId() == id){
				//Debug.Log("Return found");
				return m_listCow[i];
			}
		}
		return null;
	}
}
