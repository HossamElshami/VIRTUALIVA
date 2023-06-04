using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{
    [SerializeField] TMP_Text profileName, profileType;
    [SerializeField] Image profileImage, profileCover;
    public static ProfileManager instance;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        SetProfileData(Main.Instance.userdata);
    }
    public void SetProfileData(userData data)
    {
        profileName.text = FirstCharSubstring(data.user_name);
        profileType.text = FirstCharSubstring(data.user_type);
    }
    public string FirstCharSubstring(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return string.Empty;
        }

        return $"{input[0].ToString().ToUpper()}{input.Substring(1)}";
    }
}
