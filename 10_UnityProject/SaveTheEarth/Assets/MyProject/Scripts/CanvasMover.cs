using System.Collections;
using UnityEngine;

public class CanvasMover : MonoBehaviour
{
    [SerializeField]Transform canvasPos;
    float time = 1.0f;
    float current;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void  FixedUpdate()
    {
            //2秒後に正面にキャンバスが移動するよう滑らかに移動

            transform.position = Vector3.Lerp(transform.position, canvasPos.position, 2.0f * Time.deltaTime);
           // transform.rotation = Quaternion.Slerp(transform.rotation, canvasPos.rotation, 2.0f);

    }
}
