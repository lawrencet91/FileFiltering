using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileFiltering
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList<string> linked = new LinkedList<string>();
            bool isAdd = false;

            using (var reader = new StreamReader("data.txt"))
            {
                string line;
                while((line = reader.ReadLine())!=null)
                {
                    string[] info = line.Split(' ');
                    string empId = info[0].ToString();
                    string firstname = info[1].ToString();
                    string lastname = info[2].ToString();
                    string supervisorid = info[3].ToString();
                    if(linked.Count!=0)
                    {
                        if (supervisorid == "-")
                        {
                            linked.AddFirst(line);
                        }
                        else
                        {
                            var node = linked.First;
                            while(node!=null)
                            {
                                var next = node.Next;
                                if(node.Value.Contains(supervisorid))
                                {
                                    linked.AddAfter(node,line);
                                    isAdd = true;
                                    break;
                                }
                                node = next;
                                isAdd = false;
                            }
                            
                            if(!isAdd)
                            {
                                linked.AddLast(line);                               
                            }
                        } 
                    }
                    else
                    {
                        linked.AddLast(line);
                    }                                     
                }

                var currentNode = linked.First;
                string bossID = currentNode.Value.ToString().Split(' ')[0];
                while (currentNode != null)
                {                    
                    var nextNode = currentNode.Next;
                    var previousNode = currentNode.Previous;
                    string currentPieces = currentNode.Value.ToString().Split(' ')[0];
                    string previousPieces = "";
                    string nextPieces = "";
                    if (previousNode!=null)
                    {
                        previousPieces = previousNode.Value.ToString().Split(' ')[0];
                    }
                    if(nextNode!=null)
                    {
                        nextPieces = nextNode.Value.ToString().Split(' ')[3];
                    }

                    if (nextPieces.Equals(bossID))
                    {
                        Console.WriteLine(currentNode.Value.ToString());
                        Console.WriteLine("\t"+nextNode.Value.ToString());
                    }else if(nextPieces.Equals(currentPieces)||nextPieces.Equals(previousPieces))
                    {
                        Console.WriteLine("\t\t"+nextNode.Value.ToString());
                    }else if(nextNode!=null)
                    {
                        Console.WriteLine(nextNode.Value.ToString());
                    }
                    currentNode = nextNode;
                }

              
                Console.ReadLine();
            }
        }
    }
}
