using UnityEngine;

public class Target : MonoBehaviour
{
    public int pointValue;
    public ParticleSystem explosionParticle;

    private Rigidbody targetRb;
    private GameManager gameManager;

    private float randForceMin = 14;
    private float randForceMax = 16;
    private float randTorqueMax = 10;
    private float randPosX = 4;
    private float spawnPosY = -6;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();

        targetRb.AddForce(randomForce(), ForceMode.Impulse);
        targetRb.AddTorque(randomTorque(), randomTorque(), randomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPos();

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (!other.gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
    }

    Vector3 randomForce()
    {
        return Vector3.up * Random.Range(randForceMin, randForceMax);
    }

    float randomTorque()
    {
        return Random.Range(-randTorqueMax, randTorqueMax);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-randPosX, randPosX), spawnPosY);
    }
}
