using UnityEngine;
using System.Collections;
using UnityEngine.UI;
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
	private bool victory = false;
	[Header("BounceSpaceShip")]
	public Vector2 bounceForce;
	public float Ybounce;
	private float thresholdBounce;
	public float velocityMinForBounce;
	public bool shipIsArrive;
	public float arriveForce;
	[Header("MoveSpaceShip")]
	[Space(10)]
	public float forceOnMove;
	public float deltaRotate;
	public float rotateMultiplicator;
	public float xLimite;

	[Header("Noise")]
	[Space(10)]
	public float checkNoise;

	public float actualNoise;
	public float loseNoisePer250ms;

	private float nextActionTime = 0.0f; 
	private float period = 0.250f;

	[Header("SpaceShipNoise")]
	[Space(10)]
	public float velocityFactor;


	[Header("SoundFeedback")]
	[Space(10)]
	public GameObject soundFeedBack;
	// Use this for initialization

	[Header("Cow")]
	[Space(10)]
	public int levelCow;
	public int actualUEFOCow = 0;
	public GameObject CowText;

	[Header("Bean")]
	[Space(10)]
	public GameObject bean;

	[Header("Bean")]
	[Space(10)]
	public bool LaunchTuto = false;

	void Start () {
		GameStateManager.onChangeStateEvent += handleChangeGameState;
		m_rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
		thresholdBounce = Ybounce;

		CowManager.m_instance.onNewUFOCow += AddUFOCow;

		setCowText (actualUEFOCow+" / "+ levelCow);

		LaunchTuto = !GameStateManager.m_instance.alreadyHaveTuto;

	}

	private void setCowText(string s){
		CowText.GetComponent<Text> ().text = s;
	}


	private void AddUFOCow(){
		Debug.Log("zze");
		actualUEFOCow++;
		setCowText (actualUEFOCow+" / "+ levelCow);
	}

	void handleChangeGameState(GameState newState){
		Debug.Log ("PLAYER SEE THE NEW STATE : " + newState);
	}

	#region Intéraction
	// Update is called once per frame
	void Update () {
		if (!shipIsArrive) {
			m_rigidbody.AddForce (new Vector3(0,-arriveForce,0), ForceMode2D.Force);
		}


		if (Mathf.Abs (m_rigidbody.velocity.x) < velocityMinForBounce) {

			m_rigidbody.constraints = RigidbodyConstraints2D.None;
			if (this.transform.position.y < thresholdBounce) {
				if(!shipIsArrive){
					m_rigidbody.AddForce (new Vector3(0,arriveForce,0), ForceMode2D.Impulse);
					shipIsArrive =true;
				}
				m_rigidbody.AddForce (bounceForce, ForceMode2D.Impulse); //bounce
			}
		}
		rotateForVelocity ();

		if (transform.position.x <= -xLimite) {
			transform.position = new Vector2(-xLimite,transform.position.y);
		} else if (transform.position.x >= xLimite) {
			transform.position = new Vector2(xLimite,transform.position.y);
		}




		noiseForVelocity ();
		checkNoiseLevel ();

		if (Time.time > nextActionTime) { 
			nextActionTime += period;
			float minusNoise = actualNoise - loseNoisePer250ms;
			if(minusNoise > 0){
				actualNoise = minusNoise;
			} else {
				actualNoise = 0;
			}
		}

		if(actualUEFOCow >= levelCow){
			setVictory();

		}
		if (victory) {
			m_rigidbody.AddForce (new Vector2(0,150), ForceMode2D.Impulse);
		}
	}
	public void BeanUp(){
		bean.SetActive (true);
	}

	public void BeanDown(){
		bean.GetComponent<Beam>().clearCows();
		bean.SetActive (false);
	}

	public void UP(){
		Debug.Log("UP ! ");
	}

	public void DOWN(){
		Debug.Log("DOWN ! ");
	}

	public void LEFT(){
		if (shipIsArrive) {
			Debug.Log ("LEFT ! ");
			moveLeft ();
		}
	}

	public void RIGHT(){
		if (shipIsArrive) {
			Debug.Log ("RIGHT ! ");
			moveRight ();
		}
	}

	private void moveLeft(){
		m_rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
		m_rigidbody.AddForce(new Vector2(-forceOnMove,0), ForceMode2D.Impulse);
	}
	
	private void moveRight(){
		m_rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
		m_rigidbody.AddForce (new Vector2 (forceOnMove, 0), ForceMode2D.Impulse);
	}
	
	private void noiseForVelocity(){
		addNoise (Mathf.Abs (m_rigidbody.velocity.x)* velocityFactor);
	}
	
	private void rotateForVelocity(){
		this.transform.eulerAngles = new Vector3 (0, 0,-m_rigidbody.velocity.x * rotateMultiplicator);
	}

	public void addNoise(float noise){
		if (actualNoise < checkNoise * 1.1) {
			actualNoise += noise;
		}
		checkNoiseLevel ();
	}

	public void checkNoiseLevel() {

		if (actualNoise > checkNoise) {

			//todo check noise paysan
		}
		soundFeedBack.GetComponent<soundFeedBack> ().setSoundPercent ((actualNoise / checkNoise)*100);
	}

	public void setGameOver(){
		GameStateManager.setGameState (GameState.EndSceneGameOver);
	}

	public void setVictory(){
		Debug.Log ("Victory");
		victory = true;
	}
	                    

	#endregion Intéraction

	void OnDestroy(){
		GameStateManager.onChangeStateEvent -= handleChangeGameState;
	}
}
