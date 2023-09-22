using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Login : MonoBehaviour
{
    [SerializeField] InputField nameIF;
    [SerializeField] InputField passwordIF;
    public UserData user = null;
    public bool IsLoggedIn => this.user != null;

    public void OnClickLogin()
    {

    }

    public void Set(UserData user)
    {
        this.user = user;
    }
    public void SetActive(bool isActive)
    {
        gameObject.SetActive(true);
    }
}