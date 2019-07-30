using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour {

    public GameObject rightController;
    public GameObject player;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        rightController.transform.rotation = transform.rotation;

        //プレイヤーの操作
        //回転
        if (Input.GetKey(KeyCode.R))
            transform.Rotate(new Vector3(-1f, 0f, 0f));
        if (Input.GetKey(KeyCode.F))
            transform.Rotate(new Vector3(1f, 0f, 0f));
        if (Input.GetKey(KeyCode.Q))
            transform.Rotate(new Vector3(0f, -1f, 0f));
        if (Input.GetKey(KeyCode.E))
            transform.Rotate(new Vector3(0f, 1f, 0f));

        //移動
        if (Input.GetKey(KeyCode.W))
        {
            player.transform.Translate(new Vector3(0f, 0f, 1f * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.S))
        {
            player.transform.Translate(new Vector3(0f, 0f, -1f * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.A))
        {
            player.transform.Translate(new Vector3(-1f * Time.deltaTime, 0f, 0f));
        }
        if (Input.GetKey(KeyCode.D))
        {
            player.transform.Translate(new Vector3(1f * Time.deltaTime, 0f, 0f));
        }

        //コントローラーの移動
        if (Input.GetKey(KeyCode.LeftArrow))
            rightController.transform.Translate(new Vector3(-1f * Time.deltaTime, 0f, 0f));
        if (Input.GetKey(KeyCode.RightArrow))
            rightController.transform.Translate(new Vector3(1f * Time.deltaTime, 0f, 0f));
        if (Input.GetKey(KeyCode.UpArrow))
            rightController.transform.Translate(new Vector3(0f, 0f, 1f * Time.deltaTime));
        if (Input.GetKey(KeyCode.DownArrow))
            rightController.transform.Translate(new Vector3(0f, 0f, -1f * Time.deltaTime));

        //位置をリセット
        if (Input.GetKey(KeyCode.Space))
        {
            string hmdname;
            //hmdname = ovr
            transform.position = new Vector3(0, 1, 0);
            rightController.transform.position = new Vector3(0,1,0); 
        }

    }
}
