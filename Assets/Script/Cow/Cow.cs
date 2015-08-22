using UnityEngine;
using System.Collections;

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
	private float m_timeInWalkingAnim = 2.0f;
	#endregion IdleWalking State

	
	#region IdleStatic State
	private float m_timeStateStaticStart;
	private float m_timeInStaticAnim = 1.5f;
	
	#endregion IdleStatic State

	#region IdleEating State
	private float m_timeStateEatingStart;
	private float m_timeInEatingAnim = 3.0f;
	
	#endregion IdleEating State

	// Use this for initialization
	void Start () {
		m_cowState = CowState.IdleStatic;

		m_timeInStaticAnim += Random.Range(-0.5f,0.5f);
		m_timeInWalkingAnim += Random.Range(-0.5f,0.5f);
		m_timeInEatingAnim += Random.Range(-0.5f,0.5f);
	}

	public void Init(int id, string name){
		m_id = id;
		this.name = name;
	}

	
	// Update is called once per frame
	void Update () {
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
			CowState[] futurState = new CowState[2]{
				CowState.IdleWalking,
				CowState.IdleEating
			};
			int rand = Random.Range(0,2);
			setCowState(futurState[rand]);
		}
	}

	//La vache ne peut se déplacer qu'entre [-9;3.6]
	void UpdateIdleWalking (){
		
	}

	void UpdateIdleEating (){
		
	}

	void UpdateBeingLift ()
	{
		
	}

	void UpdateBeingLiftToShip ()
	{
		
	}
	
	void UpdateFlying ()
	{

	}
	
	void UpdateLifted ()
	{
		
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
				break;
			case CowState.IdleWalking:
				m_timeStateWalkingStart = Time.time;
				break;
			case CowState.IdleEating:
				m_timeStateEatingStart = Time.time;
				break;
			case CowState.BeingLiftToShip:
				break;
			case CowState.Flying:
				break;
			case CowState.Lifted:
				break;
				
			case CowState.Crashed:
				break;
				
			case CowState.Affraid:
				break;
				
			case CowState.Dead:
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
}
