using TMPro;
using UnityEngine;

public class WinnerPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject _panel;

    [SerializeField]
    private TextMeshProUGUI _puntos;

    [SerializeField]
    private TextMeshProUGUI _descuento;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void ActivePanel()
    {
        _panel.SetActive(true);
    }

    public void SetTextPuntos(string text)
    {
        _puntos.text = text;
    }

    public void SetTextDescuento(string text)
    {
        _descuento.text = text;
    }
}