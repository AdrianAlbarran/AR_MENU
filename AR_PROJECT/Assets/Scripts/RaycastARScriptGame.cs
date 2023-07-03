using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class RaycastARScriptGame : MonoBehaviour
{
    public GameObject spawn_prefab;
    private GameObject _spawned_object;
    private bool _ObjectSpawned;
    public ARPlaneManager planeManager;
    public GameObject gameButton;
    public GameObject areaButton;
    public TextMeshProUGUI text;
    public TextMeshProUGUI textExp1;
    public TextMeshProUGUI textExp2;

    public WinnerPanel winnerPanel;

    public bool canSpawn;

    [SerializeField]
    public Camera arCamera;

    public ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private bool foodActive;

    [SerializeField]
    private float textTime;

    [HideInInspector] public GameObject[] ingredientsArray;

    [SerializeField] private Timer timer;

    private GameManager gameManager;
    private Coroutine timerCoroutine;
    private float _foodPrice;

    private void Start()
    {
        _ObjectSpawned = false;
        foodActive = true;
    }

    private void OnEnable()
    {
        text.gameObject.SetActive(true);
        textExp1.gameObject.SetActive(true);
        textExp2.gameObject.SetActive(true);
        
    }

    public void addObject()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPose = hits[0].pose;

                Vector3 newPos = new Vector3(hitPose.position.x, hitPose.position.y, hitPose.position.z);
                _spawned_object = Instantiate(spawn_prefab, hitPose.position, hitPose.rotation);
                _spawned_object.name = "Game";
                _ObjectSpawned = true;
                canSpawn = false;
                UIEnabler(true);

                foreach (var plane in planeManager.trackables)
                {
                    plane.gameObject.SetActive(false);

                    planeManager.enabled = false;
                }
            }
        }
    }

    private void Update()
    {
        if (canSpawn)
        {
            SetText(1);
            addObject();
        }
    }

    public void Delete()
    {
        if (_ObjectSpawned)
        {
            Destroy(_spawned_object);
        }
    }

    public void Spawn()
    {
        Delete();
        UIEnabler(false);

        foreach (var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(true);

            planeManager.enabled = true;
        }

        if (foodActive)
        {
            GameObject foodPrefab = FindAnyObjectByType<RestuaranteManager>().gameObject;
            ingredientsArray = foodPrefab.GetComponent<RestuaranteManager>().CurrentModel().GetComponent<IngredientsDB>().ingredients;
            _foodPrice = foodPrefab.GetComponent<RestuaranteManager>().CurrentModel().GetComponent<IngredientsDB>().price;
           
            foodPrefab.SetActive(false);

            // desactivar bebida
            GameObject drinkPrefab = FindAnyObjectByType<DrinksManager>().CurrentModel();
            // poner precio bebidas
            _foodPrice += drinkPrefab.GetComponent<IngredientsDB>().price;
            drinkPrefab.SetActive(false);


            foodActive = false;

        }

        StartCoroutine(PlaceGame());
    }

    public void StartGame()
    {
        SetText(2);
        textExp1.gameObject.SetActive(false);
        textExp2.gameObject.SetActive(false);
        StartCoroutine(TextOnScreen());
        UIEnabler(false);
        gameManager = _spawned_object.GetComponent<GameManager>();
        gameManager.StartGame();
        timer.enabled = true;
        timerCoroutine = StartCoroutine(GameDuration(timer.starTime));
    }

    public void UIEnabler(bool control)
    {
        if (control)
        {
            areaButton.SetActive(true);
            gameButton.SetActive(true);
        }
        else
        {
            areaButton.SetActive(false);
            gameButton.SetActive(false);
            textExp1.gameObject.SetActive(false);
            textExp2.gameObject.SetActive(false);
        }
    }

    public IEnumerator PlaceGame()
    {
        yield return new WaitForSeconds(0.2f);
        canSpawn = true;
    }

    public void SetText(int idx)
    {
        switch (idx)
        {
            case 0: text.text = "Prepárate para jugar"; break;
            case 1: text.text = "Selecciona donde quieres jugar"; break;
            case 2: text.text = "Recoge todos los alimentos"; break;
        }
    }

    public IEnumerator TextOnScreen()
    {
        yield return new WaitForSeconds(textTime);
        text.gameObject.SetActive(false);
    }

    public IEnumerator GameDuration(float time)
    {
        yield return new WaitForSeconds(time);
        timer.enabled = false;
        EndGame();

    }

    public void EndGame()
    {
        double descuentoValor = _foodPrice * (1 - (gameManager.GetPoints() * 0.05f * 0.01f));
        descuentoValor = Math.Floor(descuentoValor * 100) / 100;

        if (timer.enabled)
        {
            StopCoroutine(timerCoroutine);
            timer.enabled = false;
        }

        string descuento = $"Descuento: {descuentoValor}$";

        winnerPanel.SetTextDescuento(descuento);

        winnerPanel.SetTextPuntos(gameManager.GetPoints().ToString());
        winnerPanel.ActivePanel();

        _spawned_object.GetComponent<GameManager>().EndGame();
    }
}