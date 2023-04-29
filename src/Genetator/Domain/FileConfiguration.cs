namespace Generator.Domain
{
    public class FileConfiguration
    {
        private const string PREFIX_TYPE = "txt";

        public FileConfiguration(string fileName, string language, string path, uint startChapter, uint endChapter, string? title = null, string? chapter = null)
        {
            FileName = fileName;
            Path = path;
            Language = language;
            Type = PREFIX_TYPE;
            Title = title;
            Chapter = chapter;
            StartChapter = startChapter;
            EndChapter = endChapter;
        }

        public string Path { get; set; }
        public string FileName { get; set; }
        public string Language { get; set; }
        public string Type { get; set; }
        public string? Title { get; set; }
        public string? Chapter { get; set; }
        public uint StartChapter { get; set; }
        public uint EndChapter { get; set; }
    }
}