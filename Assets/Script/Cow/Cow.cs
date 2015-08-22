using UnityEngine;
using System.Collections;

public enum CowState{
	Idle,
	BeingLift,
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
	
	// Update is called once per frame
	void Update () {
		//switch(
	}

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
