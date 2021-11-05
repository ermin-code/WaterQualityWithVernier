﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;
using CsvReader;
using Microsoft.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;

namespace WaterQualityWithVernier
{
    class TExp
    {
        // Temperature Experiment Menu
        public static void TempExpMenu()
        {
            Console.Clear();
            Console.WriteLine("*********************** Temperature Experiment ***********************");
            Console.WriteLine("");
            Console.WriteLine("     Option 1. Enter Temperature Data");
            Console.WriteLine("     Option 2. Search Temperature Data");
            Console.WriteLine("     Option 3. Exit to Main Menu");

            string myoptions;
            myoptions = Console.ReadLine();
            switch (myoptions)
            {
                case "1":
                    TempExpEntry();
                    break;

                case "2":
                    TempExpDataSearch();
                    break;


                case "3":
                    Program.MainMenu();
                    break;
            }
        }

        // Temperature Data Entry Section
        public static void TempExpEntry()
        {
            Console.Clear();
            Console.WriteLine("*********************** Temperature Experiment Data Entry ***********************");
            Console.WriteLine("");
            Console.WriteLine(" In this experiment you will collect temperature data on your pond water using Vernier temperature probe, Texas Instruments Graphing Calculator with attached CBL2 Calculator-Based Laboratory.");
            Console.WriteLine(" Outdoor water temperatures can range from 32 degrees Celsius to above 86 degrees Celsius in the summer. In general, cooler water in pond is generally considered healthier than warmer water.");
            Console.WriteLine(" Pond water temperature plays a vital role in the overal health of the eco-system and the health of pond fish. The perfect pond water should be in the range of 68 to 74 degrees Fahrenheit.");
            Console.WriteLine(" During summer, water loses much of its ability to hold oxygen when the temperature is above 85 degrees. In winter, when the temperature goes below 39 degrees Fahrenheit fish do not have to eat.");
            Console.WriteLine("");
            Console.WriteLine(" This experiment will collect temperature data, store it in a database for later retreaval, and let you know if you should contine or stop feeding your fish based on last 7-day water temperature average.");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(" Please enter the date of the experiment (date/day/year format):");
            Console.WriteLine("");

            string TExpDate = Console.ReadLine();

            Console.Clear();

            Console.WriteLine(" Please enter temperature recorded (in degrees Fahrenheit):");
            Console.WriteLine("");

            string TExpTemp = Console.ReadLine();

            Console.Clear();

            // Stores date and temperature data in CSV file following [date, temperature] format 

            List<string> DateAndTemp = new List<string>();

            DateAndTemp.Add($"{TExpDate},{TExpTemp}");

            File.AppendAllLines(Program._config["TExpData"], DateAndTemp);

            Console.Clear();
            TempExpMenu();
        }

        //Temperature Experiment Data Search Section
        public static void TempExpDataSearch()
        {

            Console.Clear();
            Console.WriteLine("*********************** Temperature Experiment Data Search ***********************");
            Console.WriteLine("");
            Console.WriteLine("What date would you like to search temperature data for? (date/day/year format");
            Console.WriteLine("");

            string TExpCSV = Program._config["TExpData"];
            string TExpDate = Console.ReadLine();
            Console.Clear();
            char csvSeparator = ',';


            // 'For Loop' that searches each entry in TExpData.csv file. If user entry matches with data entry in csv file the console displays: Temperature on <entered date> date was ...

            foreach (string line in File.ReadLines(TExpCSV))
            {
                foreach (string value in line.Replace("\"", "").Split('\r', '\n', csvSeparator))
                {
                    if (value.Trim() == TExpDate.Trim()) // case sensitive
                    {
                        Console.Clear();
                        Console.WriteLine("Temperature on " + value + " was " + line.Split(',')[1] + " degrees Fahrenheit.");
                        Console.WriteLine("");
                        Console.WriteLine("");
                        Console.WriteLine("Press ENTER to go back.");
                        Console.ReadLine();
                        Console.Clear();
                        TempExpMenu();
                    }
                    while (value.Trim() != TExpDate.Trim())
                    {
                        break;
                    }
                }
            }

            // If user entry does not match with data entry in csv file, for loop breaks and the console displays: There is no temperature data for that date!

            Console.Clear();
            Console.WriteLine("There is no temperature data for that date!");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Press ENTER to go back");
            Console.ReadLine();
            Console.Clear();
            TempExpMenu();

        }
    }
}