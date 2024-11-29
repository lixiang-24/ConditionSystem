using System.Text;

namespace ConditionSystem
{
    public static class FileGenerator
    {
        private static string conditionTemplateFilePath = "Core/ConditionTemplate.txt";
        private static string factoryTemplateFilePath = "Core/FactoryTemplate.txt";
        private static string factoryFilePath = "Core/ConditionFactory.cs";
        private static string outputConditionFilePath = "Conditions";

        public static void GenerateFiles()
        {
            var crtPath = Directory.GetCurrentDirectory();
            string crtDirPath = GetParentDirectory(crtPath, "ConditionSystem");
            GenerateConditionFiles(crtDirPath);
            GenerateConditionFactoryFiles(crtDirPath);
        }

        private static void GenerateConditionFiles(string crtDirPath)
        {
            string conditionTemplatePath = Path.Combine(crtDirPath, conditionTemplateFilePath);
            string template = File.ReadAllText(conditionTemplatePath);
            string outFilePath = Path.Combine(crtDirPath, outputConditionFilePath);
            foreach (var condition in Enum.GetValues(typeof(ConditionType)))
            {
                string conditionName = condition.ToString();
                string fileName = conditionName + "Condition.cs";
                string filePath = Path.Combine(outFilePath, fileName);
                // 检查文件是否存在
                if (File.Exists(filePath))
                    continue;
                // 使用FileStream创建文件
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(fileStream))
                    {
                        // 替换模板中的占位符并写入文件
                        string fileContent = template.Replace("{{CONDITION_NAME}}", conditionName + "Condition");
                        writer.Write(fileContent);
                        Console.WriteLine($"Generate {conditionName}Condition.cs successful!");
                    }
                }
            }
        }

        public static void GenerateConditionFactoryFiles(string crtDirPath)
        {
            string templateFilePath = Path.Combine(crtDirPath, factoryTemplateFilePath);
            string template = File.ReadAllText(templateFilePath);
            StringBuilder switchCases = new StringBuilder();
            foreach (var condition in Enum.GetValues(typeof(ConditionType)))
            {
                string conditionName = condition.ToString();
                // 添加switch case语句
                switchCases.AppendLine($"\t\t\t\tcase ConditionType.{conditionName}:");
                switchCases.AppendLine($"\t\t\t\t\treturn new {conditionName}Condition(id);");
            }
            switchCases.AppendLine($"\t\t\t\tdefault:");
            switchCases.Append($"\t\t\t\t\treturn null;");
            // 将模板中的占位符替换为生成的switch case语句
            string result = template.Replace("//[SWITCH_CASES]", switchCases.ToString());
            // 将结果写入文件
            string factoryPath = Path.Combine(crtDirPath, factoryFilePath);
            File.WriteAllText(factoryPath, result);
        }

        private static string GetParentDirectory(string path, string targetFolderName)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            while (directory != null && !directory.Name.Equals(targetFolderName, StringComparison.OrdinalIgnoreCase))
            {
                directory = directory.Parent;
            }

            return directory?.FullName;
        }
    }
}