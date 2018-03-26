using System;
using UnityEngine;

public class noise : MonoBehaviour {

    [Range(-10f, 10f)]
    public float offset;
    
    System.Random rand = new System.Random();
    
    void OnAudioFilterRead(float[] data, int channels) {
        for (int i = 0; i < data.Length; i++) {
            data[i] = (float)(rand.NextDouble() * 2.0 - 1.0 + offset);
        }
    }

	//https://www.mcvuk.com/development/procedural-audio-with-unity
	//https://support.unity3d.com/hc/en-us/articles/206485253-How-do-I-get-Unity-to-playback-a-Microphone-input-in-real-time-
    //https://www.gamasutra.com/blogs/JoeStrout/20170223/292317/Procedural_Audio_in_Unity.php
	
}
