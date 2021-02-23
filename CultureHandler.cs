using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JSON_Minimum_Test_Harness
{
    public class CultureHandler
    {
        public IEnumerable<ICulture> Load()
        {
            string folderPath = Directory.GetCurrentDirectory() + "/Cultures";
            string[] files = Directory.GetFiles(folderPath, "*.json");

            List<ICulture> cultures = new List<ICulture>();

            foreach (string file in files)
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    using (JsonTextReader jsonReader = new JsonTextReader(reader))
                    {
                        try
                        {
                            JObject jToken = JObject.Load(jsonReader);

                            foreach (JToken child in jToken["Cultures"])
                            {
                                string cultureName = (string) child["CultureName"];
                                int nonConformingGenderChance = (int) child["NonConformingGenderChance"];
                                IEnumerable<string> rulers = child["Rulers"].Select(token => (string) token);
                                IEnumerable<string> crimes = child["Crimes"].Select(token => (string) token);
                                IEnumerable<string> inhabitants =
                                    child["Inhabitants"].Select(token => (string) token);
                                IEnumerable<string> relationships =
                                    child["Relationships"].Select(token => (string) token);

                                JToken dataArray = child["Names"];
                                List<NameData> nameData = new List<NameData>();
                                foreach (var data in dataArray)
                                {
                                    string name = (string) data["Name"];
                                    int[] chain = data["Chain"]?.Select(token => (int) token).ToArray();
                                    if (chain is null)
                                    {
                                        chain = new[] {0};
                                    }

                                    string[] genderNames = data["Gender"]?.Select(token => (string) token).ToArray();
                                    if (genderNames is null)
                                    {
                                        genderNames = new[] {"all"};
                                    }

                                    int[] groups = data["Group"]?.Select(token => (int) token).ToArray();
                                    if (groups is null)
                                    {
                                        groups = new int[0];
                                    }
                                    nameData.Add(new NameData(
                                        name,
                                        chain,
                                        genderNames,
                                        groups));
                                }

                                dataArray = child["Sexualities"];
                                IDictionary<string, int> sexualities = dataArray.Select(token =>
                                        new KeyValuePair<string, int>(
                                            (string) token["Name"],
                                            (int) token["Chance"]))
                                    .ToDictionary(x => x.Key, x => x.Value);

                                dataArray = child["Romances"];
                                IDictionary<string, int> romances = dataArray.Select(token =>
                                        new KeyValuePair<string, int>(
                                            (string) token["Name"],
                                            (int) token["Chance"]))
                                    .ToDictionary(x => x.Key, x => x.Value);

                                dataArray = child["Genders"];
                                IDictionary<string, int> genders = dataArray.Select(token =>
                                        new KeyValuePair<string, int>(
                                            (string) token["Name"],
                                            (int) token["Chance"]))
                                    .ToDictionary(x => x.Key, x => x.Value);

                                dataArray = child["Sexes"];
                                IDictionary<string, int> sexes = dataArray.Select(token =>
                                        new KeyValuePair<string, int>(
                                            (string) token["Name"],
                                            (int) token["Chance"]))
                                    .ToDictionary(x => x.Key, x => x.Value);

                                dataArray = child["Statistics"];
                                IDictionary<string, Tuple<int, int>> statistics = dataArray.Select(token =>
                                        new KeyValuePair<string, Tuple<int, int>>(
                                            (string) token["Name"],
                                            new Tuple<int, int>(
                                                (int) token["Chance"],
                                                (int) token["Magnitude"])))
                                    .ToDictionary(x => x.Key, x => x.Value);

                                dataArray = child["Jobs"];
                                IDictionary<string, int> jobPrevalence = dataArray.Select(token =>
                                        new KeyValuePair<string, int>(
                                            (string) token["Name"],
                                            (int) token["Chance"]))
                                    .ToDictionary(x => x.Key, x => x.Value);

                                dataArray = child["TileSet"];
                                string tileSetName = (string) dataArray["Name"];

                                dataArray = child["UIColours"];

                                IDictionary<string, IDictionary<string, string>> cursorColours =
                                    new Dictionary<string, IDictionary<string, string>>();
                                try
                                {
                                    cursorColours = this.ExtractColourData(dataArray, "CursorColours");
                                }
                                catch (Exception e)
                                {
                                }

                                IDictionary<string, IDictionary<string, string>> backgroundColours =
                                    new Dictionary<string, IDictionary<string, string>>();
                                try
                                {
                                    backgroundColours = this.ExtractColourData(dataArray, "BackgroundColours");
                                }
                                catch (Exception e)
                                {
                                }

                                IDictionary<string, string> mainFontColours = new Dictionary<string, string>();
                                try
                                {
                                    var fontColours = dataArray["FontColours"];
                                    foreach (var colour in fontColours)
                                    {
                                        mainFontColours.Add(
                                            (string) colour["Name"],
                                            (string) colour["Value"]);
                                    }
                                }
                                catch (Exception e)
                                {
                                }
                                
                                cultures.Add(
                                    new CultureType(
                                        cultureName,
                                        tileSetName,
                                        rulers,
                                        crimes,
                                        nameData,
                                        jobPrevalence,
                                        inhabitants,
                                        sexualities,
                                        sexes,
                                        statistics,
                                        relationships,
                                        romances,
                                        genders,
                                        nonConformingGenderChance,
                                        backgroundColours,
                                        cursorColours,
                                        mainFontColours));
                            }
                        }
                        catch (Exception e)
                        {
                        }
                        finally
                        {
                            jsonReader.Close();
                            reader.Close();
                        }
                    }
                }
            }

            return cultures;
        }
        
        protected IDictionary<string, IDictionary<string, string>> ExtractColourData(
            JToken element,
            string elementName)
        {
            IDictionary<string, IDictionary<string, string>> colours =
                new Dictionary<string, IDictionary<string, string>>();
            foreach(var colour in element[elementName])
            {
                string name = (string) colour["Name"];
                foreach (var data in colour["Colour"])
                {
                    string partName = (string) data["Name"];
                    string c = (string) data["Value"];

                    if (colours.ContainsKey(name))
                    {
                        colours[name].Add(partName, c);
                    }
                    else
                    {
                        colours.Add(name, new Dictionary<string, string>
                        {
                            {partName, c}
                        });
                    }
                }
            }
            return colours;
        }
    }
}