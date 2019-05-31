using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeleccionConMouse : MonoBehaviour
{
    public Camera cam;

    Boton3D ultimo;

    void Start()
    {
        //cam = GetComponent<Camera>();
        cam = Camera.main;
    }

    void Update()
    {
        VerificarCambio();

        if(ultimo!=null){
            if(Input.GetMouseButtonDown(0)){
                ultimo.ClickDown();
            }
        }

        

    }


    void VerificarCambio(){
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue);

        RaycastHit hit;
        Boton3D boton = null;
        if(Physics.Raycast(ray, out hit)){
            boton = hit.collider.GetComponent<Boton3D>();
        }

        if(ultimo!=boton){
            if(boton==null && ultimo!=null){
                ultimo.Exit();    
            } 
            
            if(boton!=null && ultimo==null){
                boton.Hover();
            }

            if(boton!=null&&ultimo!=null){
                boton.Hover();
                ultimo.Exit();
            }
        }

        ultimo = boton;
    }
}
