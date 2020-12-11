using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicHandler : MonoBehaviour {

    public List<AudioClip> audiotracks = new List<AudioClip>();
    private List<AudioClip> finalSoundtrack = new List<AudioClip>();
    public AudioSource audioPlayer;
    public bool turnUpMusic = false;
    private float audio2Volume = 0.2f;

    // Use this for initialization
    void Start () {
        audioPlayer = GetComponent<AudioSource>();
        finalSoundtrack = ShuffleList(audiotracks);
    }

	// Update is called once per frame
	void Update () {

        if (turnUpMusic) {
            fadeIn();
        }

        if (!audioPlayer.isPlaying) {
            audioPlayer.clip = finalSoundtrack[Random.Range(0, finalSoundtrack.Count)];
            audioPlayer.Play();
        }
    }

    public void fadeIn() {
        if (audio2Volume < 1) {
            audio2Volume += 0.1f * Time.deltaTime;
            audioPlayer.volume = audio2Volume;
        }
    }

    public static List<AudioClip> ShuffleList(List<AudioClip> aList) {
        System.Random _random = new System.Random();

        AudioClip myGO;

        int n = aList.Count;
        for (int i = 0; i < n; i++) {
            int r = i + (int)(_random.NextDouble() * (n - i));
            myGO = aList[r];
            aList[r] = aList[i];
            aList[i] = myGO;
        }

        return aList;
    }
}
