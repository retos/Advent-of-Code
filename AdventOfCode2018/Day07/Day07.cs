using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode2018.Day07
{
    internal class Day07 : DayBase
    {
        public List<Step> AllSteps { get; set; }
        public int PassedTime { get; set; }
        public List<Worker> WorkerCollection { get; set; }
        public List<char> OrderedSteps { get; set; }
        public Step PendingStep { get; set; }

        public override string Title => "--- Day 7: The Sum of Its Parts ---";

        public override bool Ignore => true;

        public override string Part1(List<string> input, bool isTestRun)
        {
            //What a mess! Refactoring needed...

            List<StepInstruction> stepInstructions = new List<StepInstruction>();
            List<Step> steps = new List<Step>();
            List<char> orderedSteps = new List<char>();
            foreach (string s in input)
            {
                List<char> pieces = s.ToList();
                stepInstructions.Add(new StepInstruction(pieces[5], pieces[36]));
            }

            foreach (StepInstruction i in stepInstructions)
            {
                Step preStep = new Step() { Name = i.BeforeStepName };
                Step preStepInList = steps.Where(s => s.Name.Equals(preStep.Name)).FirstOrDefault();
                if (preStepInList != null)
                {
                    preStep = preStepInList;
                }
                else
                {
                    steps.Add(preStep);
                }

                Step childStep = new Step() { Name = i.Name };
                Step childStepInList = steps.Where(s => s.Name.Equals(childStep.Name)).FirstOrDefault();
                if (childStepInList != null)
                {
                    childStep = childStepInList;
                }
                else
                {
                    steps.Add(childStep);
                }
                preStep.PreConditionSteps.Add(childStep);
            }

            while (steps.Where(s => !s.Taken).ToList().Count > 0)
            {
                List<Step> possibleSteps = new List<Step>();


                //go trough all of them and filter for conditions
                foreach (Step s in steps.Where(x => !x.Taken))
                {
                    List<char> preconditions = s.PreConditionSteps.Select(x => x.Name).ToList();
                    bool allConditionsGood = true;
                    foreach (char condition in preconditions)
                    {
                        if (!orderedSteps.Contains(condition))
                        {
                            allConditionsGood = false;
                        }
                    }
                    if (allConditionsGood)
                    {
                        possibleSteps.Add(s);
                    }

                }





                Step nextStep = possibleSteps.OrderBy(s => s.Name).FirstOrDefault();

                if (nextStep == null)
                {
                    //Add steps with no parents to list, if they are still there
                    //My mistake was here, thought, that parentless steps should be done as soon as they are alpabetically next
                    List<Step> parentlessSteps = steps.Where(s => !s.Taken && s.PreConditionSteps.Count < 1).OrderBy(s => s.Name).ToList();
                    nextStep = parentlessSteps.OrderBy(s => s.Name).First();
                    //parentlessSteps.Add(nextStep);
                }

                orderedSteps.Add(nextStep.Name);
                nextStep.Taken = true;
            }

            return string.Join("", orderedSteps);
        }

       

        public override string Part2(List<string> input, bool isTestRun)
        {
            //What a mess! Refactoring needed...
            PassedTime = 0;
            WorkerCollection = new List<Worker>();
            if (isTestRun)
            {
                WorkerCollection.Add(new Worker());
                WorkerCollection.Add(new Worker());
            }
            else
            {
                WorkerCollection.Add(new Worker());
                WorkerCollection.Add(new Worker());
                WorkerCollection.Add(new Worker());
                WorkerCollection.Add(new Worker());
                WorkerCollection.Add(new Worker());
            }

            List<StepInstruction> stepInstructions = new List<StepInstruction>();
            AllSteps = new List<Step>();
            OrderedSteps = new List<char>();
            foreach (string s in input)
            {
                List<char> pieces = s.ToList();
                stepInstructions.Add(new StepInstruction(pieces[5], pieces[36]));
            }

            foreach (StepInstruction i in stepInstructions)
            {
                Step preStep = new Step() { Name = i.BeforeStepName };
                Step preStepInList = AllSteps.Where(s => s.Name.Equals(preStep.Name)).FirstOrDefault();
                if (preStepInList != null)
                {
                    preStep = preStepInList;
                }
                else
                {
                    AllSteps.Add(preStep);
                }

                Step childStep = new Step() { Name = i.Name };
                Step childStepInList = AllSteps.Where(s => s.Name.Equals(childStep.Name)).FirstOrDefault();
                if (childStepInList != null)
                {
                    childStep = childStepInList;
                }
                else
                {
                    AllSteps.Add(childStep);
                }
                preStep.PreConditionSteps.Add(childStep);
            }

            while (AllSteps.Where(s => !s.Taken).ToList().Count > 0)
            {
                Worker availableWorker = GetWorker();
                SetPendingStep();

                //Handover to worker, or abort
                if (PendingStep == null)
                {
                    return PassedTime.ToString();
                }
                PendingStep.Underprogress = true;
                while (PendingStep.PreConditionSteps.Any(p => !p.Taken))
                {
                    MoveTime();
                }
               
                availableWorker.WorkingOnStep = PendingStep;
                availableWorker.TimeLeft = PendingStep.Duration(isTestRun);

            }

            return PassedTime.ToString();
        }

        private Worker GetWorker()
        {
            Worker firstAvailableWorker;
            do
            {
                firstAvailableWorker = WorkerCollection.Where(w => w.TimeLeft <= 0).FirstOrDefault();
                if (firstAvailableWorker == null)
                {
                    //No available worker -> move time, also get next step again
                    MoveTime();
                    SetPendingStep();
                }
            } while (firstAvailableWorker == null);
            return firstAvailableWorker;
        }

        private void SetPendingStep()
        {     
            List<Step> possibleSteps = new List<Step>();

            //go trough all of them and filter for conditions
            foreach (Step s in AllSteps.Where(x => !x.Taken))
            {
                List<char> preconditions = s.PreConditionSteps.Select(x => x.Name).ToList();
                bool allConditionsGood = true;
                foreach (char condition in preconditions)
                {
                    if (!OrderedSteps.Contains(condition))
                    {
                        allConditionsGood = false;
                    }
                }
                if (allConditionsGood)
                {
                    possibleSteps.Add(s);
                }
            }

            PendingStep = possibleSteps.OrderBy(p => p.Underprogress).ThenBy(s => s.Name).FirstOrDefault();

            if (PendingStep == null)
            {
                //Add steps with no parents to list, if they are still there
                //My mistake was here, thought, that parentless steps should be done as soon as they are alpabetically next
                List<Step> parentlessSteps = AllSteps.Where(s => !s.Taken && s.PreConditionSteps.Count < 1).OrderBy(s => s.Name).ToList();
                PendingStep = parentlessSteps.OrderBy(s => s.Name).FirstOrDefault();
                //parentlessSteps.Add(nextStep);
            }
            if (PendingStep == null)
            {
                return;
            }
            if (PendingStep.Underprogress)
            {
                MoveTime();
                SetPendingStep();
            }
        }

        private void MoveTime()
        {
            //Console.SetCursorPosition(0, PassedTime);
            //Console.Write(PassedTime+ " ");
            //Console.SetCursorPosition(2, PassedTime);
            //Console.Write(WorkerCollection[0].WorkinStepChar);
            //Console.SetCursorPosition(3, PassedTime);
            //Console.Write(WorkerCollection[1].WorkinStepChar);


            PassedTime++;
            foreach (Worker w in WorkerCollection)
            {
                w.TimeLeft--;
                if (w.TimeLeft == 0)
                {
                    OrderedSteps.Add(w.WorkinStepChar);
                    w.WorkingOnStep.Taken = true;
                    w.WorkingOnStep = null;
                }
            }
        }
    }
}