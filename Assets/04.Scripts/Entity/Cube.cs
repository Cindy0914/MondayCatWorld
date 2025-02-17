using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Renderer cubeRenderer;
    [SerializeField] private Transform cubeTr;
    
    public Renderer CubeRenderer => cubeRenderer;
    public Transform CubeTr => cubeTr;
}
