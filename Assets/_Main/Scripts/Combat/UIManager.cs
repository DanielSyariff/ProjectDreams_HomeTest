using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject actionPanel;
    public GameObject targetSelectionPanel;
    public GameObject gameOverPanel;
    public GameObject losePanel;
    public TextMeshProUGUI gameOverText;

    public CombatManager combatManager;

    public GameObject enemyButtonPrefab;
    private string currentAction;
    private bool isMultipleTarget;

    public void ShowActionPanel()
    {
        actionPanel.SetActive(true);
    }

    public void HideActionPanel()
    {
        actionPanel.SetActive(false);
    }

    public void ShowLosePanelButton()
    {
        losePanel.SetActive(true);
    }

    public void ShowTargetSelection(List<Character> enemies, bool multiple)
    {
        targetSelectionPanel.SetActive(true);
        ClearTargetButtons();

        isMultipleTarget = multiple;

        foreach (Character enemy in enemies)
        {
            if (enemy.gameObject.activeInHierarchy)
            {
                GameObject buttonObj = Instantiate(enemyButtonPrefab, targetSelectionPanel.transform);
                buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = enemy.characterName;
                buttonObj.GetComponent<Button>().onClick.AddListener(() => SelectTarget(new List<Character> { enemy }));
            }
        }

        if (multiple)
        {
            GameObject allEnemiesButton = Instantiate(enemyButtonPrefab, targetSelectionPanel.transform);
            allEnemiesButton.GetComponentInChildren<TextMeshProUGUI>().text = "ALL";
            allEnemiesButton.GetComponent<Button>().onClick.AddListener(() => SelectTarget(enemies));
        }
    }

    public void HideTargetSelection()
    {
        targetSelectionPanel.SetActive(false);
    }

    public void ShowGameOver(string message)
    {
        gameOverText.text = message;
        gameOverPanel.SetActive(true);
    }

    public void HideGameOver()
    {
        gameOverPanel.SetActive(false);
    }

    public void OnAttackButton()
    {
        currentAction = "Attack";
        combatManager.PlayerPrepareAction(false);
    }

    public void OnSpellButton()
    {
        currentAction = "Spell";
        combatManager.PlayerPrepareAction(true);
    }

    public void OnDefendButton()
    {
        combatManager.PlayerAction("Defend", combatManager.players);
    }

    public void OnRunButton()
    {
        combatManager.PlayerAction("Run", combatManager.players);
    }

    private void SelectTarget(List<Character> targets)
    {
        combatManager.PlayerAction(currentAction, targets);
        HideTargetSelection();
    }

    private void ClearTargetButtons()
    {
        foreach (Transform child in targetSelectionPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void GoToExploring()
    {
        SceneManager.LoadScene("WorldScene", LoadSceneMode.Single);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
