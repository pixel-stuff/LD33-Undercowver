using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	#region Singleton
	public static PlayerManager m_instance;
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
	private Rigidbody2D m_rigidbody;
	[Header("BounceSpaceShip")]
	public Vector2 bounceForce;
	public float deltaBounce;
	private float thresholdBounce;
	public float velocityMinForBounce;
	[Header("MoveSpaceShip")]
	[Space(10)]
	public float forceOnMove;
	public float deltaRotate;
	public float rotateMultiplicator;

	// Use this for initialization
	void Start () {
		GameStateManager.onChangeStateEvent += handleChangeGameState;
		m_rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
		thresholdBounce = this.transform.position.y - deltaBounce;
	}

	void handleChangeGameState(GameState newState){
		Debug.Log ("PLAYER SEE THE NEW STATE : " + newState);
	}

	#region Intéraction
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs (m_rigidbody.velocity.x) < velocityMinForBounce) {
			m_rigidbody.constraints = RigidbodyConstraints2D.None;
			if (this.transform.position.y < thresholdBounce) {
				m_rigidbody.AddForce (bounceForce, ForceMode2D.Impulse); //bounce
			}
		}
		rotateForVelocity ();
	}

	public void UP(){
		Debug.Log("UP ! ");
	}

	public void DOWN(){
		Debug.Log("DOWN ! ");
	}

	public void LEFT(){
		Debug.Log("LEFT ! ");
		moveLeft();
	}

	public void RIGHT(){
		Debug.Log("RIGHT ! ");
		moveRight();
	}

	private void moveLeft(){
		m_rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
		m_rigidbody.AddForce(new Vector2(-forceOnMove,0), ForceMode2D.Impulse);
	}
	
	private void moveRight(){
		m_rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
		m_rigidbody.AddForce (new Vector2 (forceOnMove, 0), ForceMode2D.Impulse);
	}
	
	private void rotateForVelocity(){
		this.transform.eulerAngles = new Vector3 (0, 0,-m_rigidbody.velocity.x * rotateMultiplicator);
	}

	#endregion Intéraction

	void OnDestroy(){
		GameStateManager.onChangeStateEvent -= handleChangeGameState;
	}
}
