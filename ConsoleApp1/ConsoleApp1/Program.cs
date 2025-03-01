// See https://aka.ms/new-console-template for more information
using System;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Diagnostics;

public class Class1
{
    static void Main(string[] args)
    {
        //ConcurrentTest();
        //MutexLockTest();
        //transferDeadlockTest();
        //transferDeadlockPreventionTest();
        //transferDeadlockSolveTest();
        //transferStressTest();
        //pipesImplimentation();
    }

    static void ConcurrentTest()
    {
        Console.WriteLine("Starting Concurrent Test");
        
        //Creation Of Bank Accounts
        bankAccount Account1 = new bankAccount(1000, "A1", 1);
        bankAccount Account2 = new bankAccount(1000, "A2", 2);

        //Creation Of Customer Threads
        Thread Customer1 = new Thread(() => Account1.directWithdraw(300));
        Thread Customer2 = new Thread(() => Account2.directDeposit(500));

        //Naming Of Customer Threads
        Customer1.Name = "Cus1";
        Customer2.Name = "Cus2";

        //Starting Customer Threads
        Customer1.Start();
        Customer2.Start();
    }

    static void MutexLockTest() //Used To Test Mutex Locking
    {
        Console.WriteLine("Starting Mutex Lock Test");
        bankAccount Account1 = new bankAccount(500, "A1", 1);
        Thread Customer1 = new Thread(() => Account1.directWithdraw(200));
        Thread Customer2 = new Thread(() => Account1.directWithdraw(200));
        

        Customer1.Name = "Cus1";
        Customer2.Name = "Cus2";

        Customer1.Start();
        Customer2.Start();
    }

    static void transferDeadlockTest() //Used To Induce Deadlock
    {
        Console.WriteLine("Starting transfer Deadlock Test");
        bankAccount Account1 = new bankAccount(1000, "A1", 1);
        bankAccount Account2 = new bankAccount(1000, "A2", 2);

        //Creation Of Account Transfer Managers
        accountTransferManager TransferManager1 = new accountTransferManager(Account1, Account2, 300);
        accountTransferManager TransferManager2 = new accountTransferManager(Account2, Account1, 300);
        Thread Customer1 = new Thread(() => TransferManager1.transferDeadlocked());
        Thread Customer2 = new Thread(() => TransferManager2.transferDeadlocked());

        Customer1.Name = "Cus1";
        Customer2.Name = "Cus2";

        Customer1.Start();
        Customer2.Start();

    }

    static void transferDeadlockPreventionTest() //Used To Test Resource Ordinging To Prevent Deadlock
    {
        Console.WriteLine("Starting transfer Deadlock Prevention Test");
        bankAccount Account1 = new bankAccount(1000, "A1", 1);
        bankAccount Account2 = new bankAccount(1000, "A2", 2);
        accountTransferManager TransferManager1 = new accountTransferManager(Account1, Account2, 300);
        accountTransferManager TransferManager2 = new accountTransferManager(Account2, Account1, 300);
        Thread Customer1 = new Thread(() => TransferManager1.transferPrevention());
        Thread Customer2 = new Thread(() => TransferManager2.transferPrevention());

        Customer1.Name = "Cus1";
        Customer2.Name = "Cus2";

        Customer1.Start();
        Customer2.Start();
    }

    static void transferDeadlockSolveTest() //Used To Test Mutex Timeout In Order To Solve Deadlock
    {
        Console.WriteLine("Starting transfer Deadlock Solve Test");
        bankAccount Account1 = new bankAccount(1000, "A1", 1);
        bankAccount Account2 = new bankAccount(1000, "A2", 2);
        accountTransferManager TransferManager1 = new accountTransferManager(Account1, Account2, 300);
        accountTransferManager TransferManager2 = new accountTransferManager(Account2, Account1, 300);
        Thread Customer1 = new Thread(() => TransferManager1.transferResolution());
        Thread Customer2 = new Thread(() => TransferManager2.transferResolution());

        Customer1.Name = "Cus1";
        Customer2.Name = "Cus2";

        Customer1.Start();
        Customer2.Start();
    }

