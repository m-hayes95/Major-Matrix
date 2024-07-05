using UnityEngine;
using TMPro;

public class RandomID : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI id;
    [SerializeField] private string[] alphabet;
    private void Start()
    {
        id.text = GenerateRandomID();
    }
    private string GenerateRandomID()
    {
        int randomIndex = Random.Range(0, alphabet.Length);
        string char1 = alphabet[randomIndex];
        string char2 = Random.Range(0, 10).ToString();
        string char3 = Random.Range(0, 10).ToString();
        string char4 = Random.Range(0, 10).ToString();
        string char5 = Random.Range(0, 10).ToString();
        string char6 = Random.Range(0, 10).ToString();
        return char1 + char2 + char3 + char4 + char5 + char6;
    }
}
