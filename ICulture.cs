using System.Collections.Generic;

namespace JSON_Minimum_Test_Harness
{
    public interface ICulture
    {
        string Tileset { get; }
        
        int LastGroup { get; }
        
        string[] Inhabitants { get; }
        
        string CultureName { get; }
        
        string[] RulerTypes { get; }
        
        string[] Crimes { get; }
        
        string[] RelationshipTypes { get; }
        
        string[] RomanceTypes { get; }
        
        string[] Sexes { get; }
        
        string[] Sexualities { get; }
        
        string[] Genders { get; }
        
        string[] Jobs { get; }
        
        int NonConformingGenderChance { get; }
        
        NameData[] NameData { get; }
        
        IDictionary<string, IDictionary<string, string>> CursorColours { get; }
        IDictionary<string, IDictionary<string, string>> BackgroundColours { get; }
        
        IDictionary<string, string> FontColours { get; }
    }
}