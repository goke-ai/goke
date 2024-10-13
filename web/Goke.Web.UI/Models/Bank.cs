namespace Goke.Web.UI.Models;

public class Bank
{
    public List<Account.InterestEarning>? InterestEarnings { get; set; }
    public List<Account.LineOfCredit>? LineOfCredits { get; set; }
    public List<Account.GiftCard>? GiftCards { get; set; }

    public static Bank GetSampleBank()
    {
        var data = new Bank()
        {
            InterestEarnings = [
                new("Goke Oladokun", 10000),
                new("Lillian Oladokun", 1000),
                ],
            GiftCards = [
                new("Goke Oladokun", 100, 50),
                new("Lola Oladokun", 100, 50),
                new("Gbola Oladokun", 100, 50),
                ],
            LineOfCredits = [
                new("Goke Oladokun", 0, 2000),
                new("Lillian Oladokun", 0, 2500),
                ],

        };

        foreach (var giftCard in data.GiftCards)
        {
            giftCard.Withdrawal(20, DateTime.Now, "get expensive coffee");
            giftCard.Withdrawal(50, DateTime.Now, "buy groceries");
            giftCard.PerformMonthEndTransactions();
            giftCard.Deposit(27.50m, DateTime.Now, "add some additional spending money");
        }

        foreach (var savings in data.InterestEarnings)
        {
            savings.Deposit(750, DateTime.Now, "save some money");
            savings.Deposit(1250, DateTime.Now, "Add more savings");
            savings.Withdrawal(250, DateTime.Now, "Needed to pay monthly bills");
            savings.PerformMonthEndTransactions();
        }

        foreach (var lineOfCredit in data.LineOfCredits)
        {
            lineOfCredit.Withdrawal(1000m, DateTime.Now, "Take out monthly advance");
            lineOfCredit.Deposit(50m, DateTime.Now, "Pay back small amount");
            lineOfCredit.Withdrawal(5000m, DateTime.Now, "Emergency funds for repairs");
            lineOfCredit.Deposit(150m, DateTime.Now, "Partial restoration on repairs");
            lineOfCredit.PerformMonthEndTransactions();
        }

        return data;
    }

    public static Bank GetSampleBank(string customer)
    {
        var data = GetSampleBank();
        var a = data.GiftCards?.Where(w => w.Name == customer).ToList();
        var b = data.InterestEarnings?.Where(w => w.Name == customer).ToList();
        var c = data.LineOfCredits?.Where(w => w.Name == customer).ToList();

        return new Bank { GiftCards = a, InterestEarnings = b, LineOfCredits = c };
    }




    /// <summary>
    /// class Account
    /// </summary>
    public class Account
    {
        private static int ACCOUNTNUMBERSEED = 1234567890;

        readonly List<Transaction> _transactions = [];
        readonly decimal _minimumBalance;
        readonly decimal _interestRate;

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Number { get; set; }
        public string Name { get; set; }
        public decimal Balance
        {
            get
            {
                decimal balance = 0;
                foreach (var item in _transactions)
                {
                    balance += item.Amount;
                }

                return balance;
            }
        }

        public Account(string name, decimal initialBalance) : this(name, initialBalance, 0, 0) { }

        public Account(string name, decimal initialBalance, decimal minimumBalance, decimal interestRate)
        {
            Number = GetNewAccountNumber();

            Name = name;
            this._minimumBalance = minimumBalance;
            this._interestRate = interestRate;
            if (initialBalance > 0)
                Deposit(initialBalance, DateTime.Now, "Initial balance");
        }

        private static string GetNewAccountNumber()
        {
            var number = ACCOUNTNUMBERSEED.ToString();
            ACCOUNTNUMBERSEED++;
            return number;
        }

        public void Deposit(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
            }
            var deposit = new Transaction(amount, date, note);
            _transactions.Add(deposit);
        }

        public void Withdrawal(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
            }

            Transaction? overdraftTransaction = CheckWithdrawalLimit(Balance - amount < _minimumBalance);

            Transaction? withdrawal = new(-amount, date, note);

            _transactions.Add(withdrawal);
            if (overdraftTransaction != null)
                _transactions.Add(overdraftTransaction);
        }

        protected virtual Transaction? CheckWithdrawalLimit(bool isOverdrawn)
        {
            if (isOverdrawn)
            {
                throw new InvalidOperationException("Not sufficient funds for this withdrawal");
            }
            else
            {
                return default;
            }
        }

        public virtual void PerformMonthEndTransactions() { }

        public List<Transaction>? GetAccountHistory(DateTime? from, DateTime? to)
        {
            return _transactions.Where(w => from < w.Date && w.Date >= to).ToList();
        }

        public List<Transaction>? GetAllAccountHistory()
        {
            return _transactions;
        }

        public List<Transaction>? GetAccountHistory(int count = 10)
        {
            return _transactions.OrderByDescending(o => o.Date).Take(10).ToList();
        }


        /// <summary>
        /// class InterestEarning
        /// </summary>
        /// <param name="name"></param>
        /// <param name="initialBalance"></param>
        public class InterestEarning(string name, decimal initialBalance) : Account(name, initialBalance, 500m, 0.02m)
        {

            public override void PerformMonthEndTransactions()
            {
                if (Balance > _minimumBalance)
                {
                    decimal interest = Balance * _interestRate;
                    Deposit(interest, DateTime.Now, "apply monthly interest");
                }
            }
        }

        /// <summary>
        /// class LineOfCredit
        /// </summary>
        /// <param name="name"></param>
        /// <param name="initialBalance"></param>
        /// <param name="creditLimit"></param>
        public class LineOfCredit(string name, decimal initialBalance, decimal creditLimit) : Account(name, initialBalance, -creditLimit, 0.07m)
        {
            public override void PerformMonthEndTransactions()
            {
                if (Balance < 0)
                {
                    // Negate the balance to get a positive interest charge:
                    decimal interest = -Balance * _interestRate;
                    Withdrawal(interest, DateTime.Now, "Charge monthly interest");
                }
            }

            protected override Transaction? CheckWithdrawalLimit(bool isOverdrawn) =>
                isOverdrawn
                ? new Transaction(-20, DateTime.Now, "Apply overdraft fee")
                : default;
        }

        /// <summary>
        /// class GiftCard
        /// </summary>
        /// <param name="name"></param>
        /// <param name="initialBalance"></param>
        /// <param name="monthlyDeposit"></param>
        public class GiftCard(string name, decimal initialBalance, decimal monthlyDeposit = 0) : Account(name, initialBalance)
        {
            private readonly decimal _monthlyDeposit = monthlyDeposit;

            public override void PerformMonthEndTransactions()
            {
                if (_monthlyDeposit != 0)
                {
                    Deposit(_monthlyDeposit, DateTime.Now, "Add monthly deposit");
                }
            }
        }
    }

    /// <summary>
    /// class Transaction
    /// </summary>
    public class Transaction
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public decimal Amount { get; }
        public DateTime Date { get; } 
        public string Note { get; }

        public Transaction(decimal amount, DateTime date, string note)
        {
            Amount = amount;
            Date = date;
            Note = note;
        }
    }    
}
