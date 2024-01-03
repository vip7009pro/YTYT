using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using System.Diagnostics;
using System.Drawing;
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
            public IWebDriver driver;

            public void init(string url, string proxyIP, string proxyPort, string proxyUser, string proxyPass)
            {
                this.options.AddArgument(@"user-agent={userAgent}");

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

                if (proxyUser != "")
                    this.options.AddExtension("Proxy-Auto-Auth.crx");
                chromeDriverService.HideCommandPromptWindow = true;
                this.options.AddExcludedArguments("enable-automation");
                options.AddArgument("user-data-dir=C:\\WEB APP PROJECT\\AppNuget\\TestNuget\\TestNuget\\bin\\Debug\\net6.0-windows\\profile\\user1");
                options.AddArgument("--disable-blink-features=AutomationControlled");
                this.driver = new ChromeDriver(chromeDriverService = this.chromeDriverService, options = this.options);

                driver.Navigate().GoToUrl("chrome-extension://ggmdpepbjljkkkdaklfihhngmmgmpggp/options.html");
                //Thread.Sleep(1000);

                driver.FindElement(By.Id("login")).SendKeys("");
                driver.FindElement(By.Id("password")).SendKeys("");
                driver.FindElement(By.Id("retry")).Clear();
                driver.FindElement(By.Id("retry")).SendKeys("2");

                driver.FindElement(By.Id("login")).Clear();
                driver.FindElement(By.Id("password")).Clear();
                driver.FindElement(By.Id("retry")).Clear();

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
                //driver.FindElement(By.TagName("video")).SendKeys(OpenQA.Selenium.Keys.Space);
            }
        }

        public class FFBrowserInstance
        {
            public string url;
            public string userAgent = "Mega Phone";

            public FirefoxDriver driver;

            public void init(string url, string proxyIP, string proxyPort, string proxyUser, string proxyPass)
            {

                FirefoxOptions options = new FirefoxOptions();
                // options.Profile = new FirefoxProfile(@"C:\Users\vip70\AppData\Local\Temp\rust_mozprofilewS7ssc");
                //var firefoxProfile = new FirefoxProfile(@"C:\Users\vip70\AppData\Local\Temp\rust_mozprofilewS7ssc\");
                //firefoxProfile.AddExtension("autoauth.xpi");
                //options.Profile = firefoxProfile;
                options.AddArguments("-profile", @"C:\Users\vip70\AppData\Local\Temp\rust_mozprofilewS7ssc\");



                Proxy proxy = new Proxy
                {
                    Kind = ProxyKind.Manual,
                    SocksUserName = proxyUser,
                    SocksPassword = proxyPass,
                    HttpProxy = $"{proxyIP}:{proxyPort}",
                    SslProxy = $"{proxyIP}:{proxyPort}",
                };


                //string proxyWithAuthentication = "proxy1user:123456789@208.167.233.70:1893";

                //options.AddArgument($"--proxy-server=http://{proxyWithAuthentication}");



                options.Proxy = proxy;
                this.driver = new FirefoxDriver(options);
                this.driver.SwitchTo().Alert().SendKeys(proxyUser + OpenQA.Selenium.Keys.Tab + proxyPass);

                this.driver.Url = url;
                this.driver.Navigate();
            }
            public void runScript(string url)
            {


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
            driver.Navigate().GoToUrl("https://www.youtube.com/@KidzInvader");
            driver.Navigate().GoToUrl("https://www.youtube.com/watch?v=fY0qwvBFST4&ab_channel=KidzInvader");

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //FirefoxDriver firefoxDriver = new FirefoxDriver();
            //firefoxDriver.Url = "https://www.whatismyip.com/";
            //firefoxDriver.Navigate();
            FFBrowserInstance fi = new FFBrowserInstance();
            fi.init(url: "https://proxy1user:123456789@whatismyipaddress.com/", proxyIP: "208.167.233.70", proxyPort: "1893", proxyUser: "proxy1user", proxyPass: "123456789");
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
            for (int i = 0; i < 1; i++)
            {

                youtubeThread(url: "https://www.youtube.com/watch?v=fY0qwvBFST4&ab_channel=KidzInvader", proxyIP: "208.167.233.70", proxyPort: "1893", proxyUser: "proxy1user", proxyPass: "123456789");

            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
            process.StartInfo.Arguments = " --user-data-dir=C:\\WEB APP PROJECT\\AppNuget\\TestNuget\\TestNuget\\bin\\Debug\\net6.0-windows\\profile\\user1 --remote-debugging-port=8989";
            process.Start();
        }
    }
}