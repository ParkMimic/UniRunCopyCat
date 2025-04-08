using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGameover = false;
    public bool isGameclear = false;
    public bool isClear;

    public bool isTrigged;

    private float deadCount = 0f;
    public int levelCount;

    public GameObject GameClearUI;

    public GameObject level_1;
    public GameObject level_1_map;
    public GameObject level_1_trigger;

    public GameObject level_2;
    public GameObject level_2_map;
    public GameObject level_2_trigger;

    public GameObject level_3;
    public GameObject level_3_map;
    public GameObject level_3_trigger;

    public GameObject level_4;
    public GameObject level_4_map;
    public GameObject level_4_trigger;

    // 게임이 시작함과 동시에 Awake() 이벤트 메서드
    private void Awake()
    {
        GameClearUI.SetActive(false);

        level_1_map.SetActive(true);
        level_1_trigger.SetActive(true);

        level_2_map.SetActive(false);
        level_3_map.SetActive(false);
        level_4_map.SetActive(false);

        levelCount = 0;
        // 만약 instance가 null이라면
        if (instance == null)
        {
            // instance에 자신을 할당함
            instance = this;
        }
        // instance가 null이 아니라면
        else
        {
            Destroy(gameObject);
            Debug.Log("이미 씬 안에 게임 매니저가 존재합니다!");
        }
    }

    void Update()
    {
        if (isGameover == true)
        {
            deadCount += Time.deltaTime;

            if (deadCount >= 1.5)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if (levelCount == 1 && !isGameover)
        {
            level_1.SetActive(true);
        }
        else if (levelCount == 2 && !isGameover)
        {
            level_1_map.SetActive(false);
            level_1_trigger.SetActive(false);

            level_2.SetActive(true);
            level_2_map.SetActive(true);
            level_2_trigger.SetActive(true);
        }
        else if (levelCount == 3 && !isGameover)
        {
            level_2_map.SetActive(false);
            level_2_trigger.SetActive(false);

            level_3.SetActive(true);
            level_3_map.SetActive(true);
            level_3_trigger.SetActive(true);
        }
        else if (levelCount == 4 && !isGameover)
        {
            level_3_map.SetActive(false);
            level_3_trigger.SetActive(false);

            level_4.SetActive(true);
            level_4_map.SetActive(true);
            level_4_trigger.SetActive(true);
        }

        if (isGameclear == true)
        {

        }
    }

    public void OnPlayerDead()
    {
        isGameover = true;
    }

    public void OnTrigger()
    {
        levelCount += 1;
    }

    public void GameOver()
    {

    }

    public void GameClear()
    {
        GameClearUI.SetActive(true);
        isGameclear = true;
    }
}
