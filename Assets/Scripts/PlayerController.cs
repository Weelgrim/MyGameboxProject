using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float fireRate = 0.5f;

    private bool _useMouseControl = false;
    private float _nextFireTime = 0.0f;
    private Vector2 _screenBounds;

    private void Start()
    {
        if (Camera.main != null)
        {
            _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleShooting();
        HandleControlSwitch();
        ClampPosition();
    }

    private void HandleMovement()
    {
        if (_useMouseControl)
        {
            if (Camera.main != null)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0;
                transform.position = Vector2.MoveTowards(transform.position, mousePosition, speed * Time.deltaTime);
            }
        }
        else
        {
            float moveHorizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            float moveVertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            transform.Translate(moveHorizontal, moveVertical, 0);
        }
    }

    private void HandleShooting()
    {
        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && Time.time > _nextFireTime)
        {
            _nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    private void HandleControlSwitch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            _useMouseControl = !_useMouseControl;
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
    }

    private void ClampPosition()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, _screenBounds.x * -1, _screenBounds.x);
        viewPos.y = Mathf.Clamp(viewPos.y, _screenBounds.y * -1, _screenBounds.y);
        transform.position = viewPos;
    }
}