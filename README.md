# CloneLinkedList
This C# program will create a Linked List with one next and one random/arbitrary pointer based on user input. Then clone that Linked List with time complexity of O(n).
Following algorithm is used to clone the Linked List:-
1) Create the copy of node 1 and insert it between node 1 & node 2 in original Linked List, create the copy of 2 and insert it between 2 & 3.. Continue in this fashion, add the copy of N afte the Nth node.
2) Now copy the arbitrary link in this fashion:-
original->next->arbitrary = original->arbitrary->next;  /*TRAVERSE TWO NODES*/
This works because original->next is nothing but copy of original and Original->arbitrary->next is nothing but copy of arbitrary.
3) Now restore the original and copy linked lists in this fashion in a single loop.
original->next = original->next->next;
copy->next = copy->next->next;
4) Make sure that last element of original->next is NULL.