    static void transferStressTest() //Used To Stress Test All Multithreading Systems Using 10 Different Customer Threads
    {
        Console.WriteLine("Starting Stress Test");
        
        bankAccount Account1 = new bankAccount(1000, "A1", 1);
        bankAccount Account2 = new bankAccount(1000, "A2", 2);
        bankAccount Account3 = new bankAccount(1000, "A3", 3);
        bankAccount Account4 = new bankAccount(1000, "A4", 4);
        bankAccount Account5 = new bankAccount(1000, "A5", 5);
        bankAccount Account6 = new bankAccount(1000, "A6", 6);
        bankAccount Account7 = new bankAccount(1000, "A7", 7);
        bankAccount Account8 = new bankAccount(1000, "A8", 8);
        bankAccount Account9 = new bankAccount(1000, "A9", 9);

        accountTransferManager TransferManager1 = new accountTransferManager(Account1, Account2, 300);
        accountTransferManager TransferManager2 = new accountTransferManager(Account2, Account1, 300);
        accountTransferManager TransferManager3 = new accountTransferManager(Account3, Account4, 300);
        accountTransferManager TransferManager4 = new accountTransferManager(Account4, Account5, 300);
        accountTransferManager TransferManager5 = new accountTransferManager(Account8, Account9, 300);

        Thread Customer1 = new Thread(() => TransferManager1.transferFusion());
        Thread Customer2 = new Thread(() => TransferManager2.transferFusion());
        Thread Customer3 = new Thread(() => TransferManager3.transferFusion());
        Thread Customer4 = new Thread(() => TransferManager4.transferFusion());
        Thread Customer5 = new Thread(() => TransferManager5.transferFusion());
        Thread Customer6 = new Thread(() => Account6.directDeposit(400));
        Thread Customer7 = new Thread(() => Account6.directWithdraw(350));
        Thread Customer8 = new Thread(() => Account5.directDeposit(1000));
        Thread Customer9 = new Thread(() => Account9.directWithdraw(400));
        Thread Customer10 = new Thread(() => Account7.directWithdraw(1000));

        Customer1.Name = "Cus1";
        Customer2.Name = "Cus2";
        Customer3.Name = "Cus3";
        Customer4.Name = "Cus4";
        Customer5.Name = "Cus5";
        Customer6.Name = "Cus6";
        Customer7.Name = "Cus7";
        Customer8.Name = "Cus8";
        Customer9.Name = "Cus9";
        Customer10.Name = "Cus10";

        Customer1.Start();
        Customer2.Start();
        Customer3.Start();
        Customer4.Start();
        Customer5.Start();
        Customer6.Start();
        Customer7.Start();
        Customer8.Start();
        Customer9.Start();
        Customer10.Start();
    }

    static void pipesImplimentation()
    {
        startPipeSever(); //Starts The Pipe Server Method
        
        Task.Delay(1000).Wait();

        var client = new NamedPipeClientStream("Talk Pipes");
        client.Connect(); //Connects This Client To The Server
        StreamReader reader = new StreamReader(client);
        StreamWriter writer = new StreamWriter(client);

        while(true)
        {
            Console.WriteLine("Enter Your Name");
            string input = Console.ReadLine(); //Reads User Input
            Stopwatch sw = Stopwatch.StartNew();
            if(String.IsNullOrEmpty(input)) break;
            writer.WriteLine(input);
            writer.Flush();
            Console.WriteLine(reader.ReadLine());
            sw.Stop();
            Console.WriteLine("Time Elapsed: " + sw.ElapsedMilliseconds + " milliseconds");
        }
    }
    static void startPipeSever()
    {
        Task.Factory.StartNew(() =>
        {
            var server = new NamedPipeServerStream("Talk Pipes");
            server.WaitForConnection(); //Waits For Client To Conncet
            StreamReader reader = new StreamReader(server);
            StreamWriter writer = new StreamWriter(server);

            while(true)
            {
                var line = reader.ReadLine(); //Recives Client Input String
                Console.WriteLine();
                Console.WriteLine("Server Return");
                writer.WriteLine(line + " is a dumb name"); //Returns Client Input Along with " is a dumb name"
                writer.Flush();
            }
        }
        );
    }


}

