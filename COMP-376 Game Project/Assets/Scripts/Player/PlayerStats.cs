using System;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerStats : MonoBehaviour
{   
    public static PlayerStats Instance;

    [Header("Stats")]
    [Range(0, 100)] public float sanity = 100f;
    [Range(0, 100)] public float energy = 100f;

    [Header("Drain Rates")]
    public float walkEnergyDrain = 0.5f; // per second of walking
    public float sanityDrainRate = 5f;   // when exposed to scary event

    public event Action<float> OnSanityChanged;
    public event Action<float> OnEnergyChanged;
    private FPSController controller;
    void Awake()
    {
        if (Instance == null) Instance = this;
        controller = GetComponent<FPSController>();
    }
    private void Start()
    {
        UIManager.Instance.RegisterPlayer(GetComponent<PlayerStats>());
        
    }
    void Update()
    {
        HandleEnergyDrain();
    }

    void HandleEnergyDrain()
    {
        // Drains energy if player is moving
        if (controller.IsMoving())
        {
            ModifyEnergy(-walkEnergyDrain * Time.deltaTime);
        }
    }
    public void ModifySanity(float amount)
    {
        sanity = Mathf.Clamp(sanity + amount, 0, 100);
        OnSanityChanged?.Invoke(sanity);
    }

    public void ModifyEnergy(float amount)
    {
        energy = Mathf.Clamp(energy + amount, 0, 100);
        OnEnergyChanged?.Invoke(energy);
    }
}
