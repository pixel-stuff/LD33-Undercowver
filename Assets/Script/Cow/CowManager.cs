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
	private float m_areaWhereCowBeAffraid = 2.3f;
	#endregion VarToRead

	
	[Header("Sound Noise")]
	[Space(10)]
	[SerializeField]
	private float m_deadCowNoise = 15.0f;
	[SerializeField]
	private float m_affraidCowNoise = 8.0f;
	[SerializeField]
	private float m_multipleAffraidCowNoise = 13.0f;

	private float m_timeStarted;
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
		m_timeStarted = Time.time;

		m_numberOfUFOCowToReach = GameStateManager.m_instance.getNumberOfCowToLoad();

		GameObject obj;
		Cow cow;
		//positionner cow entre [-4.75;-4.60] en Y
		//positionner cow entre [-9;3.6] en X
		for (int i=0; i < m_numberOfUFOCowToReach; i++) {
			obj = (GameObject)Instantiate(m_cowPrefab,new Vector3(UnityEngine.Random.Range(-9f,4f),-3.87f,0f ),Quaternion.identity);
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
		GameObject obj = (GameObject)Instantiate(m_cowPrefab,new Vector3(UnityEngine.Random.Range(-9f,3.87f),-3.55f,0f ),Quaternion.identity);
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

	}

	public void handleCowFlying(int id){
		
	}


	public void handleCowCrashed(int id,float speedCrashed,CowState state){
		//Debug.Log ("COW CRASH ");
		if (Time.time - m_timeStarted <= 1.0f) {
			return;
		}

		Cow cow = getCowByID (id);

		if (cow.getIsUFOCatched () && state != CowState.Dead && !cow.m_pointAlreadyGive) {
			if (onNewUFOCow != null) {
				onNewUFOCow ();
			}
		} else {
			//Debug.Log("CRASHED OK");

			int numbAffraid = 0;
			//Debug.Log ("Cow Reference: " + cow.name);
			for (int i = 0; i<m_listCow.Count; i++) {
				//Debug.Log ("m_listCow[" + i + "]  : " + m_listCow[i]);
				if (m_listCow [i].getId () != id && m_listCow[i].getCowState() != CowState.Dead && !m_listCow[i].getIsUFOCatched()) {
					if (Vector3.Distance (m_listCow [i].transform.localPosition, cow.transform.localPosition) <= m_areaWhereCowBeAffraid) {
						numbAffraid++;
						m_listCow [i].setCowState (CowState.Affraid);
					}
				}
			}
			//Debug.Log("TOTO  " + numbAffraid);
			if(numbAffraid >=2){
				//Debug.Log("PLAY MULTIPLE SOUND");
				PlayerManager.m_instance.addNoise (m_multipleAffraidCowNoise);
				AudioManager.Play ("cow/Multiple_CowMoo_Court");
			}else{
				//Debug.Log("PLAY SOUND");
				PlayerManager.m_instance.addNoise (m_affraidCowNoise);
				AudioManager.Play ("cow/Cow_Moo"); 
			}
		}
	}

	public void handleDead(int id){
		m_numberOfDeadCow++;
		//Debug.Log ("COW DEAD ++");
		createCow ();
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