public class bankAccount
{
    public Mutex accountBalenceLock = new Mutex(); //Mutex That Is Used In accountTransferManager To Prevent The Account's Balance From Being Manipulated
                                                   //By More Than One Thread.
    int balance {get; set; }
    public string accountName { get; set; }

    public int accountID { get; set; } //An int That Aids In Ensuring Proper Resource Ordering For The Various Bankaccounts

    public bankAccount(int bal, string n, int id)
    {
        balance = bal;
        accountName = n;
        accountID = id;
    }

    public bool withdraw(int amount) //Withdraw Method To Be Used With accountTransferManager
    {
        if((balance - amount) < 0) //Protection Used To Prevent A Withdraw That Would Leave An Account With Negative Money
        {
            Console.WriteLine("Failed Withdraw Transaction. Only $" + balance + " in account.");
            return false;
        }
        else
        {
            Thread.Sleep(1000); 
            Console.WriteLine(Thread.CurrentThread.Name + " Removed ${0} from " + accountName + " with ${1} remaining.", amount, (balance - amount));
            balance -= amount;
            Thread.Sleep(1000);
            return true;
        }
    }

    public void deposit(int amount)//Deposit Method To Be Used With accountTransferManager
    {
        Console.WriteLine( Thread.CurrentThread.Name + " Added ${0} to " + accountName + " with the new total being ${1}", amount, (balance + amount));
        balance += amount;
        Thread.Sleep(1000);
    }

    public void directWithdraw(int amount) //Withdraw Method To Be Used Directly By Customer Threads
    {
        if((balance - amount) < 0) //Protection Used To Prevent A Withdraw That Would Leave An Account With Negative Money
        {
            Console.WriteLine("Failed Withdraw Transaction. Only $" + balance + " in account.");
        }
        else
        {
            Console.WriteLine(Thread.CurrentThread.Name + " is trying to aquire " + accountName + " Mutex");
            accountBalenceLock.WaitOne();
            Thread.Sleep(1000); 
            Console.WriteLine(Thread.CurrentThread.Name + " has aquired " + accountName + " Mutex");
            Console.WriteLine("Succesful Withdraw");
            Console.WriteLine(Thread.CurrentThread.Name + " Removed ${0} from " + accountName + " with ${1} remaining.", amount, (balance - amount));
            balance -= amount;
            Thread.Sleep(1000);
            Console.WriteLine(Thread.CurrentThread.Name + " has released " + accountName + " Mutex");
            accountBalenceLock.ReleaseMutex();
        }
    }

    public void directDeposit(int amount)//Deposit Method To Be Used Directly By Customer Threads
    {
        Thread.Sleep(1000); 
        Console.WriteLine(Thread.CurrentThread.Name + " is trying to aquire " + accountName + " Mutex");
        accountBalenceLock.WaitOne();
        Thread.Sleep(1000); 
        Console.WriteLine(Thread.CurrentThread.Name + " has aquired " + accountName + " Mutex");
        Console.WriteLine("Succesful Deposit");
        Console.WriteLine( Thread.CurrentThread.Name + " Added ${0} to " + accountName + " with the new total being ${1}", amount, (balance + amount));
        balance += amount;
        Thread.Sleep(1000);
        Console.WriteLine(Thread.CurrentThread.Name + " has released " + accountName + " Mutex");
        accountBalenceLock.ReleaseMutex();
    }

}

public class accountTransferManager //Class That Is Used To Transfer Money Between Two Objects Of The bankAccount Class
{
    bankAccount withdrawAccount; //Bank Account Being Taken From
    bankAccount depositAccount; //Bank Account Being Given To
    int transferAmount;

    public accountTransferManager(bankAccount wA, bankAccount dA, int tA)
    {
        withdrawAccount = wA;
        depositAccount = dA;
        transferAmount = tA;
    }

