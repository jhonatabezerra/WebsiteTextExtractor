namespace Generator.Domain
{
    public class FileConfiguration
    {
        private const string PREFIX_TYPE = "txt";

        public FileConfiguration(string fileName, string language, string path, uint startChapter, uint endChapter)
        {
            FileName = fileName;
            Path = path;
            Language = language;
            StartChapter = startChapter;
            EndChapter = endChapter;
            Type = PREFIX_TYPE;
        }

        public string Path { get; set; }
        public string FileName { get; set; }
        public string Chapter { get; set; }
        public string Language { get; set; }
        public string Type { get; set; }
        public uint StartChapter { get; set; }
        public uint EndChapter { get; set; }
    }
}