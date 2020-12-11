using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameHandler: MonoBehaviour {
    public float speed = 160.0f;
    private float origSpeed = 0;

	public int shield = 100;

    public bool steeringFromSpeed = false;

    private int trackLayer;

    public AudioClip count0;
    public AudioClip count1;
    public AudioClip count2;
    public AudioClip count3;

    public AudioSource engineSounds;

    public AudioClip engineStart;
    public AudioClip engineRunning;
    

    public bool racing = false;
    private float banking = 0;

    // Use this for initialization
    void Start () {
        origSpeed = speed;
        trackLayer = 1 << LayerMask.NameToLayer("Track");
        StartCoroutine(waitAndGo());
    }

    public void Restart() {
        racing = false;
        speed = origSpeed;
        StartCoroutine(waitAndGo());
    }

    IEnumerator waitAndGo() {
        FindObjectOfType<MusicHandler>().audioPlayer.Stop();
        GetComponent<AudioSource>().PlayOneShot(engineStart);
        engineSounds.clip = engineRunning;
        engineSounds.Play();
        yield return new WaitForSeconds(1.5f);
        GetComponent<AudioSource>().PlayOneShot(count3);
        yield return new WaitForSeconds(1f);
        GetComponent<AudioSource>().PlayOneShot(count2);
        yield return new WaitForSeconds(1f);
        FindObjectOfType<MusicHandler>().turnUpMusic = true;
        GetComponent<AudioSource>().PlayOneShot(count1);
        yield return new WaitForSeconds(1f);
        GetComponent<AudioSource>().PlayOneShot(count0);
        yield return new WaitForSeconds(0.15f);
        racing = true;
    }

    public static float ClampAngle(
    float currentValue,
    float minAngle,
    float maxAngle,
    float clampAroundAngle = 0
) {
        float angle = currentValue - (clampAroundAngle + 180);

        while (angle < 0) {
            angle += 360;
        }

        angle = Mathf.Repeat(angle, 360);

        return Mathf.Clamp(
            angle - 180,
            minAngle,
            maxAngle
        ) + 360 + clampAroundAngle;
    }

    void LateUpdate() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, trackLayer)) {
            Quaternion newRotation = Quaternion.LookRotation(transform.forward, hit.normal.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 0.1f); 
        }

        Quaternion headRot = Cardboard.SDK.HeadPose.Orientation;
        float test = headRot.eulerAngles.z * 0.5f;

        float speedVSturning = 2.0f;

        if (steeringFromSpeed) {
            speedVSturning = speed / 90;
        }

		/*if(speed < 200){
			speedVSturning = 1.75f;
		}else{
			speedVSturning = 2.0f;
		}
*/
        transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * speedVSturning, 0), Space.World);

        if (test > 90) {
            test = test - 180;
        }

        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + ((test * 0.01f) * -1f), transform.eulerAngles.z);

        //  transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z); ORIGINAL

        banking -= Input.GetAxis("Horizontal") * Time.deltaTime * 20f;
        banking = Mathf.Lerp(banking, 0.0f, 0.1f);
        transform.rotation *= Quaternion.Euler(new Vector3(0, 0, banking));

        if (racing) {
            transform.Translate(transform.forward * (speed * Time.deltaTime), Space.World);

            if (speed <= 700.0f)
                speed += 0.015f;

            if (Input.GetButton("Cross")) { // Circle on Android
                if (speed <= 700.0f)
                    speed += 0.2f;
            }

            if (Input.GetButton("Square")) { // Cross on Android
                if (speed >= 50.0f)
                    speed -= 0.2f;
            }
        }

        
    }
}
