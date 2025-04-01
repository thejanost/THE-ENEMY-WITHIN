using UnityEngine;

public class BulletReceiver : MonoBehaviour
{
    public float health = 100f;  // Salud del objeto

    // Método que recibe el daño
    public void ReceiveDamage(float damage)
    {
        health -= damage;
        Debug.Log(gameObject.name + " ha recibido " + damage + " de daño. Salud restante: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    // Método para manejar la muerte del objeto
    void Die()
    {
        // Lógica para la muerte del objeto (por ejemplo, destruirlo)
        Destroy(gameObject);
    }
}