    public void transferDeadlocked() //Transfer Scenario That Induses Deadlock When Two Threads Want The Same Two Account Mutexes, But Grabs Them In Opposite Orders.
    {
        Console.WriteLine(Thread.CurrentThread.Name + " Trying To Aquire " + withdrawAccount.accountName + " Mutex");
        Thread.Sleep(1000);
        withdrawAccount.accountBalenceLock.WaitOne();
        Console.WriteLine(Thread.CurrentThread.Name + " Aquired " + withdrawAccount.accountName + " Mutex");
        Thread.Sleep(1000);
        Console.WriteLine(Thread.CurrentThread.Name + " Trying To Aquire " + depositAccount.accountName + " Mutex");
        Thread.Sleep(1000);
        depositAccount.accountBalenceLock.WaitOne();
        Console.WriteLine(Thread.CurrentThread.Name + " Aquired " + depositAccount.accountName + " Mutex");
        Thread.Sleep(1000);
        if(withdrawAccount.withdraw(transferAmount))
        {
            Console.WriteLine("Succesful Transfer");
            depositAccount.deposit(transferAmount);
            
        }
        else
        {
            Console.WriteLine("Failed To Transfer Amount Due To Insufficent Funds");
        }
        depositAccount.accountBalenceLock.ReleaseMutex();
        Console.WriteLine(Thread.CurrentThread.Name + " Released " + depositAccount.accountName + " Mutex");
        withdrawAccount.accountBalenceLock.ReleaseMutex();
        Console.WriteLine(Thread.CurrentThread.Name + " Released " + withdrawAccount.accountName + " Mutex");       
    }

    public void transferPrevention() //Transfer Scenario That Makes Use Of Ordered Resources In Order To Prevent Deadlock When Two Threads Are Trying To Grab The Same Mutexes In Different Orders
    {
        if(withdrawAccount.accountID < depositAccount.accountID) //If-Else Statement That Makes It So Account Mutexes Are Locked In Order Of Their IDs
        {
            Console.WriteLine(Thread.CurrentThread.Name + " Trying To Aquire " + withdrawAccount.accountName + " Mutex");
            Thread.Sleep(1000);
            withdrawAccount.accountBalenceLock.WaitOne();
            Console.WriteLine(Thread.CurrentThread.Name + " Aquired " + withdrawAccount.accountName + " Mutex");
            Thread.Sleep(1000);
            Console.WriteLine(Thread.CurrentThread.Name + " Trying To Aquire " + depositAccount.accountName + " Mutex");
            Thread.Sleep(1000);
            depositAccount.accountBalenceLock.WaitOne();
            Console.WriteLine(Thread.CurrentThread.Name + " Aquired " + depositAccount.accountName + " Mutex");
            Thread.Sleep(1000);
            if(withdrawAccount.withdraw(transferAmount))
            {
                Console.WriteLine("Succesful Transfer");
                depositAccount.deposit(transferAmount);
            
            }
            else
            {
                Console.WriteLine("Failed To Transfer Amount Due To Insufficent Funds");
            }
            depositAccount.accountBalenceLock.ReleaseMutex();
            Console.WriteLine(Thread.CurrentThread.Name + " Released " + depositAccount.accountName + " Mutex");
            withdrawAccount.accountBalenceLock.ReleaseMutex();
            Console.WriteLine(Thread.CurrentThread.Name + " Released " + withdrawAccount.accountName + " Mutex");
        }
        else
        {
            Console.WriteLine(Thread.CurrentThread.Name + " Trying To Aquire " + depositAccount.accountName + " Mutex");
            Thread.Sleep(1000);
            depositAccount.accountBalenceLock.WaitOne();
            Console.WriteLine(Thread.CurrentThread.Name + " Aquired " + depositAccount.accountName + " Mutex");
            Thread.Sleep(1000);
            Console.WriteLine(Thread.CurrentThread.Name + " Trying To Aquire " + withdrawAccount.accountName + " Mutex");
            Thread.Sleep(1000);
            withdrawAccount.accountBalenceLock.WaitOne();
            Console.WriteLine(Thread.CurrentThread.Name + " Aquired " + withdrawAccount.accountName + " Mutex");
            Thread.Sleep(1000);
            if(withdrawAccount.withdraw(transferAmount))
            {
                Console.WriteLine("Succesful Transfer");
                depositAccount.deposit(transferAmount);
            
            }
            else
            {
                Console.WriteLine("Failed To Transfer Amount Due To Insufficent Funds");
            }
            depositAccount.accountBalenceLock.ReleaseMutex();
            Console.WriteLine(Thread.CurrentThread.Name + " Released " + depositAccount.accountName + " Mutex");
            withdrawAccount.accountBalenceLock.ReleaseMutex();
            Console.WriteLine(Thread.CurrentThread.Name + " Released " + withdrawAccount.accountName + " Mutex");
        }
        
    }

