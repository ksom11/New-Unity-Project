using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Person : MonoBehaviour
{
    public bool dead = false;
    public bool infected = false;
    public bool isolated= false;
    public bool isbroken= false;
    public int isolatedtime = 0;
    public int isbrokentime = 0;
    public int yearsold=0;
    public float money = 0;
    public float make = 0;
    public float cost = 0;
    public float deathchance = 0;
    float infectdes =2;

    public string job = "";
    // Start is called before the first frame update
    void Start()
    {
        Data.persons.Add(this);
        randset();
        randcread();
        
        //getperson();
    }

    private void Update()
    {
        //nomal 310
        //Debug.Log("ssss");240
        //GameObject A = new GameObject();50
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        
        if(!dead)
        {
            //if(this.transform.position.x<500&& this.transform.position.x > -500 && this.transform.position.z < 500 && this.transform.position.z > -500)
            //{
            //Debug.Log("runing");
            if (isolated)
            {
                isolatedtime++;
            }

            if (isolatedtime > 1000)
            {
                isolated = false;
                infected = false;
                isolatedtime = 0;
            }

            if (isbroken)
            {
                isbrokentime++;
            }

            if (isbrokentime > 1000)
            {
                //Destroy(this.gameObject);
                //this.gameObject.SetActive(false);
                dead = true;
                Data.deadNum++;
            }


            if (!isolated)
            {
                pmove(this.gameObject);
            }

            
            if (!isolated)
            {
                if (getinf(this.gameObject))
                {
                    infected = true;
                    Data.infledNum++;
                    isolate();

                }
            }
            

            if (!isolated)
            {
                makemoney();
            }


            costmoney();
            randdead(this);
        }

        

    }

    void randdead(Person P)
    {
        if(infected)
        {
            if(UnityEngine.Random.Range(0,10000)==50)
            {
                dead = true;
                Data.persons.Remove(P);
                Data.deadNum++;
            }
        }
    }

    void randcread()
    {
        var v = this.transform.position;
        yearsold =UnityEngine. Random.Range(1, 101);
        money = UnityEngine.Random.Range(10000, 1000000);
        //Debug.Log(1 / magnitude(v));
        cost= UnityEngine.Random.Range(50, 1000);
        make= UnityEngine.Random.Range(100, 10000) ;
    }

    void pmove(GameObject person)
    {
        switch (UnityEngine. Random.Range(0, 5))
        {
            case 0:
                {
                    var v = person. transform.position;
                    v.x = v.x + 1f;
                    person.transform.position = v;
                    break;
                }

            case 1:
                {
                    var v = person.transform.position;
                    v.x = v.x - 1f;
                    person.transform.position = v;
                    break;
                }
            case 2:
                {
                    var v = person.transform.position;
                    v.z = v.z + 1f;
                    person.transform.position = v;
                    break;
                }
            case 3:
                {
                    var v = person.transform.position;
                    v.z = v.z - 1f;
                    person.transform.position = v;
                    break;
                }
        }
    }

    void makemoney()
    {
        money = money + make;
    }

    void costmoney()
    {
        if(!isbroken)
        {
            if (money > cost)
            {
                money = money - cost;
            }

            else
            {
                isbroken = true;
                Data.brokenNum++;
            }
        }
    }

    void isolate()
    {
        isolated = true;
    }

    bool getinf(GameObject person)
    //void getperson()
    {
        //GameObject[] obj = FindObjectOfType(typeof( GameObject)) as GameObject[];
        /*
        List<GameObject> ob = new GameObject();
        foreach(GameObject i in GameObject.Find("Person"))
        {

        }
        */
        //GameObject.Find("Person");
        //GameObject[] all = { };
        //var all = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        //all.Initialize();
        var p = person.GetComponent<Person>();
        foreach (var item in Data.persons)
        {
            //item.name.Contains("Cube (")&&
            if (item!=person)
            {
                
                var i = item.GetComponent<Person>();
                if (p.dead == false && i.dead == false)
                {
                    /*
                    var v = person.transform.position;
                    var vv = item.transform.position;
                    var des = vv - v;
                    */
                    //person.transform.position- item.transform.position
                    if ((person.transform.position - item.transform.position).sqrMagnitude < infectdes)
                    {
                        //Debug.Log("des:" + magnitude(des));
                        //Debug.Log(person.name + " : " + item.name);
                        if (p.infected == true || i.infected == true)
                        {
                            return true;
                        }

                    }
                    
                }
                
                
            }
        }
        return false;
    }
    
    public float magnitude(Vector3 v)
    {

        //自身各分量平方运算.
        /*
        float X = v.x * v.x;
        float Y = v.y * v.y;
        float Z = v.z * v.z;
        */
        //return v.x;
        //return v.x + v.y + v.z;
        return v.sqrMagnitude;
            //return Mathf.Sqrt(X + Y + Z);//开根号,最终返回向量的长度/模/大小.
        

    }

    void randset()
    {
        var v = this.transform.position;
        v.x = UnityEngine.Random.Range(-50, 50);
        v.z = UnityEngine.Random.Range(-50, 50);
        this.transform.position = v;
    }

    
}
