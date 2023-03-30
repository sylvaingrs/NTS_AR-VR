using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplicationManager : MonoBehaviour
{

    public GameObject EnemyPrefab;
    public Transform cam;

    public int EnemyNumber = 20;
    public int EnemyKilled = 0;
    public float spawnRange = 3f;

    public float defaultTimer = 30;
    public float timer;
    public bool firstLaunch = true;
    [SerializeField] private Text timeText;
    [SerializeField] private Text scoreText;

    public Image background;
    public Button retryButton;
    public Text endText;


    public void SpawnEnemy()
    {
        for (int i = 0; i < (firstLaunch ? EnemyNumber : EnemyKilled); i++)
        {
            float x = cam.transform.position.x + Random.Range(-spawnRange, spawnRange);
            float y = cam.transform.position.y + Random.Range(-spawnRange, spawnRange);
            float z = cam.transform.position.z + Random.Range(-spawnRange, spawnRange);
            Vector3 spawnPos = new Vector3(x, y, z);
            Instantiate(EnemyPrefab, spawnPos, Quaternion.identity);
        }

        firstLaunch = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        retryButton.onClick.AddListener(() => InitGame());
        InitGame();
    }

    private void InitGame()
    {
        EnemyNumber = 25;
        EnemyKilled = -1;
        timer = defaultTimer;
        AddScore();
        SpawnEnemy();
        firstLaunch = false;

        // make menu invisible
        background.enabled = false;
        endText.enabled = false;
        retryButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 0;
        }

        DisplayTimer(timer);
    }

    public void DisplayTimer(float timer)
    {

        if (timer < 0)
        {
            timer = 0;
            EndGame(false);
        }

        timeText.text = Mathf.FloorToInt(timer).ToString();
    }

    public void AddScore()
    {
        EnemyKilled += 1;
        scoreText.text = "Score :\n" + EnemyKilled + " / " + EnemyNumber;

        if (EnemyKilled == EnemyNumber)
        {
            EndGame(true);
        }
    }

    public void EndGame(bool win)
    {
        background.enabled = true;
        endText.enabled = true;
        retryButton.gameObject.SetActive(true);

        if (win)
            endText.text = "Vous avez gagné en " + Mathf.FloorToInt(defaultTimer - timer) + "s !";

        else
            endText.text = "Vous avez perdu !";
    }
}