    public void transferResolution() //Transfer Scenario That Makes Use Of Timeout To Prevent Deadlock With There Being A While Loop To Allow A Timedout Thread To Retry Their Given Task After Releasing Their Mutexes
    {
        bool transferComplete = false;
        while(!transferComplete) //While Loop Used To Allow Threads To Retry Their Given Task If They Fail To Gather The Needed Mutexes
        {
            Console.WriteLine(Thread.CurrentThread.Name + " Trying To Aquire " + withdrawAccount.accountName + " Mutex");
            Thread.Sleep(1000);
            if(withdrawAccount.accountBalenceLock.WaitOne(TimeSpan.FromMilliseconds(500)))
            {
                Console.WriteLine(Thread.CurrentThread.Name + " Aquired " + withdrawAccount.accountName + " Mutex");
                Thread.Sleep(1000);
                Console.WriteLine(Thread.CurrentThread.Name + " Trying To Aquire " + depositAccount.accountName + " Mutex");
                Thread.Sleep(1000);

                if(depositAccount.accountBalenceLock.WaitOne(TimeSpan.FromMilliseconds(500)))
                {
                    Console.WriteLine(Thread.CurrentThread.Name + " Aquired " + depositAccount.accountName + " Mutex");
                    Thread.Sleep(1000);
                    if(withdrawAccount.withdraw(transferAmount))
                    {
                        Console.WriteLine("Succesful Transfer");
                        depositAccount.deposit(transferAmount);
            
                    }
                    else
                    {
                        Console.WriteLine("Failed To Transfer Amount Due To Insufficent Funds");
                    }
                    depositAccount.accountBalenceLock.ReleaseMutex();
                    Console.WriteLine(Thread.CurrentThread.Name + " Released " + depositAccount.accountName + " Mutex");
                    withdrawAccount.accountBalenceLock.ReleaseMutex();
                    Console.WriteLine(Thread.CurrentThread.Name + " Released " + withdrawAccount.accountName + " Mutex");
                    transferComplete = true;
                }
                else
                {
                    Console.WriteLine(Thread.CurrentThread.Name + " Failed To Aquire " + depositAccount.accountName + " Mutex");
                    Console.WriteLine(Thread.CurrentThread.Name + " Is Releasing " + withdrawAccount.accountName + " Mutex");
                    withdrawAccount.accountBalenceLock.ReleaseMutex();
                }
            }
            else
            {
                Console.WriteLine(Thread.CurrentThread.Name + " Failed To Aquire " + withdrawAccount.accountName + " Mutex");
            }

            Thread.Sleep(1000);
        }
        
        
    }

