using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	//@@@TAKEN FROM:https://www.daggerhartlab.com/unity-audio-and-sound-manager-singleton-script/
	//Improved with enum call by: sbmeteturkay

	// Audio players components.
	[SerializeField] AudioSource EffectsSource;
	[SerializeField] AudioSource[] EffectsSourceList;
	[SerializeField] AudioSource UIEffectsSource;
	[SerializeField] AudioSource[] UIEffectsSourceList;
	[SerializeField] AudioSource MusicSource;
	// Random pitch adjustment range.
	[SerializeField] float LowPitchRange = .95f;
	[SerializeField] float HighPitchRange = 1.05f;
	// Singleton instance.
	public static SoundManager Instance = null;
	// Initialize the singleton instance.

	[Tooltip("collectCoin,upgrade,buy,notEnoughMoney")]
	public AudioClip[] audioClips;
	public enum Sounds
	{
		collectCoin,
		upgrade,
		buy,
		notEnoughMoney,
		selected,
		leafFall,
		treeFall,
		treeDestroy,
		tradeCoin
	}
	private void Awake()
	{
		// If there is not already an instance of SoundManager, set it to this.
		if (Instance == null)
		{
			Instance = this;
		}
		//If an instance already exists, destroy whatever this object is to enforce the singleton.
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
		//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
		DontDestroyOnLoad(gameObject);
	}
	// Play a single clip through the sound effects source.
	public void Play(AudioClip clip)
	{
		CheckAvailableSource().clip = clip;
		CheckAvailableSource().Play();
	}

	// Play a single clip through the music source.
	public void PlayMusic(AudioClip clip)
	{
		MusicSource.clip = clip;
		MusicSource.Play();
	}
	public void Play(Sounds sound,bool ui ,bool pitchRandom = false)
	{
		if (ui)
		{
			CheckUIAvailableSource().pitch = 1;
			if (CheckUIAvailableSource().clip != audioClips[(int)sound])
            {
				CheckUIAvailableSource().Stop();
				CheckUIAvailableSource().clip = audioClips[(int)sound];
				CheckUIAvailableSource().Play();
            }
            else
            {
				//CheckUIAvailableSource().PlayDelayed(0.1f);
				
			}
		}
		else
		{
			CheckAvailableSource().clip = audioClips[(int)sound];
			if (pitchRandom)
			{
				float randomPitch = Random.Range(LowPitchRange, HighPitchRange);
				CheckAvailableSource().pitch = randomPitch;
			}
			CheckAvailableSource().Play();
		}
	}
	public void Play(AudioClip sound, bool ui, bool pitchRandom = false)
	{
		if (ui)
		{
			CheckUIAvailableSource().pitch = 1;
			if (CheckUIAvailableSource().clip != sound)
				CheckUIAvailableSource().clip = sound;
			CheckUIAvailableSource().Play();
		}
		else
		{
			CheckAvailableSource().clip = sound;
			if (pitchRandom)
			{
				float randomPitch = Random.Range(LowPitchRange, HighPitchRange);
				CheckAvailableSource().pitch = randomPitch;
			}
			CheckAvailableSource().Play();
		}

	}
	// Play a random clip from an array, and randomize the pitch slightly.
	public void RandomSoundEffect(params AudioClip[] clips)
	{
		int randomIndex = Random.Range(0, clips.Length);
		float randomPitch = Random.Range(LowPitchRange, HighPitchRange);
		CheckAvailableSource().pitch = randomPitch;
		CheckAvailableSource().clip = clips[randomIndex];
		CheckAvailableSource().Play();
	}
	AudioSource CheckAvailableSource()
    {
		foreach(AudioSource audio in EffectsSourceList)
        {
			if (!audio.isPlaying)
				return audio;
        }
		//Debug.Log("not have any source");
		return EffectsSource;
    }
	AudioSource CheckUIAvailableSource()
	{
		foreach (AudioSource audio in UIEffectsSourceList)
		{
			if (!audio.isPlaying)
				return audio;
		}
		//Debug.Log("not have any source");
		return UIEffectsSource;
	}
}
