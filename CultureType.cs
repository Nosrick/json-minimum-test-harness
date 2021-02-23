using System;
using System.Collections.Generic;
using System.Linq;

namespace JSON_Minimum_Test_Harness
{
    public class CultureType : ICulture
    {
        protected List<string> m_RulerTypes;
        protected List<string> m_Crimes;
        protected List<NameData> m_NameData;
        protected IDictionary<string, int> m_SexPrevalence;
        protected IDictionary<string, int> m_SexualityPrevalence;
        protected IDictionary<string, int> m_RomancePrevalence;
        protected IDictionary<string, int> m_GenderPrevalence;

        //The first number is the chance, the second is the actual number it can vary by
        protected IDictionary<string, Tuple<int, int>> m_StatVariance;
        protected List<string> m_RelationshipTypes;
        protected IDictionary<string, int> m_JobPrevalence;
        List<string> m_Inhabitants;

        public int LastGroup { get; protected set; }

        public string Tileset { get; protected set; }

        public string[] Inhabitants => this.m_Inhabitants.ToArray();

        public string CultureName { get; protected set; }

        public string[] RulerTypes => this.m_RulerTypes.ToArray();

        public string[] Crimes => this.m_Crimes.ToArray();

        public string[] RelationshipTypes => this.m_RelationshipTypes.ToArray();

        public string[] RomanceTypes => this.m_RomancePrevalence.Keys.ToArray();

        public string[] Sexes => this.m_SexPrevalence.Keys.ToArray();

        public string[] Sexualities => this.m_SexualityPrevalence.Keys.ToArray();

        public string[] Genders => this.m_GenderPrevalence.Keys.ToArray();

        public string[] Jobs => this.m_JobPrevalence.Keys.ToArray();

        public int NonConformingGenderChance { get; protected set; }

        public NameData[] NameData => this.m_NameData.ToArray();

        public IDictionary<string, IDictionary<string, string>> CursorColours { get; protected set; }
        public IDictionary<string, IDictionary<string, string>> BackgroundColours { get; protected set; }
        public IDictionary<string, string> FontColours { get; protected set; }

        protected const int NO_GROUP = int.MinValue;

        public CultureType()
        { }

        public CultureType(
            string nameRef,
            string tileset,
            IEnumerable<string> rulersRef,
            IEnumerable<string> crimesRef,
            IEnumerable<NameData> namesRef,
            IDictionary<string, int> jobRef,
            IEnumerable<string> inhabitantsNameRef,
            IDictionary<string, int> sexualityPrevalenceRef,
            IDictionary<string, int> sexPrevalence,
            IDictionary<string, Tuple<int, int>> statVariance,
            IEnumerable<string> relationshipTypes,
            IDictionary<string, int> romancePrevalence,
            IDictionary<string, int> genderPrevalence,
            int nonConformingGenderChance,
            IDictionary<string, IDictionary<string, string>> background,
            IDictionary<string, IDictionary<string, string>> cursor,
            IDictionary<string, string> fontColours)
        {
            this.Tileset = tileset;
            this.CultureName = nameRef;
            this.m_RulerTypes = rulersRef.ToList();
            this.m_Crimes = crimesRef.ToList();
            this.m_NameData = namesRef.ToList();
            this.m_Inhabitants = inhabitantsNameRef.ToList();
            this.m_SexPrevalence = sexPrevalence;
            this.m_StatVariance = statVariance;
            this.m_JobPrevalence = jobRef;
            this.m_SexualityPrevalence = sexualityPrevalenceRef;
            this.m_StatVariance = statVariance;
            this.m_RelationshipTypes = relationshipTypes.ToList();
            this.m_RomancePrevalence = romancePrevalence;
            this.m_GenderPrevalence = genderPrevalence;
            this.NonConformingGenderChance = nonConformingGenderChance;
            
            this.CursorColours = cursor;
            this.BackgroundColours = background;
            this.FontColours = fontColours;
        }
    }
}