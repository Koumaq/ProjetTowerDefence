using Assets.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI monnaieText;
    public TextMeshProUGUI viesText;

    public GameObject gameOverUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        MettreAJourMonnaie(GestionnaireDeJeu.Instance.Monnie);
        MettreAJourVies(GestionnaireDeJeu.Instance.Vies);
    }

    public void MettreAJourMonnaie(int montant)
    {
        monnaieText.text = $"Monnaie : {montant}";
    }

    public void MettreAJourVies(int vies)
    {
        viesText.text = $"Vies : {vies}";
    }

    public void gameOver()
    {
        gameOverUI.SetActive(true);
    }

    public void restart()
    {
        SceneManager.LoadScene(1);
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quite()
    {
        Application.Quit();

        UnityEditor.EditorApplication.isPlaying = false;
    }
}
