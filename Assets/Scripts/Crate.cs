using UnityEngine;
using UnityEngine.EventSystems;
public class Crate : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public CrateManager CrateManager;
    public GameObject CaseOpening;
    public GameObject Openings;
    public GameObject newOpening;
    public RectTransform newOpeningContent;

    private int openingsStarted = 0;

    private float timeLeft = 5;
    private bool clicked = false;
    private bool isHovering = false;
    private float velocity;
    private float smoothTime = 0.2f;
    private Vector3 spawnPosition = new Vector3(0, 0, 0);
    private Quaternion spawnRotation = new Quaternion(0, 0, 0, 0);

    void Start()
    {
        
    }

    void Update()
    {
        PopIcon();

        if (clicked)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;

            
            if (timeLeft <= 5)
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
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (openingsStarted == 2) { Destroy(newOpening); openingsStarted = 0; }
        if (!clicked)
        {
            clicked = true;
            newOpening = Instantiate(CaseOpening, spawnPosition, spawnRotation, Openings.transform);
            newOpeningContent = newOpening.transform.Find("ScrollerWindow/ScrollerContent") as RectTransform;
            CrateManager = newOpening.GetComponent<CrateManager>();
            CrateManager.content = newOpeningContent;
            
            CrateManager.StartSpinning();
            openingsStarted += 1;

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
