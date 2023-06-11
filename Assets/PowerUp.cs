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

    /// <summary>
    /// Define original position of power ups
    /// </summary>
    private void Start()
    {
        originalY = transform.position.y;
    }

    /// <summary>
    /// Make the power ups rotate and go down and up
    /// </summary>
    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        float newY = originalY + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    /// <summary>
    /// When player collide with power up
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup(other);
        }
    }

    /// <summary>
    /// Pick up power up and change acceleration of player for 5 seconds
    /// </summary>
    /// <param name="player"></param>
    private void Pickup(Collider player)
    {
        FindObjectOfType<AudioManager>().Play("PowerUp");

        carController = player.GetComponentInParent<ThatsHowWeRoll>();
        originalMaxAcceleration = carController.maxAcceleration;

        carController.maxAcceleration *= multiplier;

        Invoke("ResetSpeed", duration);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Reset the acceleration
    /// </summary>
    private void ResetSpeed()
    {
        carController.maxAcceleration = originalMaxAcceleration;
        Destroy(gameObject);
    }
}
