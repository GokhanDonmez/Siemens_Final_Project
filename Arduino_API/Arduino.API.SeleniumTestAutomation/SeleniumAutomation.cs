using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ConsoleTables;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;
using System;
using AngleSharp;
using static System.Net.Mime.MediaTypeNames;
using System.Configuration;


class SeleniumAutomation
{
    static string  filePath = String.Empty;
    private static IWebElement textElement = null;
    
   
    static async Task Main(string[] args)
    {
        #region Getting data from browser

        
        IWebDriver driver;
        driver = new ChromeDriver();
        driver.Navigate().GoToUrl("http://localhost:13440/api/Datas");
        driver.Manage().Window.Maximize();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(2000);
        textElement = driver.FindElement(By.XPath("//pre"));
        Thread.Sleep(1000);

        #endregion

        #region writing file


         await writeFileAsync();

        #endregion

        #region  Selenium automation check

        driver.Navigate().GoToUrl("C:/");
        Thread.Sleep(1000);
        IWebElement users = driver.FindElement(By.XPath("//*[@id=\"tbody\"]/tr[24]/td[1]/a"));
        users.Click();
        Thread.Sleep(1000);

        

        IWebElement Z004PUVZ = driver.FindElement(By.XPath("//*[@id=\"tbody\"]/tr[8]/td[1]/a"));
        Z004PUVZ.Click();
        Thread.Sleep(1000);

        IWebElement desktop = driver.FindElement(By.XPath("//*[@id=\"tbody\"]/tr[18]/td[1]/a"));
        desktop.Click();
        Thread.Sleep(1000);



        IWebElement evraklar = driver.FindElement(By.XPath("//*[@id=\"tbody\"]/tr[2]/td[1]/a"));
        evraklar.Click();
        Thread.Sleep(1000);

        IWebElement verilerJson = driver.FindElement(By.XPath("//*[@id=\"tbody\"]/tr/td[1]/a"));
        verilerJson.Click();
        Thread.Sleep(5000);

        //driver.Quit();

        #endregion

        #region DataTable Operation

        //string jsonDatas = textElement.Text;
        //List<Dictionary<string, string>> dictonaryDataList = ParseData(jsonDatas);
        //ConsoleTable table = new ConsoleTable("ID", "Fuel", "Latitude", "Longitude");


        //foreach (Dictionary<string, string> data in dictonaryDataList)
        //{
        //    table.AddRow(data["id"], data["fuel"], data["lat"], data["lon"]);
        //}

        //Console.WriteLine(table);

        #endregion



    }

    private static async Task writeFileAsync()
    {
        try
        {
            string filePath = ConfigurationManager.AppSettings["Path"].ToString();
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                await writer.WriteAsync(textElement.Text);
            }
            Console.WriteLine("Metin dosyaya yazıldı.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Dosya yazma hatası: " + ex.Message);
        }
        
    }
    static List<Dictionary<string, string>> ParseData(string input)
    {
        List<Dictionary<string, string>> dataList = new List<Dictionary<string, string>>();

        input = input.Trim('[', ']');
        string[] pairs = input.Split(new string[] { "},{" }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string pair in pairs)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            string[] fields = pair.Split(new char[] { ',', ':' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < fields.Length; i += 2)
            {
                string key = fields[i].Trim(new char[] { '{', '"' });
                string value = fields[i + 1].Trim(new char[] { '}', '"' });

                data.Add(key, value);
            }

            dataList.Add(data);
        }

        return dataList;
    }

}