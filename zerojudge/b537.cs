using System;

        static void B537(string[] args)
        {
            reverse();
            /*while(true)
            {
                int k = int.Parse(Console.ReadLine());
                Console.WriteLine(forward(k));
            }*/
        }
        static string forward(int k)
        {
            if(k == 1) return 1.ToString() + " " + 1.ToString();
            else
            {
                int a = 0, b = 0;
                if(k % 2 == 0)
                {
                    string str1 = forward(k/2);
                    string[] str2 = str1.Split(" ");
                    a = int.Parse(str2[0]);
                    b = int.Parse(str2[1]);
                    a += b;
                    return a.ToString() + " " + b.ToString();
                }
                else
                {
                    string str1 = forward(k-1);
                    string[] str2 = str1.Split(" ");
                    a = int.Parse(str2[0]);
                    b = int.Parse(str2[1]);
                    (a, b) = (b, a);
                    return a.ToString() + " " + b.ToString();
                }
            }
        }

        static void reverse()
        {
            while(true)
            {
                string str = Console.ReadLine();
                if(str == "0") break;
                string[] str1 = str.Split(" ");
                bool[] isEven = new bool[10000];
                int step = 0;
                int a = int.Parse(str1[0]), b = int.Parse(str1[1]);
                //double f_k = double.Parse(str1[0]) / double.Parse(str1[1]);
                while(a != 1 || b != 1)
                {
                    if(a > b)
                    {
                        a -= b;
                        isEven[step] = true;
                    }
                    else
                    {
                        (a, b) = (b, a);
                        isEven[step] = false;
                    }
                    step++;
                }
                //Console.WriteLine("step = {0}", step);
                int k = 1;
                for(int i = step-1; i >= 0; i--)
                {
                    if (isEven[i])
                    {
                        k *= 2;
                    }
                    else
                    {
                        k ++;
                    }
                }
                Console.WriteLine("f({0}) = {1}/{2}", k, str1[0], str1[1]);
            }
        }
