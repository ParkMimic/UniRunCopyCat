using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGameover = false;
    public bool isClear;

    private float deadCount = 0f;
    public int levelCount;

    public GameObject level_1;

    // 게임이 시작함과 동시에 Awake() 이벤트 메서드
    private void Awake()
    {
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
    }

    public void OnPlayerDead()
    {
        isGameover = true;
    }

    public void OnTrigger()
    {
        levelCount += 1;
    }
}
