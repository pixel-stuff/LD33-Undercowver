using UnityEngine;
using System.Collections;

public class SpaceShipMove : MonoBehaviour {

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

	/*[Header("SpaceShip")]
	[Space(10)]*/
	// Use this for initialization
	void Start () {
		m_rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
		thresholdBounce = this.transform.position.y - deltaBounce;
	}
	
	// Update is called once per frame
	void Update () {

		if (Mathf.Abs (m_rigidbody.velocity.x) < velocityMinForBounce) {
			m_rigidbody.constraints = RigidbodyConstraints2D.None;
			if (this.transform.position.y < thresholdBounce) {
				m_rigidbody.AddForce (bounceForce, ForceMode2D.Impulse); //bounce
			}
		}

		/*if(Input.GetKey("q") || Input.GetKey("a")){
			m_rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
			m_rigidbody.AddForce(new Vector2(-forceOnMove,0), ForceMode2D.Impulse);
		}
		
		if(Input.GetKey("d")){
			m_rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;;
			m_rigidbody.AddForce(new Vector2(forceOnMove,0), ForceMode2D.Impulse);
		}*/
		rotateForVelocity ();
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
}
