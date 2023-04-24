using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Server : MonoBehaviour
{
    //fields of inputfield email and password of login page
    [SerializeField] TMP_InputField email_InputField, password_InputField;
    //the location of the index.php to make the connection
    [SerializeField] string url;
    // login button :)
    [SerializeField] Button loginBtn;
    //the from we sent to index.php file to make request to database
    WWWForm form;
    //this function call when we click on login button
    public void OnLoginButtonClicked()
    {
        //make login button unclickable to ignore errors may happen if we click multiple time
        loginBtn.interactable = false;
        //call Login function
        StartCoroutine(Login());
    }
    IEnumerator Login()
    {
        form = new WWWForm();
        //Add fields of email and password to form 
        form.AddField("email", email_InputField.text);
        form.AddField("password", password_InputField.text);
       
        //this var (w) give us access to web pages like index.php file and we pass 2 parametars (url of index.php file and the form)
        WWW w = new WWW(url, form);
        yield return w;
        if(w.error != null)
        {
            //if we got error to connection to database (may the server is not work or the url is invalid)
            Debug.Log("404 not found");
        }
        else
        {   //check if the connection done to get the data
            if (w.isDone)   
            {  //check if we got any error like email or password are invalid
                if (w.text.Contains("error"))
                {
                    Debug.Log("invalid email or password");
                }
                else
                {  //load MainMenu scene and pass the data to the Manager (the data we got be like "10 - hossam@gmail.com - hossam - 12345678")
                    SceneManager.LoadScene("MainMenu");
                    //remove spaces from the text to make it avilable to split the data 
                    string data = w.text.Replace(" ", "");
                    //split the data of the user
                    string[] user = w.text.Split('-');
                    //debug message to check if we got the right data of the user for example send (welcome hossame)
                    Debug.Log("Welcome " + user[2]);
                }
            }
        }
        loginBtn.interactable = true;
        w.Dispose();
    }
}
