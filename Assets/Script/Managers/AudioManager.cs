using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	#region Singleton
	public static AudioManager m_instance;
	void Awake(){
		if(m_instance == null){
			//If I am the first instance, make me the Singleton
			m_instance = this;
			DontDestroyOnLoad(this.gameObject);
		}else{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != m_instance)
				Destroy(this.gameObject);
		}
		if (m_audioSourceBackground != null) {
			//Debug.Log ("AWAKE + AUDIO NOT NULL");
			//m_audioSourceBackground.Play ();
		}
	}
	#endregion Singleton

	private static string m_backgroundAudioSource = "grillon_background";

	private static Transform m_transform;

	private AudioSource beamSound;
	private bool isplayingBeam; 
	private GameObject beamGameObbjectSound;


	private AudioSource spaceMove;
	private bool isplayingMovingSound; 
	private GameObject shipGameObbjectSound;

	private static AudioSource m_audioSourceBackground;
	// Use this for initialization
	void Start () {
		m_transform = this.transform;
		//Play (m_backgroundAudioSource);
	}
	public void playSpaceMove(){
		if (!isplayingMovingSound) {
			isplayingMovingSound = true;
			shipGameObbjectSound = new GameObject ("Audio_" + "ship/shipMove");
			shipGameObbjectSound.transform.parent = m_transform;
			//Load clip from ressources folder
			AudioClip newClip = Instantiate (Resources.Load ("ship/shipMove", typeof(AudioClip))) as AudioClip;
		
			//Add and bind an audio source
			spaceMove = shipGameObbjectSound.AddComponent<AudioSource> ();
			spaceMove.clip = newClip;
			//Play and destroy the component
			spaceMove.loop = true;
			spaceMove.Play ();
		}
	}

	public void stopSpaceMove(){
		/*if (spaceMove && spaceMove.isPlaying) {
			isplayingMovingSound = false;
			spaceMove.Stop();
			Destroy(shipGameObbjectSound);
		}*/
	}


	public void PlayStartBeam(){
		if(!isplayingBeam) {
			isplayingBeam = true;
			beamGameObbjectSound = new GameObject ("Audio_" +  "beam/startBeam");
			beamGameObbjectSound.transform.parent = m_transform;
			//Load clip from ressources folder
			AudioClip newClip =  Instantiate(Resources.Load ("beam/startBeam", typeof(AudioClip))) as AudioClip;
			
			//Add and bind an audio source
			beamSound = beamGameObbjectSound.AddComponent<AudioSource>();
			beamSound.clip = newClip;
			//Play and destroy the component
			beamSound.Play();
			Destroy (beamGameObbjectSound, newClip.length);
			beamGameObbjectSound = null;

			if (beamSound.clip == null) {
				Debug.Log ("IS NULL");
			}
			Invoke ("playLoopBeam", beamSound.clip.length);
		}
	}


	public void playLoopBeam(){
		/*if (beamSound) {
			beamSound.Stop();
		}*/
		if (isplayingBeam && beamGameObbjectSound==null) {
			beamGameObbjectSound = new GameObject ("Audio_" + "beam/Beam");
			beamGameObbjectSound.transform.parent = m_transform;
			//Load clip from ressources folder
			AudioClip newClip = Instantiate (Resources.Load ("beam/Beam", typeof(AudioClip))) as AudioClip;
		
			//Add and bind an audio source
			beamSound = beamGameObbjectSound.AddComponent<AudioSource> ();
			beamSound.clip = newClip;
			//Play and destroy the component
			beamSound.loop = true;
			beamSound.Play ();
		}

	}


	public void StopBeam(){
		if (isplayingBeam) {
			if (beamSound.isPlaying) {
				beamSound.Stop ();
				Destroy (beamGameObbjectSound);
				beamGameObbjectSound = null;
			}
		
			beamGameObbjectSound = new GameObject ("Audio_" + "beam/EndBeam");
			beamGameObbjectSound.transform.parent = m_transform;
			//Load clip from ressources folder
			AudioClip newClip = Instantiate (Resources.Load ("beam/EndBeam", typeof(AudioClip))) as AudioClip;
			
			//Add and bind an audio source
			beamSound = beamGameObbjectSound.AddComponent<AudioSource> ();
			beamSound.clip = newClip;
			//Play and destroy the component
			beamSound.Play ();
			Destroy (beamGameObbjectSound, newClip.length);
			beamGameObbjectSound = null;
			isplayingBeam = false;
		}
	}

	public void clearBeam(){
		isplayingBeam = false;
		if (beamSound) {
			beamSound.Stop();
			Destroy(beamGameObbjectSound);
		}
	}

	public static void PlayBackgoundMusic(){
		//Debug.Log ("PLAY BACKGROUND MENU");
		GameObject go = new GameObject ("Audio_" +  m_backgroundAudioSource);
		go.transform.parent = m_transform;
		//Load clip from ressources folder
		AudioClip newClip =  Instantiate(Resources.Load (m_backgroundAudioSource, typeof(AudioClip))) as AudioClip;
		
		//Add and bind an audio source
		m_audioSourceBackground = go.AddComponent<AudioSource>();
		m_audioSourceBackground.clip = newClip;
		m_audioSourceBackground.loop = true;
		//Play and destroy the component
		m_audioSourceBackground.Play();
	}

	public void StopAllSouds(){
		Transform[] children = gameObject.GetComponentsInChildren<Transform> ();
		for (int i= 0; i<children.Length;i++){
			if (children [i] != null) {
				if(!children[i].Equals(this.transform)){
					Destroy (children [i].gameObject);
				}
			}
		}
		children = null;
	}


	public static void Play(string clipname){
		//Create an empty game object
		GameObject go = new GameObject ("Audio_" +  clipname);
		go.transform.parent = m_transform;
		//Load clip from ressources folder
		AudioClip newClip =  Instantiate(Resources.Load (clipname, typeof(AudioClip))) as AudioClip;

		//Add and bind an audio source
		AudioSource source = go.AddComponent<AudioSource>();
		source.clip = newClip;
		//Play and destroy the component
		source.Play();
		Destroy (go, newClip.length);
	}

	public static void PlayPersitente(string clipname){
		//Create an empty game object
		GameObject go = new GameObject ("Audio_" +  clipname);
		go.transform.parent = null;
		//Load clip from ressources folder
		AudioClip newClip =  Instantiate(Resources.Load (clipname, typeof(AudioClip))) as AudioClip;

		//Add and bind an audio source
		AudioSource source = go.AddComponent<AudioSource>();
		source.clip = newClip;
		//Play and destroy the component
		DontDestroyOnLoad(go);
		source.Play();
		Destroy (go, newClip.length);
	}

	// Update is called once per frame
	void Update () {
		//Debug.Log ("is dont = " + this.gameObject.don);
	}

	void OnApplicationPause(bool pauseStatus) {
		if (m_audioSourceBackground == null)
			return;

		if (pauseStatus) {
			m_audioSourceBackground.Pause ();
		} else {
			m_audioSourceBackground.Play();
		}
	}
}
