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
        modelo.transform.LookAt(EndPosition);

        float trayectoria = 0;
        while (trayectoria <= 1.0f)
        {
            trayectoria += Time.deltaTime*speed;
            modelo.transform.position = Vector3.Lerp(StartPosition, EndPosition, trayectoria);
            yield return null;
        }

        modelo.transform.SetParent(Target.transform);
        modelo.transform.localPosition = Vector3.zero;
        Anim.SetBool("IsRunning", false);

        for (int i = 0; i < ImageTargets.Length; i++)
        {
            if (ImageTargets[i] == Target)
            {
                marcadorActual = i;
                break;
            }
        }
        IsMoving = false;
    }

    private ObserverBehaviour TargetSiguiente()
    {
        foreach (ObserverBehaviour target in ImageTargets) {
            if (target != null)
            {
                bool estaVisto = target.TargetStatus.Status == Status.TRACKED;

                bool esDiferenteAlActual = target != ImageTargets[marcadorActual];

                if (estaVisto && esDiferenteAlActual)
                {
                    for (int i = 0; i < ImageTargets.Length; i++)
                    {
                        if (ImageTargets[i] == target)
                        {
                            marcadorActual = i;
                            break;
                        }
                    }
                    return target;


                }

            }
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
