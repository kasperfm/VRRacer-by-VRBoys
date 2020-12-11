using UnityEngine;
using System.Collections;

public class DeathCheck : MonoBehaviour {

    Vector3 origPos;
    Quaternion origRot;
    GameHandler gh;

    void Start() {
        origPos = transform.position;
        origRot = transform.rotation;
        gh = FindObjectOfType<GameHandler>();
    }

	// Update is called once per frame
	void Update () {
	    if(transform.position.y <= -50f) {
            Die();
        }
	}

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Wall")) {
            Die();
        }
    }

    void Die() {
        if (gh.racing) {
            Debug.Log("YOU DIED");
            transform.position = origPos;
            transform.rotation = origRot;
            gh.Restart();
        }
    }
}
