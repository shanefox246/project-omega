using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;

    public int currentArmor;
    public int maxArmor = 75;

    float elapsedTime = 0.0f;
    float waitTime = 3.0f;
    public AudioSource playerDamaged;
    


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentArmor = maxArmor;
        playerDamaged = GetComponent<AudioSource>();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > waitTime && currentHealth < 100)
        {
            currentHealth += 10;
            elapsedTime = 0.0f;
        }
    }

    public void TakeDamage(int amount)
    {
        playerDamaged.Play();

        if(currentArmor <= 0) 
            currentHealth -= amount;
        else
            currentArmor -= amount;

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RefreshArmor()
    {
        currentArmor = maxArmor;
    }

    public void OnArmorButtonClicked()
    {
        RefreshArmor();
    }
}
