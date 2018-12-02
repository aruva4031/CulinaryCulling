using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour {
    public string[] ingredients;
    public GameObject abomination;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool hasIngredients(List<string> combined)
    {
        int counter = 0;
        for(int i = 0; i < ingredients.Length; i++)
        {
            for(int j = 0; j < combined.Count; j++)
            {
                if (ingredients[i] == combined[j])
                {
                    counter++;
                }
            }
        }
        if (counter == ingredients.Length)
        {
            return true;
        }
        return false;
    }

    
}
