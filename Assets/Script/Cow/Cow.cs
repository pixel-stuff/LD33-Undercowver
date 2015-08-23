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
	private CowState m_cowState;

	#region IdleWalking State
	private float m_timeStateWalkingStart;
	private float m_timeInWalkingAnim = 5.0f;
	private Vector2 m_targetDestination = Vector2.zero;
	private float m_cowSpeed = 0.5f;
	#endregion IdleWalking State

	
	#region IdleStatic State
	private float m_timeStateStaticStart;
	private float m_timeInStaticAnim = 2.0f;
	#endregion IdleStatic State

	#region IdleEating State
	private float m_timeStateEatingStart;
	private float m_timeInEatingAnim = 3.0f;
	#endregion IdleEating State

	#region BeingLiftToShip State
	public Action<int> onBeingLiftToShipEnter;
	#endregion BeingLiftToShip State

	#region Lifted State
	public Action<int> onLiftedEnter;
	private float m_timeStateLiftedStart;
	private float m_timeInLiftedAnim = 3.0f;
	#endregion Lifted State

	#region Flying State
	public Action<int> onFlyingEnter;
	#endregion Flying State

	#region Crashed State
	public Action<int> onCrashedEnter;
	#endregion Crashed State

	#region Dead State
	public Action<int> onDeadEnter;
	#endregion Dead State

	// Use this for initialization
	void Start () {
		m_cowState = CowState.IdleStatic;

		m_timeInStaticAnim += UnityEngine.Random.Range(-0.5f,0.5f);
		m_timeInWalkingAnim += UnityEngine.Random.Range(-0.5f,0.5f);
		m_timeInEatingAnim += UnityEngine.Random.Range(-0.5f,0.5f);
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
	}


	void UpdateIdleStatic (){
		if (Time.time - m_timeStateStaticStart >= m_timeInStaticAnim) {
			//TODO: Stop animation static
			CowState[] futurState = new CowState[2]{
				CowState.IdleWalking,
				CowState.IdleEating
			};
			int rand = UnityEngine.Random.Range(0,2);
			setCowState(futurState[rand]);
		}
	}

	//La vache ne peut se déplacer qu'entre [-9;3.6] en X
	void UpdateIdleWalking (){
		//Debug.Log ("DISTANCE : " + Vector2.Distance(this.transform.localPosition,m_targetDestination));


		this.transform.position = Vector2.Lerp(this.transform.position, m_targetDestination, Time.deltaTime* m_cowSpeed/Vector2.Distance(this.transform.localPosition,m_targetDestination));
		
		if (Time.time - m_timeStateWalkingStart >= m_timeInWalkingAnim || Vector2.Distance(this.transform.localPosition,m_targetDestination) <= float.Epsilon ) {
			//TODO: Stop animation de walk
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
			//TODO: stop animation de eat
			CowState[] futurState = new CowState[2]{
				CowState.IdleStatic,
				CowState.IdleWalking
			};
			int rand = UnityEngine.Random.Range(0,2);
			setCowState(futurState[rand]);
		}
	}

	void UpdateBeingLiftToShip ()
	{
		
	}
	
	void UpdateFlying ()
	{

	}
	
	void UpdateLifted (){
		//TODO: vérifier ce comportement avec PM
		if (Time.time - m_timeStateLiftedStart >= m_timeInLiftedAnim) {
			setCowState(CowState.Flying);
		}
	}

	void UpdateCrashed ()
	{
		
	}

	void UpdateAffraid ()
	{
		
	}

	void UpdateDead(){

	}







	//Getter Setter
	public void setCowState(CowState newState){
		m_cowState = newState;
		switch (m_cowState) {
			case CowState.IdleStatic:
				m_timeStateStaticStart = Time.time;
				//TODO: Lancer animation de static
				GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.white);
			break;
			case CowState.IdleWalking:
				m_timeStateWalkingStart = Time.time;
				float dest = UnityEngine.Random.Range (-9f,3.6f);

				m_targetDestination = new Vector2(dest,this.transform.localPosition.y);

				//[-9;3.6]
				//TODO: Lancer animation de walking
				GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.red);
				break;
			case CowState.IdleEating:
				m_timeStateEatingStart = Time.time;
				//TODO: Lancer animation de Eating
				GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.green);
			break;
			case CowState.BeingLiftToShip:
				//TODO: stop all animation and play Being lift anim
				GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.blue);
				if(onBeingLiftToShipEnter != null){
					onBeingLiftToShipEnter(m_id);
				}
			break;
			case CowState.Flying:
				if(onFlyingEnter != null){
					onFlyingEnter(m_id);
				}
				//Get current vitesse pour le passage à crashed
				break;
			case CowState.Lifted:
				m_isUFOCatched = true;
				m_timeStateLiftedStart = Time.time;
				if(onLiftedEnter != null){
					onLiftedEnter(m_id);
				}
				
				break;
				
			case CowState.Crashed:
				if(onCrashedEnter != null){
					onCrashedEnter(m_id);
				}
				break;
				
			case CowState.Affraid:
				break;
				
			case CowState.Dead:
				if(onDeadEnter != null){
					onDeadEnter(m_id);
				}
				break;
		}
	}

	public CowState getCowState(){
		return m_cowState;
	}

	private void setIsUFOCatched(bool isUFOCatched){
		m_isUFOCatched = isUFOCatched;
	}
	
	public bool getIsUFOCatched(){
		return m_isUFOCatched;
	}

	public int getId(){
		return m_id;
	}
}
