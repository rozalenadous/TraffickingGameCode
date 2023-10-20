using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trust
{
    private static int trust = 80;

    public static void changeTrust(int amt){
      trust += amt;
      if(trust > 100) trust = 100;
      if(trust < 0) trust = 0;
    }

    public static int getTrust(){
      return trust;
    }
}
