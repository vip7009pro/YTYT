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
            public ChromeOptions options = new ChromeOptions();
            public ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();

            string userAgent = "Mega Phone";

            // Địa chỉ và cổng của proxy
            string proxyAddress = "208.167.233.70";
            string proxyPort = "1893";
            string proxyUser = "proxy1user";
            string proxyPass = "123456789";
            public Proxy proxy;

            public IWebDriver driver;

            public void init()
            {
                this.options.AddArgument($"user-agent={userAgent}");
                this.proxy = new Proxy
                {
                    Kind = ProxyKind.Manual,
                    SocksUserName = proxyUser,
                    SocksPassword = proxyPass,
                    HttpProxy = $"{proxyAddress}:{proxyPort}",
                    SslProxy = $"{proxyAddress}:{proxyPort}",
                    FtpProxy = $"{proxyAddress}:{proxyPort}",
                };
                this.options.Proxy = this.proxy;
                this.options.AddExtension("C:\\WEB APP PROJECT\\AppNuget\\TestNuget\\TestNuget\\bin\\Debug\\net6.0-windows\\Proxy-Auto-Auth.crx");
                chromeDriverService.HideCommandPromptWindow = true;
                options.AddExcludedArguments("enable-automation");

                this.driver = new ChromeDriver(chromeDriverService = this.chromeDriverService, options = this.options);

            }
            public void runScript()
            {
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

        public async Task youtubeThread()
        {
            await Task.Run(() =>
            {
                BrowserInstance br1 = new BrowserInstance();
                br1.init();
                br1.runScript();
            });           

        }
        private void button4_Click(object sender, EventArgs e)
        {
            for(int i=0;i<3; i++)
            {
                Thread.Sleep(2000);
                youtubeThread();
            }
            
        }
    }
}