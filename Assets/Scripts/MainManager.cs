using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    private int test = 0;
    private float count = 5;

    private void Awake() {
        if(Instance != null) return;

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        Customer.Instance.init(Data.customers[0]);
    }

    private void Update() {
        if(Customer.Instance.arrived()){
            count -= Time.deltaTime;
            if(count < 0){
                count = 1.5f;
                Customer.Instance.state = Customer.CustomerState.exit;
            }
        }else if(Customer.Instance.exited()){
            test = next();
            Customer.Instance.init(Data.customers[test]);
        }
    }

    private int next(){
        for(int i = 0; i < Data.customers.Count; i++){
            test = (test + 1) % Data.customers.Count;
            if(Data.customers[test].normal != null) return test;
        }
        return test;
    }
}
