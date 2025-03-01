A project that entailed the implementation of both multiprocess threading and IPC.

The multiprocess threading part of the project was themed around a bank with there being two big classes. A bankAccount class that a thread and withdraw from or deposit money to,
and a accountTransferManager class that handles the transfer of money between two different bankAccount objects. The bankAccount class has three variables, balance, accountName,
and accountID, along with four methods. Said methods are:

**withdraw**-A method that withdraws money from an account's balance and that works with the accountTransferManager

**deposit**-A method that deposits money to an account's balance and that works with the accountTransferManager

**directWithdraw**-A method that withdraws money from an account's balance and is intended to be used directly by a Customer thread.

**directDeposit**-A method that deposits money to an account's balance and is intended to be used directly by a Customer thread.

The accountTransferManager class holds three different variabls, withdrawAccount, depositAccount, and transferAmount, along with different transfer methods
that each handale different situations. These are:

**transferDeadlock**-A method that contains no deadlock prevention or resolution.

**transferPrevention**-A method that makes use of resource ordering to prevent deadlock

**transferResolution**-A method that makes use of Mutex timeout to resolve deadlock

**transferFusion**-A method that is the fusion of **transferPrevention** and **transferResolution**

And the IPC part of this project is very simple in compariosn as it makes use of a client and server method to send strings back and forth between each other.



**Instillation**

To make use of this source code all you need is to download it and open it with a IDE that is capable of using C# .NET.
