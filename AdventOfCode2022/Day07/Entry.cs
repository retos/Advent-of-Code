namespace AdventOfCode2022.Day07
{
    public class Entry
    {
        internal long? totalSize = null;
        public Entry(List<Entry> allEntries)
        {
            AllEntries = allEntries;
            allEntries.Add(this);
            Children = new List<Entry> ();
        }

        public bool IsDirectory { get; internal set; }
        public long Size { get; internal set; }
        public string Path { get; internal set; }
        public List<Entry> Children { get; set; }
        public List<Entry> AllEntries { get; internal set; }

        public long TotalSize 
        {
            get 
            {
                if (totalSize == null) 
                {
                    if (!IsDirectory)
                    {
                        totalSize = Size;
                        return (long)totalSize;
                    }
                    totalSize = Children.Sum(c => c.TotalSize);
                    return (long)totalSize;
                }
                else
                {
                    return (long)totalSize;
                }

            }
        }

        internal Entry GetParent()
        {
            return AllEntries.Where(e => e.Children.Contains(this)).Single();
        }
    }
}