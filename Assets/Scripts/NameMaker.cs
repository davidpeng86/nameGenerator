using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using UnityEngine;

public class NameMaker //: MonoBehaviour
{
    int train_num = 90000;
    string[] lines = System.IO.File.ReadAllLines(@"Assets/names.txt");
    const string pool = "abcdefghijklmnoprstuvxyz";// removed q w -
    
    //float[,] probs = new float[pool.Length,pool.Length]; //unoptimised array

    public static float[,,] probs = new float[20,pool.Length,pool.Length];
    
    float[,,] init(){
        for(int i = 0; i < probs.GetLength(0); i++){
            for(int j = 0; j < probs.GetLength(1); j++){
                for(int k = 0; k < probs.GetLength(2); k++){
                    probs[i,j,k] = 0.5f;
                }
            }
        }
        return probs;
    }

//Optimized version
    
    public void train(){
            //initialize
            init();
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            // sample.All((m)=>{
            //     // do something you want

            //     return true;
            // });

            for (int r = 0; r <= train_num; r++){
                string sample = lines[UnityEngine.Random.Range(0, lines.Length)].ToLower();
                for (int sample_i = 0; sample_i < sample.Length; sample_i++) {
                    
                    for (int pool_i = 0; pool_i < pool.Length; pool_i++) {

                        if (sample[sample_i] == pool[pool_i]) { //gets current letter
                            for(int pool_j = 0; pool_j < pool.Length; pool_j++){
                                if(sample_i +1< sample.Length && sample[sample_i + 1] == pool[pool_j]){   //next letter
                                    probs[sample_i,pool_i,pool_j]++;
                                    break;
                                }
                            }
                            break;
                        }
                    }

                    // int i = pool.IndexOf(sample[sample_i].ToString(), 0, StringComparison.Ordinal);

                    // int j = 0;
                    // if(sample_i +1 < sample.Length){
                    //     j = pool.IndexOf(sample[sample_i + 1].ToString(), 0, StringComparison.Ordinal);
                    // }

                    // probs[sample_i,i,j]++;

                }
            }
            sw.Stop();
            Debug.Log("time: " + sw.Elapsed.TotalMilliseconds);
        }

    int probSum(int n,int m){
        float sum = 0;
        for(int i=0; i < probs.GetLength(0); i++){
            sum += probs[n,m,i];
        }
        return (int)sum;
    }


    public string newString() {
        string newS = "";
        newS += pool[UnityEngine.Random.Range(0,pool.Length)];
        
        float cumulative = 0f;
        for (int i = 1; i < 9; i++) // i = letter position
        {
            for(int pool_i = 0; pool_i < pool.Length; pool_i++) {
                if(newS[i-1] == pool[pool_i]){//get current letter
                    //for(int pool_j = 0; pool_j < pool.Length; pool_j++){
                        int diceroll = UnityEngine.Random.Range(0, probSum(i,pool_i));
                        for (int a = 0; a < probs.GetLength(0); a++)
                        {
                            cumulative += probs[i,pool_i,a];
                            if (diceroll < cumulative)
                            {
                                newS += pool[a];//map next letter
                                cumulative = 0f;
                                break;
                            }
                        }
                        break;
                    //}
                    //break;
                }
                
            }
            
        }
        return newS;
    }


//One Dimentional array Version
    //float[] probs = new float[pool.Length];
    // public void train(){
    //     for (int r = 0; r <= train_num; r++){
    //         string sample = lines[Random.Range(0, lines.Length)].ToLower();
    //         for (int sample_i = 0; sample_i < sample.Length; sample_i++) {
    //             for (int pool_i = 0; pool_i < pool.Length; pool_i++) {
    //                 if (sample[sample_i] == pool[pool_i]) {
    //                     probs[pool_i]++;
    //                 }
    //             }
    //         }
    //     }
    // }

//Two dimensional array version 
    // public void train(){
    //     //initialize
    //     for(int i = 0; i < pool.Length; i++){
    //         for(int j = 0; j < pool.Length; j++){
    //             probs[i,j] = 1;
    //         }
    //     }
    //     for (int r = 0; r <= train_num; r++){
    //         string sample = lines[Random.Range(0, lines.Length)].ToLower();
    //         for (int sample_i = 0; sample_i < sample.Length; sample_i++) {
    //             for (int pool_i = 0; pool_i < pool.Length; pool_i++) {
    //                 if (sample[sample_i] == pool[pool_i]) { //gets current letter
    //                     for(int pool_j = 0; pool_j < pool.Length; pool_j++){
    //                         if(sample_i +1< sample.Length && sample[sample_i + 1] == pool[pool_j]){//next letter
    //                             probs[pool_i,pool_j]++;
    //                             break;
    //                         }
    //                     }
    //                     break;
    //                 }
    //             }
    //         }
    //     }
    // }


//One Dimentional array Version
    // string newString() {
    //     string newS = "";
    //     newS += pool[Random.Range(0,pool.Length)];
    //     float cumulative = 0f;
    //     for (int i = 1; i < 9; i++)
    //     {
    //         int diceroll = Random.Range(0, (int) probs.Sum());
    //         for (int a = 0; a < probs.Length; a++)
    //         {
    //             cumulative += probs[a];
    //             if (diceroll < cumulative)
    //             {
    //                 //Console.WriteLine(diceroll); //Console.WriteLine(cumulative);
    //                 //Console.WriteLine(a);Console.WriteLine(pool[a]);
    //                 newS += pool[a];
    //                 cumulative = 0f;
    //                 break;
    //             }
    //         }
    //     }
    //     return newS;
    // }



// Two dimensional array version
    // int probSum(int n){
    //     float sum = 0;
    //     for(int i=0; i < probs.GetLength(0); i++){
    //         sum += probs[n,i];
    //     }
    //     return (int)sum;
    // }

    // string newString() {
    //     string newS = "";
    //     newS += pool[Random.Range(0,pool.Length)];
        
    //     float cumulative = 0f;
    //     for (int i = 1; i < 9; i++) // i = letter position
    //     {
    //         for(int pool_i = 0; pool_i < pool.Length; pool_i++) {
    //             //used this to find out Latin doesn't have Q and W
    //             // Debug.Log("newS : " + newS.Length);
    //             // Debug.Log("pool's index : " + pool_i);
    //             if(newS[i-1] == pool[pool_i]){//get current letter
    //                 for(int pool_j = 0; pool_j < pool.Length; pool_j++){

    //                     int diceroll = Random.Range(0, probSum(pool_i));
    //                     for (int a = 0; a < probs.GetLength(0); a++)
    //                     {
    //                         cumulative += probs[pool_i,a];
    //                         if (diceroll < cumulative)
    //                         {
    //                             newS += pool[a];//map next letter
    //                             cumulative = 0f;
    //                             break;
    //                         }
    //                     }
    //                     break;
    //                 }
    //                 break;
    //             }
                
    //         }
            
    //     }
    //     return newS;
    // }


    // Start is called before the first frame update
    void Start()
    {
        train();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(newString());
    }
}
