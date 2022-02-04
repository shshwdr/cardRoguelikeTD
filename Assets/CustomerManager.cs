using Sinbad;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CustomerInfo : BaseInfo
{
    public int hp;
    public float moveSpeed;
    public bool canBlock;
}
public class CustomerManager : Singleton<CustomerManager>
{
    public GameObject finishedGameObject;
    public TMP_Text finishedGameText;

    List<Customer> customers = new List<Customer>();
    public Dictionary<string, CustomerInfo> CustomerInfoDict = new Dictionary<string, CustomerInfo>();
    private void Awake()
    {
        var customerList =  CsvUtil.LoadObjects<CustomerInfo>("Customer");
        foreach(var info in customerList)
        {
            CustomerInfoDict[info.name] = info;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public List<Customer> getAllCustomers()
    {
        return customers;
    }

    public List<Customer> getAllCustoemrsWithMoney()
    {
        return customers.Count == 0?customers: (List<Customer>)customers.Where(x => !x.isDead);
    }

    public void addCustomer(Customer cus)
    {
        customers.Add(cus);
    }

    public void removeCustomer(Customer cus)
    {
        //should separate finishe customer and no money customer
        customers.Remove(cus);

        if(StageManager.Instance.isStageFinished() && customers.Count == 0)
        {
            finishedGameObject.SetActive(true);
            finishedGameText.text = "Finished Level!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
