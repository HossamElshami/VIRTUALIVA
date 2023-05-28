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
        profileName.text = data.user_name;
        profileType.text = data.user_type;
    }
}
