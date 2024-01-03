using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using static System.Windows.Forms.Design.AxImporter;

namespace TestNuget
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public class BrowserInstance
        {
            public string url;
            public ChromeOptions options = new ChromeOptions();
            public ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();

            public string userAgent = "Mega Phone";
            /*
            public string proxyAddress = "208.167.233.70";
            public string proxyPort = "1893";
            public string proxyUser = "proxy1user";
            public string proxyPass = "123456789";
            */
            // Địa chỉ và cổng của proxy

            public IWebDriver driver;

            public void init(string url, string proxyIP, string proxyPort, string proxyUser, string proxyPass)
            {
                this.options.AddArgument($"user-agent={userAgent}");
                Proxy proxy = new Proxy
                {
                    Kind = ProxyKind.Manual,
                    SocksUserName = proxyUser,
                    SocksPassword = proxyPass,
                    HttpProxy = $"{proxyIP}:{proxyPort}",
                    SslProxy = $"{proxyIP}:{proxyPort}",
                    FtpProxy = $"{proxyIP}:{proxyPort}",
                };
                this.options.Proxy = proxy;
                if(proxyUser != "")
                this.options.AddExtension("C:\\WEB APP PROJECT\\AppNuget\\TestNuget\\TestNuget\\bin\\Debug\\net6.0-windows\\Proxy-Auto-Auth.crx");
                chromeDriverService.HideCommandPromptWindow = true;
                options.AddExcludedArguments("enable-automation");
                this.driver = new ChromeDriver(chromeDriverService = this.chromeDriverService, options = this.options);



                driver.Navigate().GoToUrl("chrome-extension://ggmdpepbjljkkkdaklfihhngmmgmpggp/options.html");
                Thread.Sleep(1000);
                driver.FindElement(By.Id("login")).SendKeys(proxyUser);
                driver.FindElement(By.Id("password")).SendKeys(proxyPass);
                driver.FindElement(By.Id("retry")).Clear();
                driver.FindElement(By.Id("retry")).SendKeys("2");
                driver.FindElement(By.Id("save")).Click();


                var tabs = driver.WindowHandles;
                if (tabs.Count > 1)
                {
                    driver.SwitchTo().Window(tabs[0]);
                    driver.Close();
                    driver.SwitchTo().Window(tabs[1]);
                }
                driver.Navigate().GoToUrl(url);



            }
            public void runScript(string url)
            {
                
                //driver.Navigate().GoToUrl("https://www.youtube.com/shorts/iJjSTFesgOw");
            }


        }

        ChromeOptions options = new ChromeOptions();
        ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();


        private void button1_Click(object sender, EventArgs e)
        {
            chromeDriverService.HideCommandPromptWindow = true;
            options.AddExcludedArguments("enable-automation");
            string userAgent = "Mega Phone";
            options.AddArgument($"user-agent={userAgent}");
            // Địa chỉ và cổng của proxy
            string proxyAddress = "208.167.233.70";
            string proxyPort = "1893";
            string proxyUser = "proxy1user";
            string proxyPass = "123456789";
            // Tạo đối tượng Proxy
            var proxy = new Proxy
            {
                Kind = ProxyKind.Manual,
                SocksUserName = proxyUser,
                SocksPassword = proxyPass,
                HttpProxy = $"{proxyAddress}:{proxyPort}",
                SslProxy = $"{proxyAddress}:{proxyPort}",
                FtpProxy = $"{proxyAddress}:{proxyPort}",
            };



            options.Proxy = proxy;
            options.AddExtension("C:\\WEB APP PROJECT\\AppNuget\\TestNuget\\TestNuget\\bin\\Debug\\net6.0-windows\\Proxy-Auto-Auth.crx");

            IWebDriver driver = new ChromeDriver(chromeDriverService = chromeDriverService, options = options);
            driver.Navigate().GoToUrl("chrome-extension://ggmdpepbjljkkkdaklfihhngmmgmpggp/options.html");
            Thread.Sleep(1000);
            driver.FindElement(By.Id("login")).SendKeys(proxyUser);
            driver.FindElement(By.Id("password")).SendKeys(proxyPass);
            driver.FindElement(By.Id("retry")).Clear();
            driver.FindElement(By.Id("retry")).SendKeys("2");
            driver.FindElement(By.Id("save")).Click();

            driver.Navigate().GoToUrl("https://youtube.com");

            var tabs = driver.WindowHandles;
            if (tabs.Count > 1)
            {
                driver.SwitchTo().Window(tabs[0]);
                driver.Close();
                driver.SwitchTo().Window(tabs[1]);
            }
            driver.Navigate().GoToUrl("https://www.youtube.com/hungnguyenpage");
            driver.Navigate().GoToUrl("https://www.youtube.com/shorts/iJjSTFesgOw");

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FirefoxDriver firefoxDriver = new FirefoxDriver();
            firefoxDriver.Url = "https://www.whatismyip.com/";
            firefoxDriver.Navigate();
        }

        public async Task youtubeThread(string url, string proxyIP, string proxyPort, string proxyUser, string proxyPass)
        {
            await Task.Run(() =>
            {
                BrowserInstance br1 = new BrowserInstance();
                br1.init(url: url, proxyIP: proxyIP, proxyPort: proxyPort, proxyUser: proxyUser, proxyPass: proxyPass);
               
            });
            

        }
        private void button4_Click(object sender, EventArgs e)
        {
            for(int i=0;i<3; i++)
            {
                //Thread.Sleep(2000);
                youtubeThread(url:"https://www.youtube.com/watch?v=fY0qwvBFST4&ab_channel=KidzInvader", proxyIP: "208.167.233.70", proxyPort: "1893", proxyUser: "proxy1user", proxyPass: "123456789");
                //youtubeThread(url: "https://www.youtube.com/watch?v=fY0qwvBFST4&ab_channel=KidzInvader", proxyIP: "49.228.131.169", proxyPort: "5000", proxyUser: "", proxyPass: "");
            }
            
        }
    }
}