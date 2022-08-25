using UnityEngine;
public class SoundTest : MonoBehaviour
{
    //Manager呼び出し
    [SerializeField]
    SoundManager soundManager;

    //最初から流れているBGMくん
    [SerializeField]
    AudioClip StartBGM;

    //ダメージ音
    [SerializeField]
    AudioClip DamageSE;
    [SerializeField]
    AudioClip ClearSE;

    public int HP = 100;
    void Start()
    {
        soundManager.PlayBgm(StartBGM);
    }

    private void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.Escape))
        {
            HP -= 10;
            soundManager.PlaySe(DamageSE);
        }*/
    }
}