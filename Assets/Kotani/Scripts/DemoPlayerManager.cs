using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoPlayerManager : MonoBehaviour
{
    //プレイヤーが死んだら新しいプレイヤーを生成する
    [SerializeField]
    private GameObject PlayerPrefab;
    private GameObject Player;
    private Vector3 PlayerPoj;
    private GameObject AlivePlayer;
    private DemoPlayerControl _playerControl;
    [SerializeField]
    private Text CountText;
    private int Count;
    
    // Start is called before the first frame update
    void Start(){
      Player= GameObject.Find("Player");
      PlayerPoj = Player.transform.position;
      _playerControl = Player.GetComponent<DemoPlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_playerControl == null)
        {
            Count++;
            CountText.text = Count.ToString();
            PlayerReborn();
        }
    }

    private void PlayerReborn()
    {
        AlivePlayer = Instantiate(PlayerPrefab, PlayerPoj, Quaternion.identity);
        AlivePlayer.transform.parent = Player.transform;
        AlivePlayer.transform.localScale = new Vector2(1,1);
        _playerControl = AlivePlayer.GetComponent<DemoPlayerControl>();
    }
}
