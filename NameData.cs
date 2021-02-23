namespace JSON_Minimum_Test_Harness
{
    public struct NameData
    {
        public string name;
        public int[] chain;
        public string[] genders;
        public int[] groups;

        public NameData(string nameRef, int[] chainRef, string[] gendersRef, int[] groupsRef)
        {
            this.name = nameRef;
            this.chain = chainRef;
            this.genders = gendersRef;
            this.groups = groupsRef;
        }
    }
}