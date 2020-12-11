using UnityEngine;
using System.Collections;

public class RotateToTrack : MonoBehaviour {

    public GameObject target;
    float curDir = 0f; // compass indicating direction
    Vector3 curNormal = Vector3.up; // smoothed terrain normal
    public float turn;

    // Update is called once per frame
    void Update() {

        curDir = (curDir + turn) % 360; // rotate angle modulo 360 according to input
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -Vector3.up, out hit, 5, 1 << LayerMask.NameToLayer("Track"))) {
            curNormal = Vector3.Lerp(curNormal, hit.normal, 4 * Time.deltaTime);
            Quaternion grndTilt = Quaternion.FromToRotation(Vector3.up, curNormal);
            target.transform.rotation = grndTilt * Quaternion.Euler(0, curDir, 0);
        }
    }
}
