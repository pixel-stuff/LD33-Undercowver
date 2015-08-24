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
	private AudioSource spaceMove;
	// Use this for initialization
	void Start () {
		m_transform = this.transform;
		//Play (m_backgroundAudioSource);
	}
	public void playSpaceMove(){
		GameObject go = new GameObject ("Audio_" +  "beam/Beam");
		go.transform.parent = m_transform;
		//Load clip from ressources folder
		AudioClip newClip =  Instantiate(Resources.Load ("beam/Beam", typeof(AudioClip))) as AudioClip;
		
		//Add and bind an audio source
		spaceMove = go.AddComponent<AudioSource>();
		spaceMove.clip = newClip;
		//Play and destroy the component
		spaceMove.loop =true;
		spaceMove.Play();
	}

	public void stopSpaceMove(){
		if (spaceMove) {
			spaceMove.Stop();
		}
	}

	public void PlayStartBeam(){
		if (beamSound) {
			beamSound.Stop();
		}
		isplayingBeam = true;
		GameObject go = new GameObject ("Audio_" +  "beam/startBeam");
		go.transform.parent = m_transform;
		//Load clip from ressources folder
		AudioClip newClip =  Instantiate(Resources.Load ("beam/startBeam", typeof(AudioClip))) as AudioClip;
		
		//Add and bind an audio source
		beamSound = go.AddComponent<AudioSource>();
		beamSound.clip = newClip;
		//Play and destroy the component
		beamSound.Play();
		Destroy (go, newClip.length);

		Invoke ("playLoopBeam", beamSound.clip.length);
	}


	public void playLoopBeam(){
		/*if (beamSound) {
			beamSound.Stop();
		}*/
		if (isplayingBeam) {
			GameObject go = new GameObject ("Audio_" + "beam/Beam");
			go.transform.parent = m_transform;
			//Load clip from ressources folder
			AudioClip newClip = Instantiate (Resources.Load ("beam/Beam", typeof(AudioClip))) as AudioClip;
		
			//Add and bind an audio source
			beamSound = go.AddComponent<AudioSource> ();
			beamSound.clip = newClip;
			//Play and destroy the component
			beamSound.loop = true;
			beamSound.Play ();
		}

	}


	public void StopBeam(){
		isplayingBeam = false;
		if (beamSound) {
			beamSound.Stop();
		}
	
		GameObject go = new GameObject ("Audio_" +  "beam/EndBeam");
		go.transform.parent = m_transform;
		//Load clip from ressources folder
		AudioClip newClip =  Instantiate(Resources.Load ("beam/EndBeam", typeof(AudioClip))) as AudioClip;
		
		//Add and bind an audio source
		beamSound = go.AddComponent<AudioSource>();
		beamSound.clip = newClip;
		//Play and destroy the component
		beamSound.Play();
		Destroy (go, newClip.length);
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
