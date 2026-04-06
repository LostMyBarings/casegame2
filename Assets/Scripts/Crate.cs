using UnityEngine;
using UnityEngine.EventSystems;
public class Crate : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] CrateManager CrateManager;
    [SerializeField] GameObject CaseOpening;

    private float timeLeft = 5;
    private bool clicked = false;
    private bool isHovering = false;
    private float velocity;
    private float smoothTime = 0.2f;

    void Start()
    {
        
    }

    void Update()
    {
        PopIcon();
        Debug.Log("timeLeft = " + timeLeft);

        if (clicked)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            CaseOpening.SetActive(true);

            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                GetComponent<SpriteRenderer>().enabled = true;
                GetComponent<BoxCollider2D>().enabled = true;
                clicked = false;
                timeLeft = 5;
            }
        }
    }

    private void PopIcon()
    {
        if (isHovering)
        {
            transform.localScale = new Vector3(
                Mathf.SmoothDamp(transform.localScale.x, 1.25f, ref velocity, smoothTime),
                Mathf.SmoothDamp(transform.localScale.y, 1.25f, ref velocity, smoothTime),
                transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(
                Mathf.SmoothDamp(transform.localScale.x, 1f, ref velocity, smoothTime),
                Mathf.SmoothDamp(transform.localScale.y, 1f, ref velocity, smoothTime),
                transform.localScale.z);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        Debug.Log("Hovering");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        Debug.Log("Not Hovering");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        clicked = true;
        CrateManager.StartSpinning();
    }
}
