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

    // ������ �����԰� ���ÿ� Awake() �̺�Ʈ �޼���
    private void Awake()
    {
        GameClearUI.SetActive(false);

        level_1_map.SetActive(true);
        level_1_trigger.SetActive(true);

        level_2_map.SetActive(false);
        level_3_map.SetActive(false);
        level_4_map.SetActive(false);

        levelCount = 0;
        // ���� instance�� null�̶��
        if (instance == null)
        {
            // instance�� �ڽ��� �Ҵ���
            instance = this;
        }
        // instance�� null�� �ƴ϶��
        else
        {
            Destroy(gameObject);
            Debug.Log("�̹� �� �ȿ� ���� �Ŵ����� �����մϴ�!");
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
