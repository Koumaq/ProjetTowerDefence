using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class GestionnaireDeJeu : MonoBehaviour
    {
        public static GestionnaireDeJeu Instance;
        public Carte carte;
        public GameObject baseObjetPrefab; // Référence à un prefab de la base à instancier
        public UIManager manager;
        public static Vector2Int basePosition;
        public int Monnie { get; set; } // Monnaie du joueur
        public int Vies { get; private set; } // Vies restantes

        private GameObject baseObjet; // Référence à l'objet de la base dans la scène

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
            Monnie = 100;
            Vies = 20;
            basePosition = carte.ChoisirBaseAleatoire();
            
            // Marquer la case de la base comme bloquée pour les tours
            carte.BloquerCase(basePosition);

            UIManager.Instance.MettreAJourVies(Vies);
            UIManager.Instance.MettreAJourMonnaie(Monnie);

            if (baseObjetPrefab != null)
            {
                baseObjet = Instantiate(baseObjetPrefab, new Vector3(basePosition.x, basePosition.y, 0), Quaternion.identity);
                baseObjet.name = "Base";
            }
            else
            {
                Debug.LogError("Le prefab de la base n'a pas été assigné !");
            }
        }



        public Vector2Int GetBasePosition()
        {
            return basePosition;
        }

        public void AjouterMonnaie(int montant)
        {
            Monnie += montant;
            UIManager.Instance.MettreAJourMonnaie(Monnie);
            Debug.Log("Monnaie actuelle : " + Monnie);
        }

        public void PerdreVie()
        {
            Vies--;
            UIManager.Instance.MettreAJourVies(Vies);

            if (Vies <= 0)
            {
                GameOver();
            }
        }


        private void GameOver()
        {
            manager.gameOver();
            Debug.Log("Game Over!");
            // Implémenter le redémarrage ou l'arrêt de la partie
        }
    }


}
