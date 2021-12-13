using System;

namespace OctopusVariableScript
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = Script.LoadFile();
            var appsettings = Script.Deserialize(file);
            var appServiceVariablesFile = Script.CreateAppServiceVariablesFile(appsettings);
            var appVariable = Script.ConvertToString(appServiceVariablesFile);
            var names = Script.GetNames(appServiceVariablesFile);
            var result = Script.Compare(appVariable);
            var duplicates = Script.FindDuplicate(result);
            var response = Script.RemoveDuplicateOccurrnce(result, duplicates);
            var duplicates2 = Script.FindDuplicate(response);

            Console.WriteLine(response);

        }
    }
}
