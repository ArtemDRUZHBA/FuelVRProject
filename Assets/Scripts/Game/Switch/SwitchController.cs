using DG.Tweening.Plugins;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    [SerializeField] private HoseLeakSpawner hoseLeakSpawner;
    [SerializeField] public CreateFuelPistol _fuelColumn;
    [SerializeField] private bool _fuelColumnLock = true;
    [SerializeField] private Collider _colController;

    private Animator animator;
    private bool isEnabled = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isEnabled", isEnabled);

        //_fuelColumn = GetComponent<CreateFuelPistol>();//CreateFuelPistol не весит на этом объекте => не имеет смысла. _fuelColumn = null
    }

    private void OnTriggerEnter(Collider _colController)
    {
        //isEnabled = !isEnabled;
        animator.SetBool("isEnabled", !isEnabled);
        if (_fuelColumnLock == true)
        {
            _fuelColumnLock = false;
            foreach (GameObject fuelPistol in _fuelColumn.fuelPistols)//Проходимся по каждому пистолету в колонке, ищем ParticleSystem и пробуем выключать.
            {                                                     //Нужно на случай если выключаешь подачу когда PS активна.
                Transform particleSystem = fuelPistol.transform.FindDeepChild("FuelPS");
                particleSystem.gameObject.SetActive(false);
                particleSystem.gameObject.SetActive(true);
                //fuelPistol.GetComponent<ParticleSystem>();
                //fuelPistol.SetActive(false);
                //fuelPistol.OnDisable();

                Debug.Log($"Deactivate ParticleSystem");
            }
        }
        else _fuelColumnLock = true;
    }
}
