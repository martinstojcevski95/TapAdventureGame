using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseAuthentication : MonoBehaviour
{

    [SerializeField] InputField RegisterEmailAddress,SignInEmailAddress;
    [SerializeField] InputField RegisterPassword,SignInPassword;
    [SerializeField] Text Log;
    [SerializeField] Color successColor, ErrorColor;
    [SerializeField] Canvas FullView,InGameView;
    [SerializeField] GameObject RegisterView, SignInView;
    [SerializeField] Toggle checkBox;
    [SerializeField] GameObject firstMenuToShow;
    string Registeremailaddress, Registerpass,SignInemailaddress,SignInpass;
    AuthType authType;
    int isRegistered; //0 = false , 1 = true
    int rememberMe; //0 = false, 1 = true

    private void Start()
    {
        //PlayerPrefs.DeleteKey("REGISTERED");
        //PlayerPrefs.DeleteKey("EMAIL");
        //PlayerPrefs.DeleteKey("PASS");
        isRegistered = PlayerPrefs.GetInt("REGISTERED"); //is registered go to sign in
        rememberMe = PlayerPrefs.GetInt("REMEMBERME"); // remember the user info
        SignInemailaddress = PlayerPrefs.GetString("EMAIL"); //remembers the email
        SignInpass = PlayerPrefs.GetString("PASS"); // remembers the pass

        if (isRegistered.Equals(1))
        {
            RegisterView.SetActive(false);
            SignInView.SetActive(true);
        }
        else if(isRegistered.Equals(0))
        {
            RegisterView.SetActive(true);
            SignInView.SetActive(false);
        }
        if (rememberMe.Equals(1))
        {
            SignInEmailAddress.text = SignInemailaddress;
            SignInPassword.text = SignInpass;
            checkBox.isOn = true;
        }
        else if(rememberMe.Equals(0))
        {
            PlayerPrefs.DeleteKey("EMAIL");
            PlayerPrefs.DeleteKey("PASS");
            checkBox.isOn = false;
        }
    }

    

   public void RememberInfo()
    {
      
        if (checkBox.isOn)
        {
            PlayerPrefs.SetInt("REMEMBERME", 1);
            rememberMe = PlayerPrefs.GetInt("REMEMBERME"); //REMEMBERS THE EMAIL AND THE PASSWORD;
        }
        else if(!checkBox.isOn)
        {
            PlayerPrefs.SetInt("REMEMBERME", 0);
        }
    }

    //  PlayerPrefs.SetInt("JavaScript", JavaScriptToggle.isOn ? 1 : 0);
    public void CreateUser()
    {
        authType = AuthType.Register;
        Registeremailaddress = RegisterEmailAddress.text;
        Registerpass = RegisterPassword.text;

        var Auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        if (Auth != null)
        {
            Auth.CreateUserWithEmailAndPasswordAsync(Registeremailaddress, Registerpass).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.Log("Creating User With Email and Password was canceled");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("Create User With Email And Password Async encountered an error: " + task.Exception);
                    StartCoroutine(CustomLog(false));
                    return;
                }


                //User Has Been Created
                Firebase.Auth.FirebaseUser newUser = task.Result;
                StartCoroutine(CustomLog(true));
                Debug.LogFormat("Firebase user created successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
                PlayerPrefs.SetInt("REGISTERED", 1); //set registration to true
                
                RegisterView.SetActive(false);
                SignInView.SetActive(true);

            });
        }
    }

    public void SignIn()
    {
        authType = AuthType.SignIn;
        SignInemailaddress = SignInEmailAddress.text;
        SignInpass = SignInPassword.text;

        var Auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        if(Auth != null)
        {
            Auth.SignInWithEmailAndPasswordAsync(SignInemailaddress, SignInpass).ContinueWith(task =>
           {
               if (task.IsCanceled)
               {
                   Debug.LogError("SignIn With Email And PasswordAsync was canceled.");
                   return;
               }
               if (task.IsFaulted)
               {
                   Debug.LogError("SignIn With Email And PasswordAsync encountered an error: " + task.Exception);
                   StartCoroutine(CustomLog(false));
                   return;
               }

               Firebase.Auth.FirebaseUser newUser = task.Result;
               StartCoroutine(CustomLog(true));
               Debug.LogFormat("User signed in successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
               PlayerPrefs.SetString("EMAIL", SignInemailaddress); //sets the email
               PlayerPrefs.SetString("PASS", SignInpass); //sets the pass


               StartCoroutine(IsInfoClosed());
               AdventurePoint adp = (AdventurePoint)FindObjectOfType(typeof(AdventurePoint));
               adp.enabled = true;
             
           });
        } 
    }

    IEnumerator IsInfoClosed()
    {
        if(GameManager._Instance.firstTime <= 0)
        {
            yield return new WaitUntil(() => GameManager._Instance.canContinue);
            FullView.enabled = false;
            InGameView.enabled = true;
            firstMenuToShow.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(0.3f);
            FullView.enabled = false;
            InGameView.enabled = true;
            firstMenuToShow.SetActive(true);
        }

 

    }

    IEnumerator CustomLog(bool success)
    {
        if(authType == AuthType.Register)
        {
            if (success)
            {
                Log.GetComponent<Text>().color = successColor;
                Log.text = "user created successfully";
            }
            else
            {
                Log.GetComponent<Text>().color = ErrorColor;
                Log.text = "User with email address " + Registeremailaddress + " already exists";
            }
            yield return new WaitForSeconds(2f);
            Log.text = "";
        }

        if(authType == AuthType.SignIn)
        {
            if (success)
            {
                Log.GetComponent<Text>().color = successColor;
                Log.text = "user signed in successfully";
            }
            else
            {
                Log.GetComponent<Text>().color = ErrorColor;
                Log.text = "wrong password or email address";
            }
            yield return new WaitForSeconds(2f);
            Log.text = "";
            SignInView.SetActive(false);
            RegisterView.SetActive(false);
            GameManager._Instance.SetInfo();

        }
    }

    enum AuthType
    {
        Register,
        SignIn
    }
}
