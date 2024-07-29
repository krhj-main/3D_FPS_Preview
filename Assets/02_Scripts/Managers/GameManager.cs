using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager gm = null;

   

    public GameState gState;

    public GameObject gameLabel;
    TextMeshProUGUI gameText;
    PlayerMove player;
    public enum GameState
    {
        Ready,
        Run,
        GameOver,
    }

    private void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
        gState = GameState.Ready;
        gameText= gameLabel.GetComponent<TextMeshProUGUI>();

        gameText.text = "Ready...";
        gameText.color = new Color32(255,185,0,255);
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();

        StartCoroutine(ReadyToStart());
    }

    // Update is called once per frame
    void Update()
    {
        
        if (player.hp <= 0)
        {
            gameLabel.SetActive(true);
            gameText.text = "GameOver";
            gameText.color = new Color32(255,0,0,255);
            player.GetComponentInChildren<Animator>().SetFloat("MoveMotion",0);

            gState = GameState.GameOver;
        }
        
    }
    IEnumerator ReadyToStart()
    {
        yield return new WaitForSeconds(1f);
        gameText.text = "3";
        yield return new WaitForSeconds(1f);

        gameText.text = "2";
        yield return new WaitForSeconds(1f);

        gameText.text = "1";

        yield return new WaitForSeconds(1f);
        gameText.text = "Go !";
        yield return new WaitForSeconds(0.5f);

        gameLabel.SetActive(false);

        gState = GameState.Run;
    }
}
