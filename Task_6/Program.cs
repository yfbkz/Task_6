using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;


namespace Task_6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string s1 = Console.ReadLine();
            string letters = "abcdefghijklmnopqrstuvwxyz";
            string incor = "";
            for (int i = 0; i < s1.Length; i++)
            {
                if (!letters.Contains(Convert.ToString(s1[i])))
                {
                    incor += $"{s1[i]} ";
                }
            }
            if (s1 != "" && incor == "")
            {
                string s4 = "";
                int a = s1.Length - 1;
                if (s1.Length % 2 == 0)
                {
                    a = a / 2;
                    string s2 = s1.Substring(0, a + 1);
                    string s3 = s1.Substring(a + 1);
                    for (int i = 0; i <= a; i++)
                    {
                        s4 = s4 + s2[a - i];
                    }
                    for (int i = 0; i <= a; i++)
                    {
                        s4 = s4 + s3[a - i];
                    }
                }
                else
                {
                    for (int i = 0; i <= a; i++)
                    {
                        s4 = s4 + s1[a - i];
                    }
                    s4 = s4 + s1;
                }
                Console.WriteLine(s4);
                char[] chars = s4.Distinct().ToArray();

                for (int i = 0; i < chars.Length; i++)
                {
                    int q = 0;
                    for (int j = 0; j < s4.Length; j++)
                    {
                        if (chars[i] == s4[j])
                        {
                            q += 1;
                        }
                    }
                    Console.WriteLine($"{chars[i]}: {q}");
                }

                for (int i = 0; i < s4.Length; i++)
                {
                    if ("aeiouy".Contains(s4[i]))
                    {
                        for (int j = s4.Length - 1; j >= 0; j--)
                        {
                            if ("aeiouy".Contains(s4[j]))
                            {
                                Console.WriteLine($"Substring: {s4.Substring(i, j - i + 1)}");
                                break;

                            }
                        }
                        break;
                    }
                    if (i == s4.Length - 1)
                    {
                        Console.WriteLine("Substring is empty.");
                    }
                }

                char method;
                Console.WriteLine("Sorting method (T-Tree sorting; Q- Quick sorting): ");
                method = Convert.ToChar(Console.Read());
                if (method == 'T') { Console.WriteLine(String.Join("", Sort.TreeSort(s4.ToCharArray()))); }
                else if (method == 'Q')
                {
                    Console.WriteLine(String.Join("", Sort.QuickSort(s4.ToCharArray())));
                }
                else { Console.WriteLine("Incorrect method input."); }



                DeleteLetter(s4);


            }
            else if (s1 == "")
            {
                Console.WriteLine("Empty line.");
            }
            else
            {
                Console.WriteLine($"Incorrect input: {incor}");

            }

            Console.ReadLine();
            Console.ReadLine();

        }
        static void DeleteLetter(string str)
        {
            int ind;
            try
            {
                string text = $"http://www.randomnumberapi.com/api/v1.0/random?min=0&max={str.Length - 1}&count=1";
                WebRequest wr = WebRequest.Create(text);
                Stream objSt = wr.GetResponse().GetResponseStream();
                StreamReader objRd = new StreamReader(objSt);
                ind = Convert.ToInt16(objRd.ReadLine().Trim('[', ']'));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Random random = new Random();
                ind = random.Next(0, str.Length - 1);
            }

            Console.WriteLine($"Trimmed string: {str.Remove(ind, 1)} (Deleted index:{ind})");
        }

    }
    public abstract class Sort
    {
        static void Swap(ref char x, ref char y)
        {
            var t = x;
            x = y;
            y = t;
        }

        static int Partition(char[] array, int minIndex, int maxIndex)
        {
            var pivot = minIndex - 1;
            for (var i = minIndex; i < maxIndex; i++)
            {
                if (array[i] < array[maxIndex])
                {
                    pivot++;
                    Swap(ref array[pivot], ref array[i]);
                }
            }

            pivot++;
            Swap(ref array[pivot], ref array[maxIndex]);
            return pivot;
        }

        static char[] Quick(char[] array, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex)
            {
                return array;
            }

            var pivotIndex = Partition(array, minIndex, maxIndex);
            Quick(array, minIndex, pivotIndex - 1);
            Quick(array, pivotIndex + 1, maxIndex);

            return array;
        }

        public static char[] QuickSort(char[] array)
        {
            return Quick(array, 0, array.Length - 1);
        }




        public static char[] TreeSort(char[] array)
        {
            var treeNode = new TreeNode(array[0]);
            for (int i = 1; i < array.Length; i++)
            {
                treeNode.Insert(new TreeNode(array[i]));
            }

            return treeNode.Transform();
        }
        public class TreeNode
        {
            public TreeNode(char data)
            {
                Data = data;
            }

            public char Data { get; set; }
            public TreeNode Left { get; set; }
            public TreeNode Right { get; set; }

            public void Insert(TreeNode node)
            {
                if (node.Data < Data)
                {
                    if (Left == null)
                    {
                        Left = node;
                    }
                    else
                    {
                        Left.Insert(node);
                    }
                }
                else
                {
                    if (Right == null)
                    {
                        Right = node;
                    }
                    else
                    {
                        Right.Insert(node);
                    }
                }
            }
            public char[] Transform(List<char> elements = null)
            {
                if (elements == null)
                {
                    elements = new List<char>();
                }

                if (Left != null)
                {
                    Left.Transform(elements);
                }

                elements.Add(Data);

                if (Right != null)
                {
                    Right.Transform(elements);
                }

                return elements.ToArray();
            }
        }
    }
}

