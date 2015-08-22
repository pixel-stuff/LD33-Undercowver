using UnityEngine;
using System.Collections;

public enum CowState{
	Idle,
	BeingLiftToShip,
	BeingLiftToGround,
	Lifted,
	Crashed,
	Affraid
}


public class Cow : MonoBehaviour {

	[SerializeField]
	private bool m_isUFOCatched = false;

	[SerializeField]
	private CowState m_cowState;

	// Use this for initialization
	void Start () {
		m_cowState = CowState.Idle;
	}

	public void Init(string name){
		this.name = name;
	}
	
	// Update is called once per frame
	void Update () {
		switch (m_cowState) {
			case CowState.Idle:
				UpdateIdle();
				break;
			case CowState.BeingLiftToShip:
				UpdateBeingLiftToShip();
				break;
			case CowState.BeingLiftToGround:
				UpdateBeingLiftToGround();
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
		}
	}

	//La vache ne peut se déplacer qu'entre [-9;3.6]
	void UpdateIdle (){
		
	}

	void UpdateBeingLift ()
	{
		
	}

	void UpdateBeingLiftToShip ()
	{
		
	}
	
	void UpdateBeingLiftToGround ()
	{
		
	}
	
	void UpdateBeingLifted ()
	{
		
	}

	void UpdateCrashed ()
	{
		
	}

	void UpdateAffraid ()
	{
		
	}









	//Getter Setter
	public void setCowState(CowState newState){
		m_cowState = newState;
	}

	public CowState getCowState(){
		return m_cowState;
	}

	public void setIsUFOCatched(bool isUFOCatched){
		m_isUFOCatched = isUFOCatched;
	}
	
	public bool getIsUFOCatched(){
		return m_isUFOCatched;
	}
}
