using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

using Gvr.Internal;
using UnityEngine.XR;
public class ui : MonoBehaviour 
{
	public GameObject menus;
	public GameObject presets;
	public GameObject[] mainMenu;
	public GameObject audio_sources;
	public AudioMixer master;
	public Text[] volume;
	public Slider[] sliders;

	public Image backgroundOFVolume;
	public Image fillOFVolume;

	public AudioSource additional_Source;
	public AudioClip[] rains;
	public AudioClip[] winds;

	public Animator holdButton;

	public GameObject background;
	public Vector3[] positions;

	public GameObject[] scene;

	void Awake()
	{
		foreach (GameObject i in scene)
		{
			i.SetActive(false);
		}
		foreach (GameObject i in mainMenu)
		{
			i.SetActive(false);
		}
		menus.SetActive(true);
		mainMenu[0].SetActive(true);
		
		presets.SetActive(false);
		audio_sources.SetActive(false);
		background.transform.position = positions[0];
	}

	public void Enter()
	{
		mainMenu[0].SetActive(false);
		mainMenu[1].SetActive(true);
	}

	IEnumerator SwitchToVR() {
		string desiredDevice = "cardboard"; 

		XRSettings.LoadDeviceByName(desiredDevice);

		yield return null;

		XRSettings.enabled = true;
	}

	IEnumerator SwitchTo2D() {
		XRSettings.LoadDeviceByName("");

		yield return null;
		Screen.orientation = ScreenOrientation.Portrait;
	}

	public void Room()
	{
		Camera.main.backgroundColor = new Color32(255,255,255,255);
		scene[0].SetActive(true);
		menus.SetActive(false);

		audio_sources.SetActive(true);
		Screen.orientation = ScreenOrientation.LandscapeLeft;

		backgroundOFVolume.color = volumeColors[0];
		fillOFVolume.color = volumeColors[0];
		StartCoroutine(SwitchToVR());
	}

	public void AllBlack()
	{
		Camera.main.backgroundColor = new Color32(0,0,0,255);
		scene[1].SetActive(true);
		menus.SetActive(false);

		audio_sources.SetActive(true);
		Screen.orientation = ScreenOrientation.LandscapeLeft;

		backgroundOFVolume.color = volumeColors[0];
		fillOFVolume.color = volumeColors[0];
		StartCoroutine(SwitchToVR());
	}

	public void AllWhite()
	{
		Camera.main.backgroundColor = new Color32(255,255,255,255);
		menus.SetActive(false);

		audio_sources.SetActive(true);
		Screen.orientation = ScreenOrientation.LandscapeLeft;

		backgroundOFVolume.color = volumeColors[0];
		fillOFVolume.color = volumeColors[0];
		StartCoroutine(SwitchToVR());
	}

	public void ToPresets()
	{
		presets.SetActive(true);
		mainMenu[0].SetActive(false);
		mainMenu[1].SetActive(false);
		background.transform.position = Vector3.Lerp(positions[0],positions[1],1f);
	}
	public void ToMenu()
	{
		presets.SetActive(false);
		mainMenu[0].SetActive(true);
		mainMenu[1].SetActive(false);
		background.transform.position = Vector3.Lerp(positions[0],positions[1],1f);
	}
	public void SetSound(float soundLevel)
	{
		master.SetFloat("MasterVol",soundLevel);
	}

	public void SetMic(float soundLevel)
	{
		master.SetFloat("mic",soundLevel);
	}

	public void SetNoise(float soundLevel)
	{
		master.SetFloat("noise",soundLevel);
	}

	public void SetAdd(float soundLevel)
	{
		master.SetFloat("additional",soundLevel);
	}

	void Update()
	{
		sliders[4] = sliders [3];
		//master
		float m;
		bool mas = master.GetFloat("MasterVol",out m);
		if(mas)
		{
			volume[0].text = m.ToString("0");
			sliders[0].value = m;
		}

		// microphone
		float mic;
		bool MPhone = master.GetFloat("mic",out mic);
		if(MPhone)
		{
			volume[1].text = mic.ToString("0");
			sliders[1].value = mic;
		}

		//noise generation volume
		float noise;
		bool NoiseGen = master.GetFloat("noise",out noise);
		if(NoiseGen)
		{
			volume[2].text = noise.ToString("0");
			sliders[2].value = noise;
		}

		//additional audio source
		float add;
		bool additional = master.GetFloat("additional",out add);
		if(additional)
		{
			volume[3].text = add.ToString("0");
			sliders[3].value = add;
		}

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			StartCoroutine(SwitchTo2D());
			BackToMenu();
		}
	}

	public void Default()
	{
		sliders[0].value = 0f;
		sliders[1].value = 0f;
		sliders[2].value = 0f;
		sliders[3].value = 0f;
	}

	public void HeavyRain()
	{
		sliders[0].value = 0f;
		sliders[1].value = -20f;
		sliders[2].value = 5f;
		sliders[3].value = 20f;	

		additional_Source.clip = rains[0];	
	}

	public void StaticSilence()
	{
		sliders[0].value = 11f;
		sliders[1].value = -14f;
		sliders[2].value = 20f;
		sliders[3].value = 6f;
	}

	public void WindyFriday()
	{
		sliders[0].value = 0f;
		sliders[1].value = -20f;
		sliders[2].value = 5f;
		sliders[3].value = 20f;

		additional_Source.clip = winds[0];
	}

	public void OnDown()
	{
		audio_sources.SetActive(true);
		holdButton.SetBool("hold",true);
		holdButton.SetBool("not_hold",false);
	}

	public void OnUp()
	{
		audio_sources.SetActive(false);
		holdButton.SetBool("not_hold",true);
		holdButton.SetBool("hold",false);
	}

	public void RandomPreset()
	{
		int index = Random.Range(0,4);

		switch(index)
		{
			case 1:
				Default();
				Room();
				break;
			case 2:
				HeavyRain();
				Room();
				break;
			case 3:
				StaticSilence();
				Room();
				break;
			case 4:
				WindyFriday();
				Room();
				break;	
		}			

	}

	public void BackToMenu()
	{
		menus.SetActive(true);
		audio_sources.SetActive(false);
		Screen.orientation = ScreenOrientation.Portrait;

		foreach (GameObject i in scene)
		{
			i.SetActive(false);
		}

		mainMenu[0].SetActive(true);
		mainMenu[1].SetActive(false);
	}

	public Color32[] volumeColors;
	public void OnScreenVolumeDown()
	{
		backgroundOFVolume.color = Color.Lerp(volumeColors[0],volumeColors[1],3.0f);
		fillOFVolume.color = Color.Lerp(volumeColors[0],volumeColors[2],3.0f);
	}

	public void OnScreenVolumeUp()
	{
		backgroundOFVolume.color = Color.Lerp(volumeColors[1],volumeColors[0],3.0f);
		fillOFVolume.color = Color.Lerp(volumeColors[2],volumeColors[0],3.0f);	
	}
}

