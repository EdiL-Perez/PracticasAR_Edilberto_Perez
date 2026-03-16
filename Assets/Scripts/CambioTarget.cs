using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vuforia;
using System.Security.Policy;

public class CambioTarget : MonoBehaviour
{
    public GameObject modelo;
    public ObserverBehaviour[] ImageTargets;
    public int marcadorActual;
    public float speed =1.0f;
    private bool IsMoving= false;
    public Animator Anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //if(modelo != null)
        //{
           // Anim = GetComponent<Animator>();
        //}
        
    }

    public void MoverTarget()
    {
        if (!IsMoving)
        {
            StartCoroutine(MoverModelo());

        }
    }

    private IEnumerator MoverModelo()
    {
        IsMoving = true;
        ObserverBehaviour Target = TargetSiguiente();
        if (Target == null)
        {
            IsMoving = false;
            yield break;
        }

        Anim.SetBool("IsRunning", true);

        Vector3 StartPosition = modelo.transform.position;
        Vector3 EndPosition = Target.transform.position;

        float trayectoria = 0;
        while (trayectoria <= 1.0f)
        {
            trayectoria += Time.deltaTime*speed;
            modelo.transform.position = Vector3.Lerp(StartPosition, EndPosition, trayectoria);
            yield return null;
        }

        Anim.SetBool("IsRunning", false);
        marcadorActual = (marcadorActual+1)%ImageTargets.Length;
        IsMoving = false;
    }

    private ObserverBehaviour TargetSiguiente()
    {
        foreach (ObserverBehaviour target in ImageTargets) {
            if (target != null && (target.TargetStatus.Status == Status.TRACKED || target.TargetStatus.Status == Status.EXTENDED_TRACKED))
            {

                return target;
            }
           
            
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
