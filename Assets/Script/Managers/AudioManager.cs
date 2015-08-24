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
	}
	#endregion Singleton

	[SerializeField]
	private string m_backgroundAudioSource;

	private static Transform m_transform;

	private AudioSource beamSound;
	private bool isplayingBeam; 
	private GameObject beamGameObbjectSound;


	private AudioSource spaceMove;
	private bool isplayingMovingSound; 
	private GameObject shipGameObbjectSound;
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
		if (spaceMove && spaceMove.isPlaying) {
			isplayingMovingSound = false;
			spaceMove.Stop();
			Destroy(shipGameObbjectSound);
		}
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

			Invoke ("playLoopBeam", beamSound.clip.length);
		}
	}


	public void playLoopBeam(){
		/*if (beamSound) {
			beamSound.Stop();
		}*/
		if (isplayingBeam) {
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
		if (beamSound.isPlaying) {
			beamSound.Stop();
			Destroy(beamGameObbjectSound);
		}
	
		beamGameObbjectSound = new GameObject ("Audio_" +  "beam/EndBeam");
		beamGameObbjectSound.transform.parent = m_transform;
		//Load clip from ressources folder
		AudioClip newClip =  Instantiate(Resources.Load ("beam/EndBeam", typeof(AudioClip))) as AudioClip;
		
		//Add and bind an audio source
		beamSound = beamGameObbjectSound.AddComponent<AudioSource>();
		beamSound.clip = newClip;
		//Play and destroy the component
		beamSound.Play();
		Destroy (beamGameObbjectSound, newClip.length);
		isplayingBeam = false;
	}

	public void clearBeam(){
		/*isplayingBeam = false;
		if (beamSound) {
			beamSound.Stop();
			Destroy(beamGameObbjectSound);
		}*/
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

	
	// Update is called once per frame
	void Update () {
	
	}
}
