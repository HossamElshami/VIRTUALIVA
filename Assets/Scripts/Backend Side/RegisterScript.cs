using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class RegisterScript : MonoBehaviour
{
    public Button signup;
    public TMP_InputField user_name, password, email, user_type;

    void Start()
    {
        signup.onClick.AddListener(() =>
        {
            StartCoroutine(SignUp(user_name.text, email.text, password.text, user_type.text));
        });
    }

    IEnumerator SignUp(string username, string password, string email, string user_type)
    {
        WWWForm form = new WWWForm();
        form.AddField("user_name", username);
        form.AddField("email", email);
        form.AddField("password", password);
        form.AddField("user_type", user_type);
        yield return new WaitForSeconds(1);


        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/virtualiva/auth/signup.php", form))
        {
            yield return www.SendWebRequest();
            string[] pages = "".Split('/');
            int page = pages.Length - 1;
            switch (www.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + www.error);
                    break;

                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + www.error);
                    break;

                case UnityWebRequest.Result.Success:
                    if (www.downloadHandler.text == "Email already exists!")
                    {
                        Debug.Log("Email already exists!");
                    }
                    else if (www.downloadHandler.text == "Email is not valid")
                    {
                        Debug.Log("Email is not valid");
                    }
                    else if (www.downloadHandler.text == "Password must be at least 8 charactes long")
                    {
                        Debug.Log("Password must be at least 8 charactes long");
                    }
                    else
                    {
                        SceneManager.LoadScene("GamePlay");
                    }
                    break;
            }
        }
    }
}