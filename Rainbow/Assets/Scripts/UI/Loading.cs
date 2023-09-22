using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] GameObject LoadingGO;
    [SerializeField] Login login;


    private void Awake()
    {
        if (login.IsLoggedIn)
        {
            LoadingGO.SetActive(true);
        }
        else
        {
            login.SetActive(true);
        }
    }
    // bg켜저있도록 
    //
    // 로그인 전,
    // 로그인 패널
    //
    // 로그인 되어 있음,
    // 로비 씬 로딩
    // 로딩 후 bg 패널 끔
    // 
    // 
    // 실 로딩일 때
    // 
}