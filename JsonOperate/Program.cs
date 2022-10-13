using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

var path = @"C:\Higgsworkspace\products";
//path = @"D:\hs-workspace\0816\products";
var key = "Kestrel";
JObject limits = new JObject();
JObject configValue = new JObject();
configValue.Add("MaxConcurrentConnections", "${MaxConcurrentConnections}");
configValue.Add("MaxConcurrentUpgradedConnections", "${MaxConcurrentUpgradedConnections}");
//TODO 未来可以考虑將內容放到設定文件
//TODO 對已經存在的key，額外添加新的屬性要再設定

var filePaths = new List<string>();
DirectoryInfo dir = new DirectoryInfo(path);

foreach (DirectoryInfo dChild in dir.GetDirectories("*"))
{
    var fileName = dChild.FullName;
    if (fileName.Contains("Api") && !fileName.Contains("Test") && !fileName.Contains("Client") && !fileName.Contains("Model"))
    {
        filePaths.Add(dChild.FullName);
    }
}

foreach (var filePath in filePaths)
{
    var files = Directory.GetFiles(filePath + "/", "appsettings.json");
    if (files.Length == 0)
    {
        continue;
    }
    JObject jObj;
    using (StreamReader file = File.OpenText(files[0]))
    {
        using (JsonTextReader jtReader = new JsonTextReader(file))
        {
            jObj = (JObject)JToken.ReadFrom(jtReader);
            if (jObj.ContainsKey(key))
            {
                if (!jObj.SelectToken(key)!.ToString().Contains("Limits"))
                {
                    var Jproperty = jObj.SelectToken(key);
                    Jproperty.Parent.Add(new JProperty(key, limits));
                }
            }
            else
            {
                jObj.TryAdd(key, limits);
            }
        }
    }
    File.WriteAllText(files[0], JsonConvert.SerializeObject(jObj, Formatting.Indented));

}