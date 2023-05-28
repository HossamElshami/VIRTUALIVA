using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoginScript : MonoBehaviour
{
    public Button login;
    public TMP_InputField email, password;

    void Start()
    {
        login.onClick.AddListener(() =>
        {
            StartCoroutine(Login(email.text, password.text));
        });
    }

    IEnumerator Login(string email, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/virtualiva/auth/login.php", form))
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
                    if (www.downloadHandler.text == "Wrong email or password!")
                    {
                        Debug.Log("Wrong email or password!");
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
