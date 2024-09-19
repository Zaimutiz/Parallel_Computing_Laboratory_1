//Real Life Situation could be any Inventory Management System
//The problem is that the stock parameter is not locked and can be used at the same time giving the same first input
//but different output if the threads use it at the same time
using System;
using System.Threading;

class Inventory
{
    private int stock = 100; 
    //Below there is 2 critical sections by adding and removing stock.
    public void AddStock(int quantity)
    {
        Thread.Sleep(10);
        stock += quantity;
    }
    public void RemoveStock(int quantity)
    {
        Thread.Sleep(10);
        stock -= quantity;
    }
    public int GetStock()
    {
        return stock;
    }
}
public class InventoryRaceCondition
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
