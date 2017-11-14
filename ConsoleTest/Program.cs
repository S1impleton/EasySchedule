﻿using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseAccess;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {

            //Mail mail = new Mail();
            //mail.SendSimpleMessage();
            //Console.WriteLine("Mail er sendt-");
            //Console.ReadLine();

            Service service = new Service();
            Console.WriteLine(service.GetEmployeeByUsername("TobMaster").Name);

            Console.ReadLine();
        }

        static void GetConnectionString()
        {
            Console.WriteLine("Local: "+ new DbConnectionADO().LocalConnectionString());
            Console.WriteLine("Kraka: " + new DbConnectionADO().KrakaConnectionString());
        }
    }
}