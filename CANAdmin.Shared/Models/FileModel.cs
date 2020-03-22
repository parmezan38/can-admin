namespace CANAdmin.Shared.Models
{
    public struct FileModel
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public FileModel(string fileName, string location)
        {
            Name = fileName.Substring(0, fileName.Length - 4);
            Location = location;
        }

    }
}