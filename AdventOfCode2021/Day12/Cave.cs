namespace AdventOfCode2021.Day12
{
    internal class Cave
    {
        private List<Cave> AllCaves;

        public string Name { get; set; }
        public List<Cave> ConnectedTo { get; set; }

        public Cave(ref List<Cave> caves, string name, string destination)
        {
            AllCaves = caves;
            ConnectedTo = new List<Cave>();
            Name = name;
            //Add destination
            this.AddDestination(destination, ref caves);            
        }

        public Cave(string name, ref List<Cave> caves)
        {
            AllCaves = caves;
            this.Name = name;
            ConnectedTo = new List<Cave>();
        }

        internal void AddDestination(Cave destination)
        {
            if (!ConnectedTo.Contains(destination))
            {
                ConnectedTo.Add(destination);
                //add reverse connection to target
                destination.AddDestination(this);
            }
        }

        internal void AddDestination(string name, ref List<Cave> caves)
        {
            if (!AllCaves.Any(c => c.Name == name))
            {
                AllCaves.Add(new Cave(name, ref caves));
            }
            Cave destination = AllCaves.Where(c => c.Name == name).Single();
            AddDestination(destination);
        }

        internal long Walk(string path, ref List<string> pathList, bool part2)
        {
            //TODO instead of 'string path' try a list
            if (Name == "end")
            {
                pathList.Add($"{path},{Name}");
                //Console.WriteLine($"{path},{Name}");
                return 1;
            }
            long routeCount = 0;

            bool thisWasVisited = path.Split(',').Contains(Name);

            bool someSmallCaveDoubleVisit = true;
            if (part2)
            {
                someSmallCaveDoubleVisit = CheckDoubleVisit(path);
            }

            if (Name == "start" || Char.IsUpper(Name[0]) || !someSmallCaveDoubleVisit || !thisWasVisited)
            {
                foreach (Cave cave in ConnectedTo.Where(c => c.Name != "start"))
                {
                    routeCount += cave.Walk($"{path},{Name}", ref pathList, part2);
                }
            }
            

            return routeCount;
        }

        private bool CheckDoubleVisit(string path)
        {
            if (path == "") return false;
            
            string[] parts = path.Split(',');
            List<string> visits = path.Split(',').Where(p => p != string.Empty && !Char.IsUpper(p[0]) && p != "start").ToList();

            if (visits.Count() < 2) return false;

            //todo instead of loop try LINQ Grouping
            foreach (string s in visits)
            {
                if (visits.Count(v => v==s) >1)
                {
                    return true;
                }
            }

            return false;
        }
    }
}