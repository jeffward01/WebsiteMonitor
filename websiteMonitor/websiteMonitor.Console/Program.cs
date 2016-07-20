﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Xml;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.PhantomJS;
using websiteMonitor.Console.Services;
using HtmlDocument = System.Windows.Forms.HtmlDocument;
using Timer = System.Timers.Timer;

namespace websiteMonitor.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            
            // Create a timer
            var myTimer = new System.Timers.Timer();
            // Tell the timer what to do when it elapses
            myTimer.Elapsed += new ElapsedEventHandler(ringMonitor);
            // Set it to go off every 60 seconds
            myTimer.Interval = 60000;
            // And start it        
            myTimer.Enabled = true;
           
            

            

            System.Console.ReadLine();


        }









        // Implement a call with the right signature for events going off
        private static void ringMonitor(object source, ElapsedEventArgs e)
        {
            var service = new websiteService();
            IWebDriver driver = new PhantomJSDriver();
            driver.Navigate().GoToUrl("https://betterthandiamond.com/amora-gem-search.php");
            //driver.Navigate().GoToUrl("http://weddingmonitor.jeffwarddevelopment.com/");
            try
            {
                var exists = driver.FindElement(By.ClassName("notificationlink"));
                var exists2 = driver.FindElement(By.ClassName("notFound"));
                {
                    System.Console.Clear();
                    var key = "(We are sold out! Please click here to be notified when we restock...)";
                    var key2 = "NO RESULTS";
                    if (exists.Text == key && exists2.Text == key2)
                    {
                        System.Console.Clear();

                        service.sendSMS();

                        System.Console.WriteLine("Matching... Ring out of stock");
                        

                    }
                    else
                    {
                        System.Console.Clear();

                        System.Console.WriteLine("NOT FOUND! Ring in stock!");
                        service.sendSMS();
                        Environment.Exit(0);
                    }
                    driver.Close();
                    System.Console.ReadLine();
                }
            }
            catch (Exception)
            {

                System.Console.WriteLine("Not found");
                driver.Close();

                System.Console.ReadLine();
            }



            driver.Close();

            System.Console.ReadLine();
        }
    }
}