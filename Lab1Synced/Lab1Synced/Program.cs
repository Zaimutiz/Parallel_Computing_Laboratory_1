//Real Life Situation could be any Inventory Management System 
//The is no problem here because when one of the functions take the stock parameter it locks it and the other thread
//cannot use it untill it is changed/released.
using System;
using System.Threading;

class Inventory
{
    private int stock = 100;
    private readonly object stockLock = new object();
    //Below there is 2 critical sections by adding and removing stock.
    public void AddStock(int quantity)
    {
        lock (stockLock)
        {
            Thread.Sleep(10);
            stock += quantity;  
        }
    }
    public void RemoveStock(int quantity)
    {
        lock (stockLock)
        {
            Thread.Sleep(10);
            stock -= quantity;
        }
    }
    public int GetStock()
    {
        lock (stockLock)
        {
            return stock;
        }
    }
}
public class SynchronizedInventory
{
    public static void Main(string[] args)
    {
        for (int run = 1; run <= 5; run++)  
        {
            Inventory inventory = new Inventory();
            Thread addThread = new Thread(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    inventory.AddStock(10);
                }
            });
            Thread removeThread = new Thread(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    inventory.RemoveStock(10);
                }
            });
            addThread.Start();
            removeThread.Start();
            addThread.Join();
            removeThread.Join();
            Console.WriteLine("Final stock: " + inventory.GetStock());
        }
    }
}
