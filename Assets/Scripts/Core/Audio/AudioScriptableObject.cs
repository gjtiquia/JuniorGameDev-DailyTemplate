using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AudioList")]
public class AudioScriptableObject : ScriptableObject
{
    public List<AudioData> audioList = new List<AudioData>();
}

[System.Serializable]
public class AudioData
{
    public string name;
    public AudioClip clip;
    public AudioData(string name, AudioClip clip)
    {
        this.name = name;
        this.clip = clip;
    }
}