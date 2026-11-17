using System.Security.Principal;

namespace BankApp12E
{
    public class Bank
    {
        public Dictionary<string, int> accounts = new Dictionary<string, int>();

        public void CreateAccount(string account)
        {
            accounts.Add(account, 0);
        }

        public int GetBalance(string account)
        {
            if (!accounts.ContainsKey(account))
            {
                throw new ArgumentException("Invalid account");
            }
            return accounts[account];
        }

        public void Deposit(string account, int amount)
        {
            if (!accounts.ContainsKey(account))
            {
                throw new ArgumentException("Invalid account");
            }
            if (amount < 0)
            {
                throw new ArgumentException("Invalid amount");
            }
            accounts[account] += amount;
        }
        public bool Withdraw(string account, int amount) 
        {
            if (!accounts.ContainsKey(account))
            {
                throw new ArgumentException("Invalid account");
            }
            if (amount < 0)
            {
                throw new ArgumentException("Invalid amount");
            }
            if (accounts[account] < amount) 
            {
                return false;
            }
            accounts[account]-=amount;
            return true;
        }

        public bool Transfer(string fromAccount, string toAccount, int amount)
        {
            if (!accounts.ContainsKey(fromAccount)||!accounts.ContainsKey(toAccount)||fromAccount==toAccount)
            {
                throw new ArgumentException("Invalid account");
            }
            if (amount <= 0)
            {
                throw new ArgumentException("Invalid amount");
            }
            if (accounts[fromAccount] >= amount)
            {
                accounts[fromAccount] -= amount;
                accounts[toAccount] += amount;
                return true;
            }
            return false;
        }

        public void CloseAccount(string account)
        {
            if (!accounts.ContainsKey(account)) 
            { 
                throw new ArgumentException("Invalid account");
            }
            if (accounts[account] > 0) 
            {
                throw new InvalidOperationException("You cant close an account with a balance");
            }
            accounts.Remove(account);
        }
    }
}
