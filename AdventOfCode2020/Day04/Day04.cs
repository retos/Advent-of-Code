using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Day04
{
    internal class Day04 : DayBase
    {
        public override string Title => "--- Day 4: Passport Processing ---";
        public override bool Ignore => true;

        public override string Part1(List<string> input, bool isTestRun)
        {
            List<Passport> passports = Convert(input);

            return $"The number of valid passports is: {passports.Count(p => p.IsValid)}";
        }

        public override string Part2(List<string> input, bool isTestRun)
        {            
            List<Passport> passports = Convert(input);

            return $"The number of valid passports is: {passports.Count(p => p.IsValidPart2)}";
        }

        private List<Passport> Convert(List<string> input)
        {
            List<Passport> passports = new List<Passport>();
            Passport currentPassport = new Passport();
            passports.Add(currentPassport);

            foreach (string line in input)
            {
                if (string.IsNullOrEmpty(line))
                {
                    currentPassport = new Passport();
                    passports.Add(currentPassport);
                }
                else
                {
                    string[] splitted = line.Split(' ');
                    foreach (string s in splitted)
                    {
                        switch (s.Substring(0,3))
                        {
                            //byr(Birth Year)
                            case "byr":
                                currentPassport.Byr = s.Split(':')[1];
                                break;

                            //iyr(Issue Year)
                            case "iyr":
                                currentPassport.Iyr = s.Split(':')[1];
                                break;

                            //eyr(Expiration Year)
                            case "eyr":
                                currentPassport.Eyr = s.Split(':')[1];
                                break;

                            //hgt(Height)
                            case "hgt":
                                currentPassport.Hgt = s.Split(':')[1];
                                break;

                            //hcl(Hair Color)
                            case "hcl":
                                currentPassport.Hcl = s.Split(':')[1];
                                break;

                            //ecl(Eye Color)
                            case "ecl":
                                currentPassport.Ecl = s.Split(':')[1];
                                break;

                            //pid(Passport ID)
                            case "pid":
                                currentPassport.Pid = s.Split(':')[1];
                                break;

                            //cid(Country ID)
                            case "cid":
                                currentPassport.Cid = s.Split(':')[1];
                                break;

                            default:
                                throw new Exception("no category found!");
                        }
                    }
                }
            }

            return passports;
        }
    }
}
