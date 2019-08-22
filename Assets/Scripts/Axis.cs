using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axis : MonoBehaviour{
    float angleUp = 60f;
    float angleDown = -60f;                   //X軸角度の制限範囲

    [SerializeField] GameObject player;
    [SerializeField] Camera came;
    [SerializeField] GameObject unityChan;    //各Object格納場所

    [SerializeField] float rotate_speed = 3;  //Cameraの回転スピード
    [SerializeField] Vector3 axisPos;         //Axisの位置の指定
    [SerializeField] Vector3 thirdPos;        //三人称のCamera位置
    [SerializeField] Vector3 thirdPosAdd;     //三人称の際のCameraにスクロールで伸びた距離を足した値を入れる
    [SerializeField] float scroll;            //マウスホイールの値
    [SerializeField] float scrollLog;         //マウスホイールの値を保存

    [SerializeField] bool switching = true;   //三人称と一人称の切り替え
    [SerializeField] bool toFirst = false;    //一度だけ一人称に
    [SerializeField] bool toThird = false;    //一度だけ三人称に

    private Vector3 velocity = Vector3.zero;
    [SerializeField] float smoothTime = 0.5f;

    void Start(){
        came.transform.localPosition = new Vector3(0, 0, -3);
        toFirst = true;
    }

    void Update(){
        transform.position = player.transform.position + axisPos;
        thirdPosAdd = thirdPos + new Vector3(0, 0, scrollLog);

        if (Input.GetKeyDown(KeyCode.F)){
            switching = !switching;
        }

        if (switching){
            unityChan.SetActive(true);
            if (toThird){
                came.transform.localPosition = Vector3.SmoothDamp(
                    came.transform.localPosition,
                    new Vector3(0, 0,
                    thirdPosAdd.z),
                    ref velocity,
                    smoothTime);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    player.transform.rotation,
                    Time.deltaTime);
                Invoke("ToThirdSwitch", 2);
            }

            ThirdPersonMode();
        }
        else{
            unityChan.SetActive(false);
            if (toFirst){
                came.transform.position = Vector3.SmoothDamp(
                    came.transform.position,
                    transform.position,
                    ref velocity,
                    smoothTime);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    player.transform.rotation,
                    Time.deltaTime);
                Invoke("ToFirstSwitch", 2);
            }

            FirstPersonMode();
        }

        transform.eulerAngles += new Vector3(
            Input.GetAxis("Mouse Y") * rotate_speed,
            Input.GetAxis("Mouse X") * rotate_speed
            , 0);

        float angle_x = transform.eulerAngles.x;
        if (angle_x >= 180){
            angle_x = angle_x - 360;
        }
        transform.eulerAngles = new Vector3(
            Mathf.Clamp(angle_x, angleDown, angleUp),
            transform.eulerAngles.y,
            transform.eulerAngles.z
        );
    }

    private void ThirdPersonMode(){
        scroll = Input.GetAxis("Mouse ScrollWheel");
        scrollLog += Input.GetAxis("Mouse ScrollWheel");

        came.transform.localPosition
            = new Vector3(came.transform.localPosition.x,
            came.transform.localPosition.y,
            came.transform.localPosition.z + scroll);
    }

    private void FirstPersonMode(){
        player.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    void ToThirdSwitch(){
        toThird = false;
        toFirst = true;
    }

    void ToFirstSwitch(){
        toThird = true;
        toFirst = false;
    }
}