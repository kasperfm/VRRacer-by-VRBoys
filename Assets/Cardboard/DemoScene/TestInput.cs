using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TestInput : MonoBehaviour {

    private Text text;

    void Awake() {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButton("Cross")) {
            text.text = "X PRESSED";
        }
	}
}
