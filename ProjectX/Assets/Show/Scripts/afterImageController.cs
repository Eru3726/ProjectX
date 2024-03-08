using UnityEngine;

public class afterImageController : MonoBehaviour
{
    [SerializeField]
    private float timer = 0.5f;

    private SpriteRenderer spriteRenderer;

    private float time = 0;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        time = timer;
    }

    private void Update()
    {
        if (time <= 0)
        {
            Destroy(this.gameObject);
        }
        else time -= Time.deltaTime;

        spriteRenderer.color = new Color(spriteRenderer.color.r,
                                         spriteRenderer.color.g,
                                         spriteRenderer.color.b,
                                         time);
    }
}