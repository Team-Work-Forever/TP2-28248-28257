using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float multiplier = 3f;
    public float duration = 5f;

    private ThatsHowWeRoll carController;
    private float originalMaxAcceleration;

    public float rotationSpeed = 50f;
    public float floatAmplitude = 0.5f;
    public float floatSpeed = 1f;

    private float originalY;

    private void Start()
    {
        originalY = transform.position.y;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        float newY = originalY + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup(other);
        }
    }

    private void Pickup(Collider player)
    {
        FindObjectOfType<AudioManager>().Play("PowerUp");

        carController = player.GetComponentInParent<ThatsHowWeRoll>();
        originalMaxAcceleration = carController.maxAcceleration;

        carController.maxAcceleration *= multiplier;

        Invoke("ResetSpeed", duration);
        gameObject.SetActive(false);
    }

    private void ResetSpeed()
    {
        carController.maxAcceleration = originalMaxAcceleration;
        Destroy(gameObject);
    }
}
