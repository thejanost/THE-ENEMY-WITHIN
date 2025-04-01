using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform shootingPoint;  // Punto desde donde sale el raycast
    public float shootRange = 50f;  // Distancia máxima de disparo
    public int maxAmmo = 10;  // Balas disponibles
    private int currentAmmo;
    private bool isAiming = false;

    public float bulletDamage = 10f;  // Daño de la bala

    void Start()
    {
        currentAmmo = maxAmmo;  // Inicializa las balas
    }

    void Update()
    {
        // Detectar si el botón derecho del mouse está presionado
        isAiming = Input.GetMouseButton(1);

        // Si se está apuntando y presionamos el botón izquierdo del mouse
        if (isAiming && Input.GetMouseButtonDown(0) && currentAmmo > 0)
        {
            FireRaycast();
        }
    }

    void FireRaycast()
    {
        // Asegurarse de que shootingPoint esté asignado, si no, se utiliza la posición del jugador
        if (shootingPoint == null)
        {
            shootingPoint = transform;  // Si no se asigna un shootingPoint, se usará la posición del jugador
        }

        // Disparar un raycast desde el shootingPoint hacia donde está mirando
        Ray ray = new Ray(shootingPoint.position, shootingPoint.forward);  // Desde el shootingPoint
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, shootRange))
        {
            // Si el raycast colisiona con un objeto, se puede hacer algo aquí
            Debug.Log("Disparo exitoso! Impactó en: " + hit.collider.name);

            // Si el raycast golpea un BulletReceiver, aplica el daño
            BulletReceiver receiver = hit.collider.gameObject.GetComponent<BulletReceiver>();
            if (receiver != null)
            {
                receiver.ReceiveDamage(bulletDamage);
            }
        }
        else
        {
            // Si no impacta en nada, muestra un mensaje de fallo
            Debug.Log("Disparo fallido. No impactó en nada.");
        }

        // Reducir el número de balas
        currentAmmo--;
        Debug.Log("Balas restantes: " + currentAmmo);
    }
}

