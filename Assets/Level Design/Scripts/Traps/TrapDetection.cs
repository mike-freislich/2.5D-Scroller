using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrapDetection : MonoBehaviour
{
   public UnityEvent OnPlayerEntered;
   public UnityEvent OnPlayerExited;
    
  void OnTriggerEnter2D(Collider2D col)
  {
      if(col.CompareTag("Player"))
      OnPlayerEntered.Invoke();
      
  }

  void OnTriggerExit2D(Collider2D col)
  {
      if(col.CompareTag("Player"))
      OnPlayerExited.Invoke();
      
  }
}
