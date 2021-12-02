using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day04
{
    internal class Passport
    {
        public string Byr { get; internal set; }
        public string Iyr { get; internal set; }
        public string Eyr { get; internal set; }
        public string Hgt { get; internal set; }
        public string Pid { get; internal set; }
        public string Ecl { get; internal set; }
        public string Hcl { get; internal set; }
        public string Cid { get; internal set; }
        public bool IsValid 
        { 
            get
            {
                return Byr != null && Iyr != null && Eyr != null && Hgt != null && Pid != null && Ecl != null && Hcl != null;
            }
        }

        public bool IsValidPart2 
        {
            get
            {
                if (!IsValid)
                {
                    return false;
                }

                //byr(Birth Year) - four digits; at least 1920 and at most 2002.
                int byr = int.Parse(Byr);
                if (byr<1920 || byr>2002)
                {
                    return false;
                }
                
                //iyr(Issue Year) - four digits; at least 2010 and at most 2020.
                int iyr = int.Parse(Iyr);
                if (iyr < 2010 || iyr > 2020)
                {
                    return false;
                }

                //eyr(Expiration Year) - four digits; at least 2020 and at most 2030.
                int eyr = int.Parse(Eyr);
                if (eyr < 2020 || eyr > 2030)
                {
                    return false;
                }

                //hgt(Height) - a number followed by either cm or in:
                string dimension = Hgt.Substring(Hgt.Length - 2);
                int height = int.Parse(Hgt.Substring(0, Hgt.Length - 2));
                switch (dimension)
                {
                    case "cm": //If cm, the number must be at least 150 and at most 193.
                        if (height < 150 || height > 193)
                        {
                            return false;
                        }
                        break;
                    case "in": //If in, the number must be at least 59 and at most 76.
                        if (height < 59 || height > 76)
                        {
                            return false;
                        }
                        break;
                    default:
                        return false;
                }

                //hcl(Hair Color) - a # followed by exactly six characters 0-9 or a-f.
                Regex rx = new Regex(@"^#[\da-f]{6}$");
                // Find matches.
                MatchCollection matches = rx.Matches(Hcl);
                if (matches.Count<1)
                {
                    return false;
                }

                //ecl(Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
                //php|html
                rx = new Regex(@"amb|blu|brn|gry|grn|hzl|oth");
                // Find matches.
                matches = rx.Matches(Ecl);
                if (matches.Count < 1)
                {
                    return false;
                }

                //pid(Passport ID) - a nine - digit number, including leading zeroes.
                rx = new Regex(@"^[0-9]{9}$");
                // Find matches.
                matches = rx.Matches(Pid);
                if (matches.Count < 1)
                {
                    return false;
                }
                //cid(Country ID) - ignored, missing or not.

                return true;
            }  
        }
    }
}