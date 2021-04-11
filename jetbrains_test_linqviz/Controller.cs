using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace jetbrains_test_linqviz
{
    public class Controller
    {
        int n;
        int m;
        int i = 0;

        bool[] conditional;
        int[] conditionS;
        int[] ruleParameterI;
        int[] ruleParameterJ;
        int[] ruleParameterK;
        int[] ruleParameterT;
        int[] ruleParameterP;
        int[] ruleParameterR;

        bool[] configured;

        object __consistencyLock;

        public Controller(int n, int m)
        {
            this.n = n;
            this.m = m;

            configured = new bool[n];
            conditional = new bool[n];
            conditionS = new int[n];
            ruleParameterI = new int[n];
            ruleParameterJ = new int[n];
            ruleParameterK = new int[n];
            ruleParameterT = new int[n];
            ruleParameterR = new int[n];
            ruleParameterP = new int[n];
            __consistencyLock =  new object();
        }
        void Execute(int logAfter, HashSet<string> stampsLogSet)
        {
            int department = 0;
            int nextDepartment = 0;
            bool[] stamps = new bool[m];

            do
            {
                department = nextDepartment;
                if (!configured[department])
                {
                    throw new Exception("Character is in undefined department. Configure it.");
                }
                bool stampCondition = true;
                if (conditional[department])
                {
                    stampCondition = stamps[conditionS[department]];
                }


                if (stampCondition)
                {
                    SetDeleteStamp(stamps, ruleParameterI[department], ruleParameterJ[department]);
                    nextDepartment = ruleParameterK[department];
                }
                else
                {
                    SetDeleteStamp(stamps, ruleParameterT[department], ruleParameterR[department]);
                    nextDepartment = ruleParameterP[department];
                }

                if (department == logAfter)
                {

                    string checkList = boolArrayToString(stamps);

                    if (stampsLogSet.Contains(checkList))
                    {
                        stampsLogSet.Add("loop");
                    }
                    stampsLogSet.Add(checkList);
                    return;

                }
            } while (department != n - 1);

            foreach (string str in stampsLogSet)
            {
                System.Console.WriteLine(str);
            }
        }

        private void SetDeleteStamp(bool[] stamps, int i, int j)
        {
            if (i >= 0)
            {
                stamps[i] = true;
            }
            if (j >= 0)
            {
                stamps[j] = false;
            }
        }

        public string boolArrayToString(bool[] array)
        {
            return array.Aggregate("", (acc, b) => acc + Convert.ToByte(b).ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dep"></param>
        /// <param name="i">0 if no stamp</param>
        /// <param name="j">0 if no stamp</param>
        /// <param name="k"></param>
        public void SetSimpleDepartment(int dep, int i, int j, int k)
        {
            if (dep > n || dep < 1)
            {
                throw new ArgumentException();
            }
            if (i > m || j > m || k > n || i < 0 || j < 0 || k < 1)
            {
                throw new ArgumentException();
            }
            lock (__consistencyLock)
            {
                conditional[--dep] = false;
                ruleParameterI[dep] = --i;
                ruleParameterJ[dep] = --j;
                ruleParameterK[dep] = --k;

                configured[dep] = true;
            }
        }

        /// <summary>
        /// that's what I call a convenient API
        /// </summary>
        /// <param name="dep"></param>
        /// <param name="s"></param>
        /// <param name="i">0 if no stamp</param>
        /// <param name="j">0 if no stamp</param>
        /// <param name="k"></param>
        /// <param name="t">0 if no stamp</param>
        /// <param name="r">0 if no stamp</param>
        /// <param name="p"></param>
        public void SetConditionalDepartment(int dep, int s, int i, int j, int k, int t, int r, int p)
        {
            if (dep > n || dep < 1)
            {
                throw new ArgumentException();
            }
            if (i > m || j > m || k > n || i < 0 || j < 0 || k < 1)
            {
                throw new ArgumentException();
            }
            if (t > m || r > m || p > n || t < 0 || r < 0 || p < 1)
            {
                throw new ArgumentException();
            }
            if (s > m || s < 1)
            {
                throw new ArgumentException();
            }

            lock (__consistencyLock)
            {
                conditional[--dep] = true;
                conditionS[dep] = --s;
                ruleParameterI[dep] = --i;
                ruleParameterJ[dep] = --j;
                ruleParameterK[dep] = --k;
                ruleParameterT[dep] = --t;
                ruleParameterR[dep] = --r;
                ruleParameterP[dep] = --p;

                configured[dep] = true;
            }
        }
        public HashSet<string> GetCheckList(int departmentNumber)
        {
            if (departmentNumber > n || departmentNumber < 1)
            {
                throw new ArgumentException();
            }
            departmentNumber--;
            HashSet<string> stampsLogSet = new HashSet<string>();
            lock (__consistencyLock)
            {
                Execute(departmentNumber, stampsLogSet);
            }
            return stampsLogSet;
        }

    }
}
