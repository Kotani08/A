using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class FadeManager : MonoBehaviour
{
    #region やりたいこと 役割
    /*
    外部から呼ばれたときに
    既定の座標Ａから既定の座標Ｂまで移動をする
    既定の座標Ｂについたら停止
        →次のシーンを呼び出す
    また呼ばれたときに既定の座標Ｃまで移動
    移動が完了し終わったら座標Ａに戻る
    */
    #endregion

    //移動するシーン名
    [SerializeField]
    private string sceneName;

    //動かすFade用のオブジェクト
    [SerializeField]
    private GameObject fadeObj;

    //現在がFadeInかOutかを判断するためのbool
    [SerializeField]
    private bool fadeFlag = true;
    [SerializeField]
    private bool fadeStopFlag = true;

    public bool SetfadeStopFlag(bool set)
    {
        return fadeStopFlag = set;
    }

    //フェードイン前の座標
    private Vector3 fadeInBeforeVec = new Vector3(-1200,1200,0);
    //フェードイン後の座標
    private Vector3 fadeInAfterVec = new Vector3(0,0,0);
    //フェードアウト後の座標
    private Vector3 fadeOutAfterVec = new Vector3(1200,-1200,0);

    // Start is called before the first frame update
    void Start()
    {
        fadeObj.transform.localPosition = fadeInBeforeVec;
    }

    // Update is called once per frame
    void Update()
    {
        //Stopしていないときに処理
        if(fadeStopFlag == false)
        {
            if(fadeFlag == true)
            {
                FadeIn();
            }
            else if(fadeFlag == false)
            {
                FadeOut();
            }
        }
        if (Input.GetKeyDown(KeyCode.U)){SceneChanger();}
        if (Gamepad.current != null)
      {
        if(Gamepad.current.leftShoulder.wasPressedThisFrame){SceneChanger();}
      }
    }

    //fadeFlag == true
    private void FadeIn()
    {
        //既定の位置になったら停止（StopFlag = true）
        if(fadeObj.transform.localPosition.y <= fadeInAfterVec.y)
        {
            //fadeFlagをfalseに変更
            fadeStopFlag = true;
            fadeFlag = false;
        }
        else
        {
        //位置をfadeInAfterVecに近づける
        Vector3 SetPoj = fadeObj.transform.localPosition;
        fadeObj.transform.localPosition = SetPoj + new Vector3(10,-10,0);
        }
    }

    //fadeFlag == false
    private void FadeOut()
    {
        //既定の位置になったら停止（StopFlag = true）
        if(fadeObj.transform.localPosition.y <= fadeOutAfterVec.y)
        {
            //fadeFlagをfalseに変更
            fadeStopFlag = true;
            fadeFlag = true;
            fadeObj.transform.localPosition = fadeInBeforeVec;
        }
        else
        {
        //位置をfadeInAfterVecに近づける
        Vector3 SetPoj = fadeObj.transform.localPosition;
        fadeObj.transform.localPosition = SetPoj + new Vector3(10,-10,0);
        }
    }

    //SceneChanger
    public void SceneChanger()
    {
        switch(SceneManager.GetActiveScene().name)
        {
             case "title":
            sceneName = "TutorialScene";
            break;
            case "TutorialScene":
            sceneName = "TestOutScene";
            break;
            case "TestOutScene":
            sceneName = "TestOutScene 1";
            break;
            case "TestOutScene 1":
            sceneName = "TestOutScene 3";
            break;
            case "TestOutScene 3":
            sceneName = "TestOutScene 2";
            break;
            case "TestOutScene 2":
            sceneName = "TestOutScene 4";
            break;
            default:
            break;
        }
        SceneManager.LoadScene(sceneName);
        fadeStopFlag=false;
    }
}
