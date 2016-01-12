using UnityEngine;
using System.Collections;
using System;

public enum CowState{
	IdleWalking = 0,
	IdleStatic= 1,
	IdleEating= 2,
	BeingLiftToShip= 3,
	Flying = 4,
	Lifted= 5,
	Crashed= 6,
	Affraid= 7,
	Dead = 8
}


public class Cow : MonoBehaviour {

	[SerializeField]
	private int m_id;

	[SerializeField]
	private bool m_isUFOCatched = false;

	[SerializeField]
	private GameObject m_UFOLight;
	[SerializeField]
	private CowState m_cowState;

	#region IdleWalking State
	private float m_timeStateWalkingStart;
	private float m_timeInWalkingAnim = 5.0f;
	private Vector2 m_targetDestination = Vector2.zero;
	private float m_cowSpeed = 0.5f;

	private Vector3 m_startLocalPosition;

	
	[Header("WalkArea")]
	[Space(10)]
	[SerializeField]
	private float m_minWalkArea = -9.0f;
	[SerializeField]
	private float m_maxWalkArea = 4.0f;

	#endregion IdleWalking State
	
	#region IdleStatic State
	[Space(10)]
	[Header("Static State")]
	[SerializeField]
	private float m_timeInStaticAnim = 2.0f;
	private float m_timeStateStaticStart;
	#endregion IdleStatic State
	
	#region Affraid State
	private float m_timeStateAffraidStart;
	[Header("Affraid State")]
	[Space(10)]
	[SerializeField]
	private float m_timeInAffraidAnim = 5.0f;
	private Vector3 m_localPosAffraidStart;
	#endregion Affraid State

	#region IdleEating State
	private float m_timeStateEatingStart;
	[Header("Eating State")]
	[Space(10)]
	[SerializeField]
	private float m_timeInEatingAnim = 3.0f;
	#endregion IdleEating State

	#region BeingLiftToShip State
	public Action<int> onBeingLiftToShipEnter;
	#endregion BeingLiftToShip State

	#region Lifted State
	public Action<int> onLiftedEnter;
	private float m_timeStateLiftedStart;
	private float m_timeInLiftedAnim = 3.0f;
	public bool m_pointAlreadyGive = false;
	#endregion Lifted State

	#region Flying State
	public Action<int> onFlyingEnter;
	private float m_flyingSpeed = 0.0f;
	
	[Space(10)]
	[Header("Flying State")]
	[SerializeField]
	private float m_cowDieIfSpeedOver = 7.5f;
	[SerializeField]
	private float m_cowAffraidIfSpeedOver = 2.5f;
	#endregion Flying State

	#region Crashed State
	public Action<int,float,CowState> onCrashedEnter;
	#endregion Crashed State

	#region Dead State
	public Action<int> onDeadEnter;
	#endregion Dead State

	[Space(10)]
	[Header("Flying State")]
	[SerializeField]
	private bool isDebug = true;

	[Space(10)]

	[SerializeField]
	private GameObject m_DeadAnim;


	private Animator m_animator;


	public float YGroundPLus;
	// Use this for initialization
	void Start () {
		m_flyingSpeed = 0f;
		m_animator = this.GetComponent<Animator> ();

		m_startLocalPosition = this.transform.localPosition;

		setCowState(CowState.IdleStatic);

		m_timeInStaticAnim += UnityEngine.Random.Range(-0.5f,0.5f);
		m_timeInWalkingAnim += UnityEngine.Random.Range(-0.5f,0.5f);
		m_timeInEatingAnim += UnityEngine.Random.Range(-0.5f,0.5f);
		m_timeInAffraidAnim += UnityEngine.Random.Range(-0.5f,0.5f);

		float cowColliderSize = this.GetComponent<BoxCollider2D>().size.y;
		GameObject groundCollider = GameObject.Find ("Ground");

		YGroundPLus = groundCollider.transform.position.y + groundCollider.GetComponent<BoxCollider2D> ().size.y/2 + cowColliderSize/2 + 0.001f;

	}

