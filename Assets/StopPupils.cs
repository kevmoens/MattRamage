using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class StopPupils : MonoBehaviour {
    public GameObject Eye1;
    public GameObject pupil1;
    public CircleEdgeCollider2D EyeCollider1;
    public GameObject Eye2;
    public GameObject pupil2;
    public CircleEdgeCollider2D EyeCollider2;
    public UnityEngine.UI.Text buttonText;
    public UnityEngine.UI.Text messageText;
    public VideoPlayer video;
    public GameObject videoRenderer;
    private bool IsPlaying = true;
    private Vector3 pupil1OrigTransform;
    private Vector3 pupil2OrigTransform;
    private float videoTimeRemaining;
	// Use this for initialization
	void Start () {
        pupil1OrigTransform = pupil1.transform.position;
        pupil2OrigTransform = pupil2.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (videoTimeRemaining <= 0)
        {
            videoRenderer.GetComponent<Renderer>().enabled = false;
        }	else
        {
            videoTimeRemaining -= Time.deltaTime;
        }	
	}
    public void PupilStop()
    {
        var rbPupil1 = pupil1.GetComponent<Rigidbody2D>();
        var rbPupil2 = pupil2.GetComponent<Rigidbody2D>();
        if (IsPlaying)
        {
            var dis1 = Eye1.transform.position - pupil1.transform.position;
            var dis2 = Eye2.transform.position - pupil2.transform.position;
            Debug.Log(System.Math.Abs(dis1.x).ToString() + ":" + System.Math.Abs(dis1.y).ToString() + ":" + System.Math.Abs(dis2.x).ToString() + ":" + System.Math.Abs(dis2.y).ToString());
            var total = System.Math.Abs(dis1.x) + System.Math.Abs(dis1.y) + System.Math.Abs(dis2.x) + System.Math.Abs(dis2.y);
            messageText.text = total.ToString();
            rbPupil1.constraints = RigidbodyConstraints2D.FreezeAll;
            rbPupil2.constraints = RigidbodyConstraints2D.FreezeAll;
            buttonText.text = "Start";
            videoTimeRemaining = 17f;
            videoRenderer.GetComponent<Renderer>().enabled = true;
            video.Play();
        } else
        {
            rbPupil1.constraints = RigidbodyConstraints2D.None;
            rbPupil2.constraints = RigidbodyConstraints2D.None;
            pupil1.transform.position = pupil1OrigTransform;
            pupil2.transform.position = pupil2OrigTransform;
            float x1 = 0, y1 = 0, x2 = 0, y2 = 0;
            PupilForce.GetForce(ref x1, ref x1, EyeCollider1.Radius);
            pupil1.GetComponent<Rigidbody2D>().AddForce(new Vector2(x1, y1));
            PupilForce.GetForce(ref x2, ref x2, EyeCollider2.Radius);
            pupil2.GetComponent<Rigidbody2D>().AddForce(new Vector2(x2, y2));
            buttonText.text = "Stop";
        }
        IsPlaying = !IsPlaying;
    }
}
