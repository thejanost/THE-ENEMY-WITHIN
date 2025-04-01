using UnityEngine;

public class BulletReceiver : MonoBehaviour
{
    public float health = 100f;  // Salud del objeto

    // M�todo que recibe el da�o
    public void ReceiveDamage(float damage)
    {
        health -= damage;
        Debug.Log(gameObject.name + " ha recibido " + damage + " de da�o. Salud restante: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    // M�todo para manejar la muerte del objeto
    void Die()
    {
        // L�gica para la muerte del objeto (por ejemplo, destruirlo)
        Destroy(gameObject);
    }
}
