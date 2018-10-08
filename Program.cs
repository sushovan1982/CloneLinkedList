using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Taking user inputs for Linked List elements
                Console.WriteLine("Please enter the Linked List elements separated by space:");
                string input = Console.ReadLine();
                DoublyLinkedList dLL = new DoublyLinkedList();
                dLL.createOiginalAndClonedLinkedList(dLL, input + " "); //Passing the input to a fundtion which will create both original and cloned linked lists
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.ToString());
                Console.Read();
            }
        }
    }

    //Creating Node class for using in Linked List
    class Node
    {
        public object data;
        public Node next;
        public Node arbitrary;
    }

    public class DoublyLinkedList
    {
        Node head = new Node();
        Node current = new Node();
        List<object> lstRandom = new List<object>(); //This is for keeping track of Linked List pointers

        //Initializing Linked List
        public DoublyLinkedList()
        {
            head.data = "HEAD";
            current = head;
        }

        //Adding nodes to Linked List
        public void addToLast(object value)
        {
            try
            {
                Node newNode = new Node();
                GCHandle gcHandle = GCHandle.Alloc(newNode, GCHandleType.WeakTrackResurrection);
                IntPtr thePointer = GCHandle.ToIntPtr(gcHandle); //Pointer for new node
                newNode.data = value;
                newNode.next = null;
                current.next = newNode;
                newNode.arbitrary = current;
                current = newNode;
                lstRandom.Add(thePointer); //Listing the pointers to create random pointers for arbitrary nodes (later)
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.ToString());
                Console.Read();
            }
        }

        //Assigning arbitrary pointers randomly 
        public void createRandomPointers()
        {
            try
            {
                Random rnd = new Random();
                current = head.next;
                int pos = 0;

                while (current != null)
                {
                    rnd = new Random();
                    pos = rnd.Next(0, lstRandom.Count - 1); //Generating a random value between '0' and highest element of the pointer list
                    GCHandle gcHandle = GCHandle.FromIntPtr((IntPtr)lstRandom[pos]); //Accessing the pointer of random position
                    current.arbitrary = (Node)gcHandle.Target; //Assigning the arbitrary node of current node to the random pointer
                    lstRandom.RemoveAt(pos); //Removing the pointer to make sure it is not repeated
                    current = current.next;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.ToString());
                Console.Read();
            }
        }

        public void printLinkedList(DoublyLinkedList dLL)
        {
            try
            {
                Node current = dLL.head.next;

                while (current != null && current.arbitrary != null && current.next != null)
                {
                    Console.WriteLine("Data Element:- " + current.data + ", Next Pointer:- " + current.next.data + ", Arbitrary Pointer:- " + current.arbitrary.data);
                    current = current.next;
                }

                if (current.next == null)
                {
                    Console.WriteLine("Data Element:- " + current.data + ", Next Pointer:- NULL, Arbitrary Pointer:- " + current.arbitrary.data);
                    Console.WriteLine("--------------------------------------------------------------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.ToString());
                Console.Read();
            }
        }

        public void tempPrintLinkedList(DoublyLinkedList dLL)
        {
            try
            {
                Node current = dLL.head;
                while (current != null)
                {
                    Console.Write(current.data + " -> ");
                    current = current.next;
                }
                Console.WriteLine("NULL");
                Console.WriteLine("--------------------------------------------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.ToString());
                Console.Read();
            }
        }

        public void createOiginalAndClonedLinkedList(DoublyLinkedList dLL, string input)
        {
            try
            {
                #region CreatingOriginalPointer

                string temp = null;
                while (input.Length > 0)
                {
                    temp = input.Substring(0, input.IndexOf(" "));
                    dLL.addToLast(temp);
                    input = input.Substring(input.IndexOf(" ") + 1);
                }

                dLL.createRandomPointers();

                Console.WriteLine("Original Linked List:-");
                printLinkedList(dLL);

                #endregion

                #region CreatingDuplicateElements

                current = dLL.head.next;

                while (current != null)
                {
                    Node newNode = new Node();
                    newNode.data = current.data;
                    newNode.next = current.next;
                    current.next = newNode;
                    current = current.next.next;
                }

                Console.WriteLine("Intermediate Linked List:-");
                tempPrintLinkedList(dLL);

                #endregion

                #region AdjustingRandomPointers

                current = dLL.head.next;

                //Assigning arbitrary/random pointers of cloned Linked List
                while (current != null)
                {
                    current.next.arbitrary = current.arbitrary.next;
                    current = current.next.next;
                }

                #endregion

                #region RestoringOriginalLinkedList

                current = dLL.head.next;

                DoublyLinkedList clonedLL = new DoublyLinkedList();
                Node clonedCurrent = clonedLL.head;

                while (current != null)
                {
                    clonedCurrent.next = current.next;
                    current.next = current.next.next;
                    current = current.next;
                    clonedCurrent = clonedCurrent.next;
                }

                Console.WriteLine("Cloned Linked List:-");
                printLinkedList(clonedLL);

                Console.WriteLine("Restored Original Linked List:-");
                printLinkedList(dLL);

                Console.Read();

                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.ToString());
                Console.Read();
            }
        }
    }
}
