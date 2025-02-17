using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Renderer cubeRd;
    [SerializeField] private Rigidbody cubeRb;
    [SerializeField] private Transform cubeTr;
    
    public Renderer CubeRenderer => cubeRd;
    public Rigidbody CubeRigidbody => cubeRb;
    public Transform CubeTr => cubeTr;
}
