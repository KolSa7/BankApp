using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp12E
{
    [TestFixture]
    public class BankTest
    {
        Bank bank;

        [SetUp]
        public void SetUp()
        {
            bank = new Bank();
            bank.CreateAccount("1234", "Gipsz Jakab");
            bank.CreateAccount("5678", "Koczka István");
        }

        [Test]
        public void StartingBalanceIsZero()
        {
            Assert.That(bank.GetBalance("1234"), Is.Zero);
        }

        [Test]
        public void DepositOnce()
        {
            bank.Deposit("1234", 10000);
            Assert.That(bank.GetBalance("1234"), Is.EqualTo(10000));
        }

        [Test]
        public void DepositMultiple()
        {
            bank.Deposit("1234", 10000);
            bank.Deposit("1234", 15000);
            Assert.That(bank.GetBalance("1234"), Is.EqualTo(25000));
        }

        [Test]
        public void DepositNegativeAmount()
        {
            Assert.That(() => bank.Deposit("1234", -10000), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void DepositMultipleAccounts()
        {

            bank.Deposit("1234", 10000);
            Assert.That(bank.GetBalance("1234"), Is.EqualTo(10000));
            Assert.That(bank.GetBalance("5678"), Is.Zero);
        }

        [Test]
        public void GetBalanceOfInvalidAccount()
        {
            Assert.That(() => bank.GetBalance("2345"), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void DepositToInvalidAccount()
        {
            Assert.That(() => bank.Deposit("2345", 5000), Throws.TypeOf<ArgumentException>());
        }


        [Test]
        public void WithdrawFromInvalidAccount()
        {
            Assert.That(() => bank.Withdraw("2345", 5000), Throws.TypeOf<ArgumentException>());
        }
        [Test]
        public void WithdrawNegativeAmount()
        {
            Assert.That(() => bank.Withdraw("1234", -10000), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void WithdrawTooMuch()
        {
            Assert.That(bank.Withdraw("1234", 10000), Is.False);
        }

        [Test]
        public void WithdrawResult()
        {
            bank.Deposit("1234", 10000);
            Assert.That(bank.Withdraw("1234", 1000), Is.True);
            Assert.That(bank.GetBalance("1234"), Is.EqualTo(9000));
        }


        [Test]
        public void TransferAccountsValid()
        {
            bank.Deposit("1234", 10000);
            Assert.That(() => bank.Transfer("1234", "56789", 1000), Throws.TypeOf<ArgumentException>());
            Assert.That(() => bank.Transfer("12345", "5678", 1000), Throws.TypeOf<ArgumentException>());
            Assert.That(() => bank.Transfer("1234", "1234", 1000), Throws.TypeOf<ArgumentException>());
            Assert.That(bank.GetBalance("1234"), Is.EqualTo(10000));
        }
        [Test]
        public void TransferAmountIsValid()
        {
            bank.Deposit("1234", 10000);
            Assert.That(() => bank.Transfer("1234", "5678", 0), Throws.TypeOf<ArgumentException>());
            Assert.That(() => bank.Transfer("1234", "5678", -1000), Throws.TypeOf<ArgumentException>());
            Assert.That(bank.GetBalance("1234"), Is.EqualTo(10000));
        }
        [Test]
        public void TransferAmountIsPossible()
        {
            bank.Deposit("1234", 10000);
            Assert.That(bank.Transfer("1234", "5678", 11000), Is.False);
            Assert.That(bank.GetBalance("1234"), Is.EqualTo(10000));
            Assert.That(bank.GetBalance("5678"), Is.EqualTo(0));
        }
        [Test]
        public void RightTransfer()
        {
            bank.Deposit("1234", 10000);
            Assert.That(bank.Transfer("1234", "5678", 1000), Is.True);
            Assert.That(bank.GetBalance("1234"), Is.EqualTo(9000));
            Assert.That(bank.GetBalance("5678"), Is.EqualTo(1000));
        }


        [Test]
        public void GetBalanceOfClosedAccount()
        {
            bank.CloseAccount("1234");
            Assert.That(() => bank.GetBalance("1234"), Throws.TypeOf<ArgumentException>());
        }
        [Test]
        public void CloseAccountWithBalance()
        {
            bank.Deposit("1234", 10000);
            Assert.That(() => bank.CloseAccount("1234"), Throws.TypeOf<InvalidOperationException>());
        }
        [Test]
        public void CloseAccountWithInvalidId()
        {
            Assert.That(() => bank.CloseAccount("2345"), Throws.TypeOf<ArgumentException>());
        }


        [Test]
        public void newAccountWithSameName()
        {
            Assert.That(bank.CreateAccount("8153", "Gipsz Jakab"),Is.False);
        }
        [Test]
        public void IdCheck()
        {
            Assert.That(() => bank.CreateAccount("1234","Nagy Endre"), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void newAccount()
        {
            Assert.That(bank.CreateAccount("8153", "Nagy Endre"), Is.True);
        }

        [Test]
        public void GetOwnerOfNonExistantAccount()
        {
            Assert.That(bank.GetOwner("3465"), Is.Null);
        }
        [Test]
        public void GetOwner()
        {
            Assert.That(bank.GetOwner("1234"), Is.EqualTo("Gipsz Jakab"));
        }
    }
}
