using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using Unity.Collections;
using System.Collections;

public abstract class Ennemi : MonoBehaviour
{
    public float vitesse;
    public int pv;
    public int argentRecompense = 20;
    private Carte carte;  // Déclarer la référence à la carte
    private bool estBrule = false; // Vérifie si l'effet est déjà appliqué


    protected int indexChemin = 0;
    protected List<Vector3> Chemin;

    public void Initialiser(List<Vector3> chemin, Carte carte)
    {
        this.carte = carte;
        Chemin = chemin;
        if (Chemin.Count > 0)
        {
            transform.position = Chemin[0];  // Initialisation de la position à la première case du chemin
            Debug.Log("Chemin initialisé avec " + Chemin.Count + " points.");
        }
        else
        {
            Debug.LogError("Le chemin est vide !");
        }
    }

    void Update()
    {
        // Vérifier si le chemin est bien défini et s'il reste des cases à parcourir
        if (Chemin.Count == 0 || indexChemin >= Chemin.Count)
            return;

        // Obtenir la prochaine position de la case
        Vector3 targetPosition = Chemin[indexChemin];

        // Déplacer l'ennemi vers la position cible (utilise la taille de la case pour garantir le mouvement par case)
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, vitesse * Time.deltaTime);

        // Vérifier si l'ennemi est suffisamment proche de la position cible pour passer à la suivante
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            indexChemin++; // Passer à la prochaine case du chemin
        }

        // Si l'ennemi arrive à la base, effectuer l'action appropriée
        if (indexChemin >= Chemin.Count)
        {
            // L'ennemi est arrivé à la base, logique à implémenter
            GestionnaireDeJeu.Instance.PerdreVie();
            Destroy(gameObject);  // Détruire l'ennemi
        }
    }

    public void InfligerBrulure(int degatsParSeconde, float duree)
    {
        if (!estBrule) // Empêcher d'empiler plusieurs effets de brûlure
        {
            StartCoroutine(EffetBrulure(degatsParSeconde, duree));
        }
    }

    private IEnumerator EffetBrulure(int degatsParSeconde, float duree)
    {
        estBrule = true;
        float tempsRestant = duree;

        while (tempsRestant > 0)
        {
            RecevoirDegats(degatsParSeconde);
            yield return new WaitForSeconds(1f);
            tempsRestant -= 1f;
        }

        estBrule = false;
    }

    public void Ralentir(float moinsvitesse)
    {
        vitesse *= moinsvitesse;
    }


    public void RecevoirDegats(int degats)
    {
        pv -= degats;
        if (pv <= 0) Mourir();
    }

    protected virtual void Mourir()
    {
        GestionnaireDeJeu.Instance.AjouterMonnaie(argentRecompense);
        Destroy(gameObject);
    }
}
