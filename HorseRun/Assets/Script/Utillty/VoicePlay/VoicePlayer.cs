using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(AudioListener))]
public class VoicePlayer : FSingleton.SingletonMono<VoicePlayer> {

    private bool _canPlay = true;
    private Queue<string> _audioQueue = new Queue<string>();
    private AudioListener audioListener = null;
    private AudioSource audioSource = null;

    private void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.volume = 1;
        audioSource.playOnAwake = false;
        audioListener = this.GetComponent<AudioListener>();
    }

    // Update is called once per frame
    void Update () {
        if (_canPlay)
        {
            if (!audioSource.isPlaying)
            {

            }
        }
	}

    public void Say(string url)
    {
        StartCoroutine(Load(url));    
    }

    IEnumerator Load(string url)
    {
        FileInfo fileInfo = new FileInfo(url);
        if (fileInfo.Exists)
        {
            string str = fileInfo.FullName;
            WWW www = new WWW("file://" + str);
            yield return www;
            if (www.error != null) Debug.Log(www.error + fileInfo.FullName);
            else
            {
                audioSource.clip = www.GetAudioClip();
                if (audioSource.clip.loadState == AudioDataLoadState.Loaded)
                {
                    audioSource.Play();
                }
            }
        }
    }
}
