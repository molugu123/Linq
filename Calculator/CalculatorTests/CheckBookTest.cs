using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator.CheckBook;
using System.Linq;
using System.Collections.ObjectModel;

namespace CalculatorTests
{
    [TestClass]
    public class CheckBookTest
    {
        [TestMethod]
        public void FillsUpProperly()
        {
            var ob = new CheckBookVM();

            Assert.IsNull(ob.Transactions);

            ob.Fill();

            Assert.AreEqual(12, ob.Transactions.Count);
        }

        [TestMethod]
        public void CountofEqualsMoshe()
        {
            var ob = new CheckBookVM();
            ob.Fill();

            var count = ob.Transactions.Where( t => t.Payee == "Moshe" ).Count();

            Assert.AreEqual(4, count);
        }

        [TestMethod]
        public void SumOfMoneySpentOnFood()
        {
            var ob = new CheckBookVM();
            ob.Fill();

            var category = "Food";

            var food = ob.Transactions.Where(t=> t.Tag == category );

            var total = food.Sum(t => t.Amount);

            Assert.AreEqual(261, total);

        }

        [TestMethod]
        public void Group()
        {
            var ob = new CheckBookVM();
            ob.Fill();

            var total = ob.Transactions.GroupBy(t => t.Tag).Select(g => new { g.Key, Sum = g.Sum(t => t.Amount) });

            Assert.AreEqual(261, total.First().Sum);
            Assert.AreEqual(300, total.Last().Sum);
        }





//-------------------------------- Test One -------------------------------------------------
[TestMethod]
public void AvgAmTag()
{
	var obj = new CheckBookVM();
	obj.Fill();

	var total = obj.Transactions.GroupBy(t => t.Tag).Select(g => new { g.Key, Avg = g.Average(t => t.Amount) });
	Assert.AreEqual(32.625, total.First().Avg);
	Assert.AreEqual(75, total.Last().Avg);
}


//-------------------------------- Test Two -------------------------------------------------
[TestMethod]
public void TotalMoneySpentPerPayee()
{
	var obj = new CheckBookVM();
	obj.Fill();

	var PayToMoshe = obj.Transactions.Where(p => p.Payee == "Moshe").Sum(a => a.Amount);
	Assert.AreEqual(130, PayToMoshe);

	var PayToTim = obj.Transactions.Where(p => p.Payee == "Tim").Sum(a => a.Amount);
	Assert.AreEqual(300, PayToTim);

	var PayToBracha = obj.Transactions.Where(p => p.Payee == "Bracha").Sum(a => a.Amount);
	Assert.AreEqual(131, PayToBracha);
}


//-------------------------------- Test Three -------------------------------------------------
[TestMethod]
public void TotalMoneySpentOnFdPerPayee()
{
	var obj = new CheckBookVM();
	obj.Fill();

	var PayToMoshe = obj.Transactions.Where(p => p.Payee == "Moshe" && p.Tag == "Food").Sum(a => a.Amount);
	Assert.AreEqual(130, PayToMoshe);
    
	var PayToTim = obj.Transactions.Where(p => p.Payee == "Tim" && p.Tag == "Food").Sum(a => a.Amount);
	Assert.AreEqual(0, PayToTim);

	var PayToBracha = obj.Transactions.Where(p => p.Payee == "Bracha" && p.Tag == "Food").Sum(a => a.Amount);
	Assert.AreEqual(131, PayToBracha);
}


//-------------------------------- Test Four -------------------------------------------------
[TestMethod]
public void TranBtwn5and7th()
{
    var obj = new CheckBookVM();
    obj.Fill();

    var dates = obj.Transactions.Where(t => t.Date.ToShortDateString() == "4/6/2015").Select(t => t.Date).ToArray();
    Assert.AreEqual("4/6/2015", dates[0].ToShortDateString());
    Assert.AreEqual("4/6/2015", dates[1].ToShortDateString());
    Assert.AreEqual("4/6/2015", dates[2].ToShortDateString());
}


//-------------------------------- Test Five -------------------------------------------------
[TestMethod]
public void DtsOfTrans()
{
    var obj = new CheckBookVM();
    obj.Fill();

    var trans = obj.Transactions.Where(t => t.Account == "Checking").Select(t => t.Date).ToArray();
    Assert.AreEqual("4/7/2015", trans[0].ToShortDateString());
    Assert.AreEqual("4/5/2015", trans[1].ToShortDateString());
    Assert.AreEqual("4/6/2015", trans[2].ToShortDateString());
    Assert.AreEqual("4/3/2015", trans[3].ToShortDateString());
    Assert.AreEqual("4/2/2015", trans[4].ToShortDateString());
    Assert.AreEqual("4/6/2015", trans[5].ToShortDateString());

    trans = obj.Transactions.Where(t => t.Account == "Credit").Select(t => t.Date).ToArray();
    Assert.AreEqual("4/7/2015", trans[0].ToShortDateString());
    Assert.AreEqual("4/6/2015", trans[1].ToShortDateString());
    Assert.AreEqual("4/5/2015", trans[2].ToShortDateString());
    Assert.AreEqual("4/4/2015", trans[3].ToShortDateString());
    Assert.AreEqual("4/3/2015", trans[4].ToShortDateString());
    Assert.AreEqual("4/2/2015", trans[5].ToShortDateString());
}
//-------------------------------- Test Six -------------------------------------------------
[TestMethod]
public void AccForMostAutoExps()
{
    var obj = new CheckBookVM();
    obj.Fill();

    var max = obj.Transactions.GroupBy(t => t.Account).Select(g => new { g.Key, Account = g.Select(t => t.Tag == "Auto") }).Max(t => t.Key);
    Assert.AreEqual("Credit", max);
}


//-------------------------------- Test Seven -------------------------------------------------
[TestMethod]
public void CntOfTransBtn5and7th()
{
    var obj = new CheckBookVM();
    obj.Fill();

    var dates = obj.Transactions.Where(t => t.Date.ToShortDateString() == "4/6/2015").Select(t => t.Date).ToArray().Length;
    Assert.AreEqual(3, dates);
}


        }
    }

