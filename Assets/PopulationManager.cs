using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class PopulationManager : MonoBehaviour
{
    public GameObject personPrefab;
    public int populationSize = 10;
    List<GameObject> population = new();


    public static float elapsed = 0;
    int trialTime = 10;
    int generation = 1;

    GUIStyle guiStyle = new GUIStyle();

     void OnGUI()
    {
        guiStyle.fontSize = 50;
        guiStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10,10,100,20), "Generation: "+ generation,guiStyle);
        GUI.Label(new Rect(10,65,100,29), "Trial Times: " + (int)elapsed,guiStyle);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i <populationSize; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-8, 9), Random.Range(-3.0f, 6.0f), 0);
            GameObject go = Instantiate(personPrefab, pos, Quaternion.identity);
            go.GetComponent<DNA>().size = Random.Range(0.1f, 0.3f);
            go.GetComponent<DNA>().r = Random.Range(0.0f, 1.0f);
            go.GetComponent<DNA>().g = Random.Range(0.0f, 1.0f);
            go.GetComponent<DNA>().b = Random.Range(0.0f, 1.0f);
            population.Add(go);
        }
        
    }

    GameObject Breed(GameObject parent1, GameObject parent2) 
    {
        Vector3 pos = new Vector3(Random.Range(-8, 9), Random.Range(-3.0f, 6.0f), 0);
        GameObject go  = GameObject.Instantiate(personPrefab,pos, Quaternion.identity);
        DNA dna1  = parent1.GetComponent<DNA>();
        DNA dna2 = parent2.GetComponent<DNA>();
        if (Random.Range(0, 1000) > 5)
        {
            go.GetComponent<DNA>().size = Random.Range(0,10) >5 ? dna1.size : dna2.size;
            go.GetComponent<DNA>().r = Random.Range(0, 10) < 5 ? dna1.r : dna2.r;
            go.GetComponent<DNA>().g = Random.Range(0, 10) < 5 ? dna1.g : dna2.g;
            go.GetComponent<DNA>().b = Random.Range(0, 10) < 5 ? dna1.b : dna2.b;
        }
        else
        {
            go.GetComponent<DNA>().size = Random.Range(0.1f, 0.3f);
            go.GetComponent<DNA>().r = Random.Range(0.0f, 1.0f);
            go.GetComponent<DNA>().g = Random.Range(0.0f, 1.0f);
            go.GetComponent<DNA>().b = Random.Range(0.0f, 1.0f);

        }

        return go;

    }

    void BreedNewPopulation()
    {
       // List<GameObject> newPopulation = new();
        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<DNA>().timeToDie).ToList();

        population.Clear();
        for (int i = (int)(sortedList.Count / 2.0f) -1; i < sortedList.Count - 1; i++) {
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i+1], sortedList[i]));
        }
        for(int i = 0; i< sortedList.Count; i++)
        {
            Destroy(sortedList[i]);
        }
        generation++;

    }


    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed > trialTime)
        {
            BreedNewPopulation();
            elapsed = 0;
        }
        
    }
}
