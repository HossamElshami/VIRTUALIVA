using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Silder : MonoBehaviour
{
    [SerializeField] private TMP_Text PersentText;
    [SerializeField] private float PersentAdd = 0f;

    private void Start()
    {
        PersentText.text = ((int)(GetComponent<Slider>().value + PersentAdd)).ToString() + "%";
    }
    public void ChangeText(float persent)
    {
        PersentText.text = ((int)(persent + PersentAdd)).ToString() + "%";
    }
}
