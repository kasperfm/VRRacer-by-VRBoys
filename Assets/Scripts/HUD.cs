using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    private GameHandler gameHandler;
    public Text spedometer;
    // Use this for initialization
    void Start () {
        gameHandler = FindObjectOfType<GameHandler>();
	}
	
	// Update is called once per frame
	void Update () {
	    spedometer.text = System.Math.Round(gameHandler.speed, 0).ToString();
    }
}
