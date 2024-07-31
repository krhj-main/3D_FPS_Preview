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

        
        // ���̵� ����Ȱ��� ���ٸ�
        if (!PlayerPrefs.HasKey(id_IF.text))
        {
            //���̵� ����
            PlayerPrefs.SetString(id_IF.text,pw_IF.text);
            notify.text = "Create User ID";
        }
        // ���̵� ����Ȱ��� �ִٸ�
        else
        {
            // �̹� �����ϴ� ���̵��Դϴ�.
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
        // �Է°��� ���� �����Ͱ��� ��ġ�Ѵٸ� �α���
        if (pw_IF.text == _pass)
        {
            Debug.Log("���̵� ��ġ");
            SceneManager.LoadScene(1);
        }
        // ��ġ���������� �����޽��� ���
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
