using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FPS : MonoBehaviour {
    Text fpsTxt;
    float currentTime;
    float fps;
    // Use this for initialization
    void Start () {
        fpsTxt = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        fps = 1f / Time.deltaTime;
        currentTime += Time.deltaTime;
        if (currentTime > 0.5f)
        {
            fpsTxt.text = Mathf.Round(fps).ToString() + "fps";
            currentTime = 0;
        }
    }
}