    public void transferFusion() //Transfer Scenario That Is A Fusion Of Ordered Resources And Timeout.
    {
        bool transferComplete = false;
        while(!transferComplete)
        {
            if(withdrawAccount.accountID < depositAccount.accountID)
            {
                Console.WriteLine(Thread.CurrentThread.Name + " Trying To Aquire " + withdrawAccount.accountName + " Mutex");
                Thread.Sleep(1000);
                if(withdrawAccount.accountBalenceLock.WaitOne(TimeSpan.FromMilliseconds(500)))
                {
                    Console.WriteLine(Thread.CurrentThread.Name + " Aquired " + withdrawAccount.accountName + " Mutex");
                    Thread.Sleep(1000);
                    Console.WriteLine(Thread.CurrentThread.Name + " Trying To Aquire " + depositAccount.accountName + " Mutex");
                    Thread.Sleep(1000);

                    if(depositAccount.accountBalenceLock.WaitOne(TimeSpan.FromMilliseconds(500)))
                    {
                        Console.WriteLine(Thread.CurrentThread.Name + " Aquired " + depositAccount.accountName + " Mutex");
                        Thread.Sleep(1000);
                        if(withdrawAccount.withdraw(transferAmount))
                        {
                            Console.WriteLine("Succesful Transfer");
                            depositAccount.deposit(transferAmount);
            
                        }
                        else
                        {
                            Console.WriteLine("Failed To Transfer Amount Due To Insufficent Funds");
                        }
                        depositAccount.accountBalenceLock.ReleaseMutex();
                        Console.WriteLine(Thread.CurrentThread.Name + " Released " + depositAccount.accountName + " Mutex");
                        withdrawAccount.accountBalenceLock.ReleaseMutex();
                        Console.WriteLine(Thread.CurrentThread.Name + " Released " + withdrawAccount.accountName + " Mutex");
                        transferComplete = true;
                    }
                    else
                    {
                        Console.WriteLine(Thread.CurrentThread.Name + " Failed To Aquire " + depositAccount.accountName + " Mutex");
                        Console.WriteLine(Thread.CurrentThread.Name + " Is Releasing " + withdrawAccount.accountName + " Mutex");
                        withdrawAccount.accountBalenceLock.ReleaseMutex();
                    }
                }
                else
                {
                    Console.WriteLine(Thread.CurrentThread.Name + " Failed To Aquire " + withdrawAccount.accountName + " Mutex");
                }

                Thread.Sleep(1000);
            }
            else
            {
                Console.WriteLine(Thread.CurrentThread.Name + " Trying To Aquire " + depositAccount.accountName + " Mutex");
                Thread.Sleep(1000);
                if(depositAccount.accountBalenceLock.WaitOne(TimeSpan.FromMilliseconds(500)))
                {
                    Console.WriteLine(Thread.CurrentThread.Name + " Aquired " + depositAccount.accountName + " Mutex");
                    Console.WriteLine(Thread.CurrentThread.Name + " Trying To Aquire " + withdrawAccount.accountName + " Mutex");
                    Thread.Sleep(1000);
                    if(withdrawAccount.accountBalenceLock.WaitOne(TimeSpan.FromMilliseconds(500)))
                    {
                        Console.WriteLine(Thread.CurrentThread.Name + " Aquired " + withdrawAccount.accountName + " Mutex");
                        Thread.Sleep(1000);
                        if(withdrawAccount.withdraw(transferAmount))
                        {
                            Console.WriteLine("Succesful Transfer");
                            depositAccount.deposit(transferAmount);
            
                        }
                        else
                        {
                            Console.WriteLine("Failed To Transfer Amount Due To Insufficent Funds");
                        }
                        withdrawAccount.accountBalenceLock.ReleaseMutex();
                        Console.WriteLine(Thread.CurrentThread.Name + " Released " + withdrawAccount.accountName + " Mutex");
                        depositAccount.accountBalenceLock.ReleaseMutex();
                        Console.WriteLine(Thread.CurrentThread.Name + " Released " + depositAccount.accountName + " Mutex");
                        transferComplete = true;
                    }
                    else
                    {
                        Console.WriteLine(Thread.CurrentThread.Name + " Failed To Aquire " + withdrawAccount.accountName + " Mutex");
                        Console.WriteLine(Thread.CurrentThread.Name + " Is Releasing " + depositAccount.accountName + " Mutex");
                        depositAccount.accountBalenceLock.ReleaseMutex();
                    }
                }
                else
                {
                    Console.WriteLine(Thread.CurrentThread.Name + " Failed To Aquire " + depositAccount.accountName + " Mutex");
                }

                Thread.Sleep(1000);
            }
        }
    }
}
