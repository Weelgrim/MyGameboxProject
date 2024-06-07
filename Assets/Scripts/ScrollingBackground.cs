using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public GameObject background1;
    public GameObject background2;
    public float scrollSpeed = 1;

    private float _backgroundHeight;

    void Start()
    {
        _backgroundHeight = background1.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        background1.transform.position += Vector3.down * scrollSpeed * Time.deltaTime;
        background2.transform.position += Vector3.down * scrollSpeed * Time.deltaTime;
        
        if (background1.transform.position.y < -_backgroundHeight)
        {
            background1.transform.position += new Vector3(0, 2 * _backgroundHeight, 0);
        }
        if (background2.transform.position.y < -_backgroundHeight)
        {
            background2.transform.position += new Vector3(0, 2 * _backgroundHeight, 0);
        }
    }
    public void SetScrollSpeed(float newSpeed)
    {
        scrollSpeed = newSpeed;
    }
}