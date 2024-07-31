using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField id_IF;
    public TMP_InputField pw_IF;

    public TextMeshProUGUI notify;

    string myID, myPW;
    // Start is called before the first frame update
    void Start()
    {
        notify.text = " ";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveUserData()
    {
        if (!CheckInput(id_IF.text, pw_IF.text))
        {
            return;
        }

        
        // 아이디가 저장된것이 없다면
        if (!PlayerPrefs.HasKey(id_IF.text))
        {
            //아이디 생성
            PlayerPrefs.SetString(id_IF.text,pw_IF.text);
            notify.text = "Create User ID";
        }
        // 아이디가 저장된것이 있다면
        else
        {
            // 이미 존재하는 아이디입니다.
            notify.text = "Availability Username";
        }
    }
    public void CheckUserData()
    {
        if (!CheckInput(id_IF.text, pw_IF.text))
        {
            return;
        }
        string _pass = PlayerPrefs.GetString(id_IF.text);
        // 입력값과 유저 데이터값이 일치한다면 로그인
        if (pw_IF.text == _pass)
        {
            Debug.Log("아이디 일치");
            SceneManager.LoadScene(1);
        }
        // 일치하지않으면 에러메시지 출력
        else
        {
            notify.text = "Incorrect User Data.";
        }
    }
    bool CheckInput(string _id, string _pw)
    {
        if (_id == "" || _pw == "")
        {
            notify.text = "Please Input ID, PW";
            return false;
        }
        else
        {
            return true;
        }
    }
    /*
    public void LogInCheck()
    {
        string _curID, _curPW;
        _curID = PlayerPrefs.GetString("ID");
        _curPW = PlayerPrefs.GetString("PW");
        if (_curID != null && _curPW != null)
        {
            myID = id_IF.text;
            myPW = pw_IF.text;

            if (_curID == myID && _curPW == myPW)
            {
                Debug.Log("Login Correct");
            }
        }
    }
    public void NewSignIn()
    {
        myID = id_IF.text;
        myPW = pw_IF.text;

        PlayerPrefs.SetString("ID", myID);
        PlayerPrefs.SetString("PW", myPW);
    }
    */
}
