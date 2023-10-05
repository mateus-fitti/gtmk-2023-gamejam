using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseLevel : MonoBehaviour
{
    public GameObject levelPrefab;

    // Start is called before the first frame update
    void Start()
    {
        int currentLevel = GameController.instance.CurrentLevel();

        if (GameController.instance != null && currentLevel > 1)
        {
            // Find the GameObject with GridLayoutGroup inside LevelSelector
            GridLayoutGroup gridLayout = this.gameObject.GetComponentInChildren<GridLayoutGroup>();

            if (gridLayout != null)
            {
                for (int i = 1; i <= currentLevel; i++)
                {
                    GameObject newLevelPrefab = Instantiate(levelPrefab, gridLayout.transform);
                    TextMeshProUGUI levelText = newLevelPrefab.GetComponentInChildren<TextMeshProUGUI>();
                    Button btn = newLevelPrefab.GetComponentInChildren<Button>();

                    // Use a lambda function to correctly pass the method reference
                    btn.onClick.AddListener(() => LoadLevel(levelText.text));

                    levelText.text = i.ToString();
                }
            }
            else
            {
                Debug.LogError("GridLayoutGroup component not found in children of LevelSelector.");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // You can add update logic here if needed
    }

    public void LoadLevel(string level)
    {
        SceneManager.LoadScene("Level" + level);
    }
}