	public void Init(int id, string name){
		m_id = id;
		this.name = name;
	}

	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("STATE : " + m_cowState);
		switch (m_cowState) {
			case CowState.IdleStatic:
				UpdateIdleStatic();
				break;
			case CowState.IdleWalking:
				UpdateIdleWalking();
				break;
			case CowState.IdleEating:
				UpdateIdleEating();
				break;


			case CowState.BeingLiftToShip:
				UpdateBeingLiftToShip();
				break;
			case CowState.Flying:
				UpdateFlying();
				break;
			case CowState.Lifted:
				UpdateLifted();
				break;

			case CowState.Crashed:
				UpdateCrashed ();
				break;
			
			case CowState.Affraid:
				UpdateAffraid ();
				break;
				
			case CowState.Dead:
				UpdateDead ();
				break;
		}
		m_flyingSpeed = this.GetComponent<Rigidbody2D>().velocity.magnitude;
	}


	void UpdateIdleStatic (){
		if (Time.time - m_timeStateStaticStart >= m_timeInStaticAnim) {
			CowState[] futurState = new CowState[2]{
				CowState.IdleWalking,
				CowState.IdleEating
			};
			int rand = UnityEngine.Random.Range(0,2);
			setCowState(futurState[rand]);
		}
	}

	//La vache ne peut se déplacer qu'entre [-9;4] en X
	void UpdateIdleWalking (){
        //Debug.Log ("DISTANCE : " + Vector2.Distance(this.transform.localPosition,m_targetDestination));

        float _oldZ = this.transform.position.z;
		
        Vector2 newPos2D = Vector2.Lerp(
                this.transform.position, m_targetDestination, Time.deltaTime* m_cowSpeed/Vector2.Distance(this.transform.localPosition,m_targetDestination
                ));
        this.transform.position = new Vector3(newPos2D.x, newPos2D.y, _oldZ);


        if (Time.time - m_timeStateWalkingStart >= m_timeInWalkingAnim || Vector2.Distance(this.transform.localPosition,m_targetDestination) <= float.Epsilon ) {
			CowState[] futurState = new CowState[2]{
				CowState.IdleStatic,
				CowState.IdleEating
			};
			int rand = UnityEngine.Random.Range(0,2);
			setCowState(futurState[rand]);
		}
	}

	void UpdateIdleEating (){
		if (Time.time - m_timeStateEatingStart >= m_timeInEatingAnim) {
			CowState[] futurState = new CowState[2]{
				CowState.IdleStatic,
				CowState.IdleWalking
			};
			int rand = UnityEngine.Random.Range(0,2);
			setCowState(futurState[rand]);
		}
	}

	void UpdateBeingLiftToShip (){
		//LEt PM do the stuff <3
	}
	
	void UpdateFlying (){
	}
	
	void UpdateLifted (){
		//Let PM do the stuff <3
	}

	void UpdateCrashed (){
		
	}

	void UpdateAffraid (){
		Vector3 newLocalPos = m_localPosAffraidStart;
		newLocalPos.x += UnityEngine.Random.Range (-0.03f, 0.03f);
		newLocalPos.y = this.transform.localPosition.y;
		this.transform.localPosition = newLocalPos;

		if (Time.time - m_timeStateAffraidStart >= m_timeInAffraidAnim) {
			this.transform.localPosition = m_localPosAffraidStart;
			setCowState(CowState.IdleStatic);
		}
	}

	void UpdateDead(){

	}



	void OnCollisionEnter2D(Collision2D   coll){
		//Debug.Log ("COLLISION DETECTED WITH :" + coll.gameObject.name);
		if (coll.gameObject.tag == "Ground") {
			setCowState(CowState.Crashed);
		}
	}



	//Getter Setter
	public void setCowState(CowState newState){
		m_cowState = newState;
		StopAllAnim ();
		switch (m_cowState) {
			case CowState.IdleStatic:
				m_timeStateStaticStart = Time.time;
				//TODO: Lancer animation de static
				if(isDebug){
					GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.white);
				}
			break;
			case CowState.IdleWalking:
				m_animator.SetBool ("isWalking", true);
				if(UnityEngine.Random.Range(0,3) ==0) {
					AudioManager.Play("cow/cow_cloche");
				}
				m_timeStateWalkingStart = Time.time;
				float dest = UnityEngine.Random.Range (m_minWalkArea,m_maxWalkArea);

				m_targetDestination = new Vector2(dest,this.transform.localPosition.y);

				if(this.transform.localPosition.x < m_targetDestination.x){
					this.transform.eulerAngles = new Vector3(0f,180f,0f);	
				}else{
				this.transform.eulerAngles = new Vector3(0f,0f,0f);	
			}
			   
				//[-9;3.6]
				//TODO: Lancer animation de walking
				if(isDebug){
					GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.red);
				}
				break;
			case CowState.IdleEating:
				m_animator.SetBool ("isEating", true);
				m_timeStateEatingStart = Time.time;
				//TODO: Lancer animation de Eating
				if(isDebug){
					GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.green);
				}
			break;
			case CowState.BeingLiftToShip:
				//TODO: stop all animation and play Being lift anim
				if(isDebug){
					GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.blue);
				}
				if(onBeingLiftToShipEnter != null){
					onBeingLiftToShipEnter(m_id);
				}
			break;
			case CowState.Flying:
				if(isDebug){
					GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.white);
				}
				if(onFlyingEnter != null){
					onFlyingEnter(m_id);
				}
				break;
			case CowState.Lifted:
				if(isDebug){
					GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.black);
				}
				setIsUFOCatched(true);
				m_timeStateLiftedStart = Time.time;
				if(onLiftedEnter != null){
					onLiftedEnter(m_id);
				}
				
				break;
				
			case CowState.Crashed:
				if(isDebug){
					GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.red);
				}
				//Debug.Log ("SPEED AU CALCUL : " + m_flyingSpeed);
				//Avec les distances liées à la maquette, position possible de la cow en [-4.5;1]
				//La vitesse à l'arrivée est comprise entre [0;10]
				//Si la cow est lachée à Pos, elle aura à l'arrivée la vitesse Vit :
				//Pos -> Vit
				//-		-> 0
				//-3.75		-> 5
				//-1.75		-> 5.7
				//-0.3125	-> 9
				//1			-> 10
				//Debug.Log("TOTO "+m_flyingSpeed);
				if(m_flyingSpeed <= m_cowAffraidIfSpeedOver){
					setCowState(CowState.IdleStatic);
					if(m_isUFOCatched){
						//this.GetComponent<Rigidbody2D>().isKinematic = true;
						//this.GetComponent<BoxCollider2D>().enabled = false;
					}

				}else if(m_flyingSpeed <= m_cowDieIfSpeedOver){
					if(m_isUFOCatched){
						setCowState(CowState.IdleStatic);
						//this.GetComponent<Rigidbody2D>().isKinematic = true;
						//this.GetComponent<BoxCollider2D>().enabled = false;
					}else{
						setCowState(CowState.Affraid);
					}
				}else{
					setCowState(CowState.Dead);
				}
				if(onCrashedEnter != null){
				   		onCrashedEnter(m_id,m_flyingSpeed,m_cowState);
					if(m_isUFOCatched){
						m_pointAlreadyGive = true;
					}
				}
			
			break;
				
			case CowState.Affraid:
				m_timeStateAffraidStart = Time.time;
				if(isDebug){
					GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.yellow);
				}
				m_localPosAffraidStart = this.transform.localPosition;
				break;
				
		case CowState.Dead:
			
			m_animator.SetBool ("isDead", true);
			Vector3 vect = this.transform.localPosition;
			vect.y = m_startLocalPosition.y - 0.4f;
			this.transform.localPosition = vect;
			if (isDebug) {
				GetComponent<SpriteRenderer> ().material.SetColor ("_Color", Color.black);
			}
			m_DeadAnim.SetActive (true);
				
			
			this.GetComponent<Rigidbody2D> ().isKinematic = true;
			this.GetComponent<BoxCollider2D> ().enabled = false;
			if (onDeadEnter != null) {
				onDeadEnter (m_id);
			}
			Light[] lights = gameObject.GetComponentsInChildren<Light> ();
			for (int i = 0; i < lights.Length; i++) {
				lights [i].enabled = false;
				lights [i].gameObject.SetActive (false);
			}
			lights = null;
			break;
		}
	}


	private void StopAllAnim(){
		if(m_animator){
			m_animator.SetBool ("isWalking", false);
			m_animator.SetBool ("isEating", false);
			m_animator.SetBool ("isDead", false);
		}
	}

	public CowState getCowState(){
		return m_cowState;
	}

	private void setIsUFOCatched(bool isUFOCatched){
		if (isUFOCatched) {
			m_UFOLight.SetActive(true);
		} else {
			m_UFOLight.SetActive(false);
		}
		m_isUFOCatched = isUFOCatched;
	}
	
	public bool getIsUFOCatched(){
		return m_isUFOCatched;
	}

	public int getId(){
		return m_id;
	}

	public void hideAndFreeze(){
		this.GetComponent<Rigidbody2D>().isKinematic = true;
		this.GetComponent<SpriteRenderer>().enabled = false;
	}

	public void showAndUnfreeze (){
		this.GetComponent<Rigidbody2D>().isKinematic = false;
		this.GetComponent<SpriteRenderer>().enabled = true;
	}

	public void setCowAtPosition(Vector3 position){
		this.transform.position = position;
	}

	public void setOverGround(){
		setCowAtPosition (new Vector3 (this.transform.position.x,YGroundPLus,this.transform.position.z));
	}
}
