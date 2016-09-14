﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflexion
{
    class Program
    {
        static void Main(string[] args)
        {
            var zero = "0";

            Type type = zero.GetType();
            Assembly assembly = type.Assembly;

            Console.WriteLine(type);
            Console.WriteLine(type.Assembly);

            foreach (var ty in assembly.GetTypes()
                .Where(ty => ty.FullName.Split('.').Count() == 2 &&
                    ty.Name.StartsWith("Int")))
            {
                Console.WriteLine(ty);
            }

            var int32Type = assembly.GetType("System.Int32");

            var createdInt  = Activator.CreateInstance(int32Type);
            Console.WriteLine(createdInt);


            var phone = new Smartphone();
            phone.IsLocked = true;
            phone.Carrier = "Entel";

            var t = phone.GetType();

            var carrierProperty = t.GetProperty("Carrier");


            foreach (var att in carrierProperty.GetCustomAttributes())
            {
                Console.WriteLine(att);
            }

            var propertiesWithDisplayName = from prop in t.GetProperties()
                                            where prop.GetCustomAttributes<DisplayAttribute>().Any()
                                            select prop;

            foreach (var property in propertiesWithDisplayName)
            {
                var attr = property.GetCustomAttribute<DisplayAttribute>();
                Console.WriteLine(attr.Name + ": " + property.GetValue(phone));
            }

            Console.Read();
        }
    }
}
