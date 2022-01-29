using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour {

	const string MASTER_VOLUME_KEY = "master_volume";
	const string MUSIC_VOLUME_KEY = "music_volume";
	const string SFX_VOLUME_KEY = "sfx_volume";

	public static void SetMasterVolume(float volume) {
		if (volume >= -40f && volume <= 1.000001f)
		{
			PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
		} else {
			Debug.LogError("Master volume out of range");
		}
	}

	public static float GetMasterVolume(){
		return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
	}

	public static void SetMusicVolume(float volume){
		if (volume >= -40f && volume <= 1.000001f)
		{
			PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, volume);
		} else{
			Debug.LogError("Music volume out of range");
		}
	}

	public static float GetMusicVolume(){
		return PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY);
	}

	public static void SetSFXVolume(float volume){
		if (volume >= -40f && volume <= 1.000001f){
			PlayerPrefs.SetFloat(SFX_VOLUME_KEY, volume);
		} else{
			Debug.LogError("SFX volume out of range");
		}
	}

	public static float GetSFXVolume(){
		return PlayerPrefs.GetFloat(SFX_VOLUME_KEY);
	}

	public static void DeleteAllPlayerPrefs() {
		PlayerPrefs.DeleteAll();
	}
}
