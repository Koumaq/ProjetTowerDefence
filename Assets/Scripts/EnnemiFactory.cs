using UnityEngine;

public static class EnnemiFactory
{
    public static GameObject CreerEnnemi(string type, Vector2 position)
    {
        GameObject prefab = null;

        switch (type)
        {
            case "normal":
                prefab = Resources.Load<GameObject>("Prefabs/EnnemiNormal");
                break;
            case "rapide":
                prefab = Resources.Load<GameObject>("Prefabs/EnnemiRapide");
                break;
            case "resistant":
                prefab = Resources.Load<GameObject>("Prefabs/EnnemiResistant");
                break;
            default:
                Debug.LogError("Type d'ennemi inconnu : " + type);
                return null;
        }

        return Object.Instantiate(prefab, position, Quaternion.identity);
    }
}
