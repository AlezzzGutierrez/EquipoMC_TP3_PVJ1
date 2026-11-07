
using UnityEngine;
using TMPro;

public class WeaponPickup : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private TMP_Text pickupPrompt;

    private WeaponDataSO weaponData;
    private GameObject visualInstance;

    public WeaponDataSO WeaponData => weaponData;

    private void Awake()
    {
        ShowPrompt(false);
    }

    public void Initialize(WeaponDataSO data)
    {
        weaponData = data;

        if (weaponData == null)
        {
            Debug.LogError("WeaponPickup no recibió WeaponDataSO.");
            return;
        }

        // Crear el modelo visual del arma en el suelo.
        if (weaponData.weaponPrefab != null)
        {
            visualInstance = Instantiate(
                weaponData.weaponPrefab,
                transform
            );

            visualInstance.transform.localPosition = Vector3.zero;
            visualInstance.transform.localRotation = Quaternion.identity;

            // El arma del suelo no necesita disparar.
            foreach (MonoBehaviour component in
                     visualInstance.GetComponentsInChildren<MonoBehaviour>())
            {
                component.enabled = false;
            }
        }

        if (pickupPrompt != null)
        {
            pickupPrompt.text =
                "Presiona E para equipar " + weaponData.weaponName;

            ShowPrompt(false);
        }
    }

    public void ShowPrompt(bool show)
    {
        if (pickupPrompt != null)
        {
            pickupPrompt.gameObject.SetActive(show);
        }
    }
}
