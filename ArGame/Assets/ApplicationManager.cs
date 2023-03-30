using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplicationManager : MonoBehaviour
{

    public GameObject EnemyPrefab;
    public Transform cam;

    public int EnemyNumber = 25;
    public float spawnRange = 3f;

    public float defaultTimer = 30;
    public float timer = 30;
    [SerializeField] private Text timeText;
    [SerializeField] private Text scoreText;

    public void SpawnEnemy()
    {
        for (int i = 0; i < EnemyNumber; i++)
        {
            float x = cam.transform.position.x + Random.Range(-spawnRange, spawnRange);
            float y = cam.transform.position.y + Random.Range(-spawnRange, spawnRange);
            float z = cam.transform.position.z + Random.Range(-spawnRange, spawnRange);
            Vector3 spawnPos = new Vector3(x, y, z);
            Instantiate(EnemyPrefab, spawnPos, Quaternion.identity);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        EnemyNumber = 25;
        timer = 30;
        EditScore(0);
        SpawnEnemy();
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

    public void EditScore(int score)
    {
        scoreText.text = "Score :\n" + score + " / " + EnemyNumber;
    }

    public void EndGame(bool win)
    {
        timer = 50;
    }
}
