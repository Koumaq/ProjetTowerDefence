using Assets.Scripts;
using UnityEngine;

public class Tour : MonoBehaviour
{
    public int degats = 10;
    public float rayonDetection = 5f;
    public float cadenceTir = 1f;
    private float prochainTir;

    private LineRenderer ligneCercle;
    private ITypeTour typeTour; // Référence à l'interface ITypeTour

    public void Initialiser(ITypeTour type)
    {
        typeTour = type;  // Initialiser le type de la tour
    }

    private void Start()
    {
        // Initialiser le LineRenderer, etc.
        ligneCercle = gameObject.AddComponent<LineRenderer>();
        ligneCercle.startWidth = 0.05f;
        ligneCercle.endWidth = 0.05f;
        ligneCercle.loop = true;
        ligneCercle.positionCount = 50;
        ligneCercle.material = new Material(Shader.Find("Sprites/Default"));
        ligneCercle.startColor = new Color(1, 0, 0, 0.5f);
        ligneCercle.endColor = new Color(1, 0, 0, 0.5f);

        DessinerCercle();
    }

    private void Update()
    {
        Collider2D[] ennemis = Physics2D.OverlapCircleAll(transform.position, rayonDetection);

        foreach (Collider2D ennemi in ennemis)
        {
            if (ennemi.CompareTag("Ennemi"))
            {
                Ennemi ennemiScript = ennemi.GetComponent<Ennemi>();
                if (ennemiScript != null)
                {
                    Attaquer(ennemiScript);
                }
            }
        }
    }

    private void Attaquer(Ennemi cible)
    {
        if (Time.time >= prochainTir)
        {
            prochainTir = Time.time + cadenceTir;
            typeTour.Attaquer(cible, this);  // Appel de la méthode spécifique à chaque type de tour
        }
    }

    private void DessinerCercle()
    {
        float angle = 0f;
        for (int i = 0; i < ligneCercle.positionCount; i++)
        {
            float x = Mathf.Cos(angle) * rayonDetection;
            float y = Mathf.Sin(angle) * rayonDetection;
            ligneCercle.SetPosition(i, new Vector3(transform.position.x + x, transform.position.y + y, 0));
            angle += (2 * Mathf.PI) / ligneCercle.positionCount;
        }
    }
}
