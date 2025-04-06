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

    // ������ �����԰� ���ÿ� Awake() �̺�Ʈ �޼���
    private void Awake()
    {
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
