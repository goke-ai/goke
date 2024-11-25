namespace Goke.Web.Shared.Models.Banks;

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
            // savings.PerformMonthEndTransactions();

            for (int i = 0; i < 20; i++)
            {
                decimal amount = (decimal)Goke.FakeGen.FakeGen.GetMONEY();
                DateTime future = DateTime.Now.AddDays(Random.Shared.Next(0, i));
                savings.Deposit(amount + 1, future, Goke.FakeGen.FakeGen.GetRECEIVETRANSACTIONS());
                amount = (decimal)Goke.FakeGen.FakeGen.GetMONEY();
                future = DateTime.Now.AddDays(Random.Shared.Next(0, i));
                savings.Withdrawal(amount + 1, future, Goke.FakeGen.FakeGen.GetPAYTRANSACTIONS());
            }
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




   
}
