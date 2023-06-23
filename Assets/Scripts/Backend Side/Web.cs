using UnityEngine;
using Newtonsoft.Json;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class Web : MonoBehaviour
{
    public IEnumerator Login(string email, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/virtualiva/auth/login.php", form))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                MainManager.instance.showDialogBox("Cannot connect to the server, maybe the problem could be from the Internet.", MainManager.dialogType.Attention);
            }
            if (www.downloadHandler.text != string.Empty && www.downloadHandler.text != null)
            {
                string[] data = www.downloadHandler.text.Split('"');

                if (data[data.Length - 2] != "Wrong email or password!")
                {
                    var userdataClass = JsonConvert.DeserializeObject<user>(www.downloadHandler.text);
                    Main.Instance.userdata = userdataClass.data;
                    SceneManager.LoadScene("MainMenu");
                }
                else
                    MainManager.instance.showDialogBox(data[data.Length - 2] + "<br>Please try again.", MainManager.dialogType.Error);
            }
        }
    }

    public IEnumerator Register(string user_name, string email, string password, string user_type)
    {
        WWWForm form = new WWWForm();
        form.AddField("user_name", user_name);
        form.AddField("email", email);
        form.AddField("password", password);
        form.AddField("user_type", user_type);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/virtualiva/auth/signup.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
                Debug.Log(www.error);
            else
                Debug.Log(www.downloadHandler.text);
        }
    }
}
public class user
{
    public userData data { get; set; }
}
[System.Serializable]
public class userData
{
    public int id;
    public string user_name;
    public string email;
    public string password;
    public string user_type;
}
