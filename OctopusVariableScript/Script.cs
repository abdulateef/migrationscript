using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OctopusVariableScript
{
    public static class Script
    {
        public static string LoadFile()
        {
            string file = string.Empty;
            using (StreamReader reader = new StreamReader(File.OpenRead(@"C:\Users\lawal.abdulateef\source\repos\OctopusVariableScript\OctopusVariableScript\Appsettings\swervepay-transaction-variables.json")))
            {
                file = reader.ReadToEnd();
            }
            return file;
        }

        public static Appsetting Deserialize(string file)
        {
            var appsettings = JsonConvert.DeserializeObject<Appsetting>(file);
            if (appsettings.Variables.Count > 0)
            {
                foreach (var variable in appsettings.Variables)
                {
                    if (variable.Name.Contains(":"))
                    {
                        variable.Name = variable.Name.Replace(':', '_');
                    }
                }
            }
            return appsettings;
        }

        public static List<AppServiceVariable> CreateAppServiceVariablesFile(Appsetting appsettings)
        {
            List<AppServiceVariable> appServiceVariables = new List<AppServiceVariable>();
            HashSet<string> varNames = new HashSet<string>();
            foreach (var variable in appsettings.Variables)
            {
                if (!varNames.Contains(variable.Name))
                {
                    AppServiceVariable appServiceVariable = new AppServiceVariable
                    {
                        name = variable.Name,
                        slotSetting = false,
                        value = variable.Value == null ? "" : variable.Value

                    };
                    appServiceVariables.Add(appServiceVariable);
                }

                varNames.Add(variable.Name);

            }
            return appServiceVariables;
        }
        public static string RemoveDuplicateOccurrnce(string appsettings, List<string> duplicateNames)
        {
            var appvariable = JsonConvert.DeserializeObject<List<AppServiceVariable>>(appsettings);
           
            foreach (var duplicateName in duplicateNames)
            {
                var result = appvariable.Where(x => x.name.Trim().ToLower() == duplicateName.Trim().ToLower() );
                if (result.Count() > 1)
                {
                    appvariable.Remove(result.First());

                }
            }
            //appvariable.Remove(appvariable.First(x => x.name == "AppSettings_CorsUrls"));
          return JsonConvert.SerializeObject(appvariable);
        }
        public static List<string> FindDuplicate(string appsettings)
        {
            var appvariable = JsonConvert.DeserializeObject<List<AppServiceVariable>>(appsettings);
            return appvariable.GroupBy(x => x.name.Trim().ToLower())
                                         .Where(g => g.Count() > 1)
                                         .Select(x => x.Key).ToList();

        }



        public static string ConvertToString(List<AppServiceVariable> appServiceVariables)
        {
            return JsonConvert.SerializeObject(appServiceVariables);
        }

        public static string GetNames(List<AppServiceVariable> appServiceVariables)
        {
            string names = string.Empty;
            foreach (var appServiceVariable in appServiceVariables)
            {

                names = names + " \n" + appServiceVariable.name;
            }

            return names;
        }
        public static List<AppServiceVariable> LoadFileSecondVariable()
        {
            string file = string.Empty;
            using (StreamReader reader = new StreamReader(File.OpenRead(@"C:\Users\lawal.abdulateef\source\repos\OctopusVariableScript\OctopusVariableScript\Appsettings\azureAppServerTemplate.json")))
            {
                file = reader.ReadToEnd();
            }

            var appsettings = JsonConvert.DeserializeObject<List<AppServiceVariable>>(file);

            return appsettings;
        }
        public static string Compare(string item1)
        {
            var variable1 = LoadFileSecondVariable();
            var variable2 = JsonConvert.DeserializeObject<List<AppServiceVariable>>(item1);
            foreach (var appServiceVariable in variable1)
            {
                if (!variable2.Contains(appServiceVariable))
                {
                    variable2.Add(appServiceVariable);
                }
            }

            return JsonConvert.SerializeObject(variable2);
            ;
        }
    }
}
