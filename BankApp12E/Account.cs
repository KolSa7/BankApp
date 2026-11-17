using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp12E
{
    internal class Account
    {
        string name;
        int balance;

        public Account(string name, int balance=0)
        {
            this.Name = name;
            this.Balance = balance;
        }

        public string Name { get => name; set => name = value; }
        public int Balance { get => balance; set => balance = value; }
    }
}
