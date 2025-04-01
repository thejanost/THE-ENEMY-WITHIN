using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform shootingPoint;  // Punto desde donde sale el raycast
    public float shootRange = 50f;  // Distancia m�xima de disparo
    public int maxAmmo = 10;  // Balas disponibles
    private int currentAmmo;
    private bool isAiming = false;

    public float bulletDamage = 10f;  // Da�o de la bala

    void Start()
    {
        currentAmmo = maxAmmo;  // Inicializa las balas
    }

    void Update()
    {
        // Detectar si el bot�n derecho del mouse est� presionado
        isAiming = Input.GetMouseButton(1);

        // Si se est� apuntando y presionamos el bot�n izquierdo del mouse
        if (isAiming && Input.GetMouseButtonDown(0) && currentAmmo > 0)
        {
            FireRaycast();
        }
    }

    void FireRaycast()
    {
        // Asegurarse de que shootingPoint est� asignado, si no, se utiliza la posici�n del jugador
        if (shootingPoint == null)
        {
            shootingPoint = transform;  // Si no se asigna un shootingPoint, se usar� la posici�n del jugador
        }

        // Disparar un raycast desde el shootingPoint hacia donde est� mirando
        Ray ray = new Ray(shootingPoint.position, shootingPoint.forward);  // Desde el shootingPoint
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, shootRange))
        {
            // Si el raycast colisiona con un objeto, se puede hacer algo aqu�
            Debug.Log("Disparo exitoso! Impact� en: " + hit.collider.name);

            // Si el raycast golpea un BulletReceiver, aplica el da�o
            BulletReceiver receiver = hit.collider.gameObject.GetComponent<BulletReceiver>();
            if (receiver != null)
            {
                receiver.ReceiveDamage(bulletDamage);
            }
        }
        else
        {
            // Si no impacta en nada, muestra un mensaje de fallo
            Debug.Log("Disparo fallido. No impact� en nada.");
        }

        // Reducir el n�mero de balas
        currentAmmo--;
        Debug.Log("Balas restantes: " + currentAmmo);
    }
}

